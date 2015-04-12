using PSU_Calculator.DataWorker;
using PSU_Calculator.Dateizugriffe;
using PSU_Calculator.Komponenten;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PSU_Calculator
{
  public partial class Form1 : Form
  {
    int verbrauch = 20;
    public Form1()
    {
      PSUCalculatorSettings.Get();
      InitializeComponent();
      SetTags();
      addGPU();
      ActiveComponents.Get().CbxCoolingSolution = cbxCooling;
      ActiveComponents.Get().CbxOC = cbxOverclocking;

      LoaderModul m = LoaderModul.getInstance();

      m.LoadCPU(this.cbxCpu);
      m.LoadCPU(this.cbxCPU2);
      m.setCPUSearchEvent(this.cbxCpu);
      m.setCPUSearchEvent(this.cbxCPU2);

      m.LoadPhysXKarten(this.cbxPhysx);
      m.setPhysXSearchEvent(this.cbxPhysx);

      m.LoadCoolingSolutions(this.cbxCooling);
      m.LoadOCVariations(this.cbxOverclocking);

      setUpdateVerbrauchEvent();

      new Updater().RunUpdateAsync();

      berechneVerbrauch(this, null);
      AddPriceComparorsToToolStrip();
      DoubleBuffered = true;
    }

    private void SetTags()
    {
      soundblaster.Tag = new PcComponent("Soundblaster", 12, "None");
      soundblasterwfront.Tag = new PcComponent("SoundblasterwFront", 15, "None");
      tvtuner.Tag = new PcComponent("TV-Tuner", 11, "None");
      raidcard.Tag = new PcComponent("Raidcard", 8, "None");
      fancontroll.Tag = new PcComponent("Fancontroll", 5, "None");
      cardreader.Tag = new PcComponent("Cardreader", 2, "None");
      lcd.Tag = new PcComponent("LCD", 4, "None");

      //Drop down Boxen mit den entsprechenden Werten versehen
      cbxKaltlicht.Items.Clear();
      cbxKaltlicht.Items.AddRange(CountList(5, 5, "Kaltlicht", Stecker.SteckerType.Molex.ToString()));
      cbxHDD.Items.Clear();
      cbxHDD.Items.AddRange(CountList(5, 15, "HDD", Stecker.SteckerType.Sata.ToString()));
      cbxLaufwerke.Items.Clear();
      cbxLaufwerke.Items.AddRange(CountList(5, 10, "Laufwerke", Stecker.SteckerType.Sata.ToString()));
      cbxSSD.Items.Clear();
      cbxSSD.Items.AddRange(CountList(4, 10, "SSD", Stecker.SteckerType.Sata.ToString()));
      cbxFans.Items.Clear();
      cbxFans.Items.AddRange(CountList(2, 30, "Lüfter"));
      cbxLED.Items.Clear();
      cbxLED.Items.AddRange(CountList(3, 5, "LED", Stecker.SteckerType.Molex.ToString()));
    }

    private object[] CountList(int tdp, int maxvalue, string type, string steckertyp)
    {
      object[] output = new object[maxvalue + 1];
      PcComponent com;
      for (int i = 0; i <= maxvalue; i++)
      {
        com = new PcComponent(i.ToString(), tdp * i, type);

        if (!string.IsNullOrEmpty(steckertyp))
        {
          Element ele = new Element("Stecker");
          ele.addAttribut(steckertyp, i.ToString());
          com.XML = com.XML.addElement(Element.New("Data").addElement(ele));
        }
        output[i] = com;
      }
      return output;
    }
    private object[] CountList(int tdp, int maxvalue, string type)
    {
      return CountList(tdp, maxvalue, type, "");
    }

    /// <summary>
    /// DAs event setzten für das Updaten des verbrauches und der Netzteilemepfehlungen
    /// </summary>
    private void setUpdateVerbrauchEvent()
    {
      cbxHDD.SelectedIndex = 0;
      cbxSSD.SelectedIndex = 0;
      cbxLaufwerke.SelectedIndex = 0;
      cbxOverclocking.SelectedIndex = 0;
      cbxFans.SelectedIndex = 0;
      cbxKaltlicht.SelectedIndex = 0;
      cbxCooling.SelectedIndex = 0;
      cbxLED.SelectedIndex = 0;

      foreach (Control tmpControl in this.gbxDaten.Controls)
      {
        ActiveComponents.Get().AddControl(tmpControl);
        if (tmpControl is CheckBox)
        {
          (tmpControl as CheckBox).CheckedChanged += new System.EventHandler(this.berechneVerbrauch);
        }
        else if (tmpControl is ComboBox)
        {
          (tmpControl as ComboBox).SelectedIndexChanged += new System.EventHandler(this.berechneVerbrauch);
        }
      }
    }

    /// <summary>
    /// Berechnet den Verbrauch des Rechners anhand der TDP und stösst danach die Anzeige der guten Netzteile an.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void berechneVerbrauch(object sender, System.EventArgs e)
    {
      int verbrauch = ActiveComponents.Get().GetWattage();
      lblVerbrauch.Text = verbrauch.ToString();
      if (verbrauch > 1000)
      {
        pgbEffizienz.Value = 1000;
      }
      else
      {
        pgbEffizienz.Value = verbrauch;
      }
      //EMpfehlenswerte Netzteile anzeigen lassen
      SetPsuGuiList(ActiveComponents.Get().EmpfohleneNetzteile(verbrauch));
    }

    /// <summary>
    /// Netzteile für den Rechner empfehlen
    /// </summary>
    /// <param name="Watt"></param>
    public List<PowerSupply> EmpfehleNetzteile(int Watt)
    {
      List<PowerSupply> empfehlenswerte = new List<PowerSupply>();
      foreach (PowerSupply nt in LoaderModul.getInstance().GetPowerSupplys())
      {
        if (nt.UsageLoadMaximum < Watt)
        {
          continue;
        }
        if (nt.UsageLoadMinimum != -1)
        {
          if (nt.UsageLoadMinimum < Watt)
          {
            empfehlenswerte.Add(nt);
          }
          continue;
        }
        if (nt.UsageLoadMaximum < (Watt + 100 + (nt.TDP * 0.1)))
        {
          //Empfehlen
          empfehlenswerte.Add(nt);
        }
      }
      return empfehlenswerte;
    }

    /// <summary>
    /// Erstellt die Labels mit Farbe für die ANzeige in der Linklist.
    /// </summary>
    /// <param name="empfehlenswerte"></param>
    private void SetPsuGuiList(List<PowerSupply> empfehlenswerte)
    {
      List<Control> tmpControls = new List<Control>();
      foreach (PowerSupply nt in empfehlenswerte)
      {
        var color = Color.Blue;
        string link = "";
        if (PSUCalculatorSettings.Get().ShowPriceComparer)
        {
          link = nt.CurrentPresvergleichLink;
          if (string.IsNullOrEmpty(link))
          {
            link = nt.AnyPresvergleichLink;
            color = Color.Navy;
            if (string.IsNullOrEmpty(link))
            {
              color = Color.Black;
            }
          }
        }
        else
        {
          if (nt.Testberichte.Count == 0)
          {
            color = Color.Black;
          }
        }
        LinkLabel l = new LinkLabel();
        l.AutoSize = true;
        l.BackColor = System.Drawing.Color.Transparent;
        l.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        l.ForeColor = System.Drawing.Color.Black;
        l.Location = new System.Drawing.Point(5, 2);
        l.Name = link;
        l.Size = new System.Drawing.Size(35, 15);
        l.TabIndex = 64;
        l.Text = nt.ToString();
        l.LinkColor = color;
        l.Click += GoToLink;
        l.Tag = nt;

        Panel p = new Panel();
        p.AutoSize = true;
        p.Controls.Add(l);

        tmpControls.Add(p);
      }
      FlowLayoutPanel pnl = new FlowLayoutPanel();
      pnl.Anchor = pnlNetzteile.Anchor;
      pnl.FlowDirection = pnlNetzteile.FlowDirection;
      pnl.Location = pnlNetzteile.Location;
      pnl.Name = pnlNetzteile.Name;
      pnl.Size = pnlNetzteile.Size;
      pnl.TabIndex = pnlNetzteile.TabIndex;
      pnl.Controls.AddRange(tmpControls.ToArray());

      gbxNetzteile.Controls.Remove(pnlNetzteile);
      gbxNetzteile.Controls.Add(pnl);
      pnlNetzteile = pnl;
      //pnlNetzteile.SuspendLayout();
      //pnlNetzteile.Controls.Clear();
      //pnlNetzteile.Controls.AddRange(tmpControls.ToArray());
      //pnlNetzteile.ResumeLayout();
    }

    void GoToLink(object sender, System.EventArgs e)
    {
      string url = "";
      LinkLabel orginal;
      if (sender is LinkLabel)
      {
        orginal = (sender as LinkLabel);
        url = orginal.Name;
      }
      else
      {
        return;
      }

      if (PSUCalculatorSettings.Get().ShowPriceComparer)
      {
        if (url.StartsWith("http://geizhals") || url.StartsWith("http://www.toppreise"))
        {
          System.Diagnostics.Process.Start(url);
        }
      }
      else
      {
        PowerSupply ps = (PowerSupply)orginal.Tag;
        if (ps.Testberichte.Count > 0)
        {
          new TestberichteAuswahl(ps).ShowDialog();
        }
      }
    }

    void chkdualcpu_CheckedChanged(object sender, System.EventArgs e)
    {
      lblCPU2.Visible = !lblCPU2.Visible;
      cbxCPU2.Visible = !cbxCPU2.Visible;
      PlaziereElemente();
    }

    /// <summary>
    /// Handeln ob Singel GPU oder ob Multi GPU zum einsatz kommt.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void chkCFSLI_CheckedChanged(object sender, System.EventArgs e)
    {
      if (chkCFSLI.Checked)
      {
        if (cbxGrakaList.Count == 1)
        {
          addGPU();
          return;
        }
        else
        {
          for (int i = 0; i < cbxGrakaList.Count; i++)
          {
            cbxGrakaList[i].Visible = true;
            lblGrakaList[i].Visible = true;
          }
        }
      }
      else
      {
        for (int i = 1; i < cbxGrakaList.Count; i++)
        {
          cbxGrakaList[i].Visible = false;
          lblGrakaList[i].Visible = false;
        }
      }
      PlaziereElemente();
      renameGPULabels();
    }

    private void addGPU()
    {
      //Grafikkarte Combobox erstellen
      System.Windows.Forms.ComboBox cbxGraka = new System.Windows.Forms.ComboBox();
      cbxGraka.FormattingEnabled = true;
      cbxGraka.Location = new System.Drawing.Point(87, 42);
      cbxGraka.Name = "cbxGraka";
      cbxGraka.Size = new System.Drawing.Size(173, 21);
      cbxGraka.Text = "Bitte wählen";
      cbxGraka.SelectedIndexChanged += new System.EventHandler(this.berechneVerbrauch);
      cbxGraka.SelectedIndexChanged += new System.EventHandler(this.handleAnzahlGPU);

      //Grafikkarte Label erstellen
      System.Windows.Forms.Label lblGraka = new Label();
      lblGraka.AutoSize = true;
      lblGraka.BackColor = System.Drawing.Color.Transparent;
      lblGraka.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      lblGraka.ForeColor = System.Drawing.Color.Black;
      lblGraka.Location = new System.Drawing.Point(9, 43);
      lblGraka.Name = "lblGraka";
      lblGraka.Size = new System.Drawing.Size(69, 15);
      lblGraka.Text = "Grafikkarte:";

      this.gbxDaten.Controls.Add(cbxGraka);
      this.gbxDaten.Controls.Add(lblGraka);

      this.lblGrakaList.Add(lblGraka);
      this.cbxGrakaList.Add(cbxGraka);
      PlaziereElemente();
      LoaderModul.getInstance().LoadGPU(cbxGraka);
      LoaderModul.getInstance().setGPUSearchEvent(cbxGraka);
      ActiveComponents.Get().AddControl(cbxGraka);
      renameGPULabels();
    }

    //Label der GPU's setzten, für ersichtlichkeit
    private void renameGPULabels()
    {
      if (lblGrakaList.Count == 1 || (lblGrakaList.Count > 1 && !chkCFSLI.Checked))
      {
        lblGrakaList[0].Text = "Grafikkarte:";
      }
      else if (lblGrakaList.Count > 1)
      {
        for (int i = 0; i < lblGrakaList.Count; i++)
        {
          lblGrakaList[i].Text = string.Format("Grafikkarte {0}:", i + 1);
        }
      }
    }

    /// <summary>
    /// Anzahl der SLots für GPU handhaben, also ob Dual/Tripple/Quad oder gar GPGPU Rig.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void handleAnzahlGPU(object sender, System.EventArgs e)
    {
      //nur wenn Mehrere GPU's ausgewählt sind
      if (!chkCFSLI.Checked)
      {
        return;
      }

      //Anzahl leere ausfindig machen
      int nrOfEmpty = 0;
      foreach (ComboBox box in cbxGrakaList)
      {
        if (box.SelectedItem is PcComponent)
        {
          if ((box.SelectedItem as PcComponent).TDP == 0)
          {
            nrOfEmpty++;
          }
        }
        else
        {
          nrOfEmpty++;
        }
      }

      //Keine Leeren, neue GPU anfügen
      if (nrOfEmpty == 0)
      {
        addGPU();
        return;
      }
      else if (nrOfEmpty == 1)//1 Leere, nix tun
      {
        return;
      }

      //mehr als 1 leere, reduzieren auf 1 leere
      for (int i = 0; i < cbxGrakaList.Count; i++)
      {
        if (cbxGrakaList[i].SelectedItem is PcComponent)
        {
          if ((cbxGrakaList[i].SelectedItem as PcComponent).TDP == 0)
          {
            nrOfEmpty++;
          }
        }
        else
        {
          nrOfEmpty++;
        }
        if (nrOfEmpty > 1 && cbxGrakaList.Count > 2)
        {
          ActiveComponents.Get().RemoveControl(cbxGrakaList[i]);
          cbxGrakaList.Remove(cbxGrakaList[i]);
          lblGrakaList.Remove(lblGrakaList[i]);
        }
      }
      PlaziereElemente();
      renameGPULabels();
    }

    private void LinkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      System.Diagnostics.Process.Start("http://extreme.pcgameshardware.de/tools-anwendungen-und-sicherheit/355665-der-pcgh-community-netzteil-calculator-free.html");
    }

    void hilfeToolStripMenuItem_Click(object sender, System.EventArgs e)
    {
      System.Diagnostics.Process.Start("http://extreme.pcgameshardware.de/tools-anwendungen-und-sicherheit/355665-der-pcgh-community-netzteil-calculator-free.html");
    }

    void AllgemeinerNetzeilDiskusionsthread(object sender, System.EventArgs e)
    {
      System.Diagnostics.Process.Start("http://extreme.pcgameshardware.de/netzteile-und-gehaeuse/105022-allgemeiner-diskussionsthread-zu-netzteilen.html");
    }

    void SRvsMR(object sender, System.EventArgs e)
    {
      System.Diagnostics.Process.Start("http://extreme.pcgameshardware.de/netzteile-und-gehaeuse/326979-single-rail-vs-multi-rail-ist-das-vorteile-und-nachteile.html");
    }

    void guenstigVSbillig(object sender, System.EventArgs e)
    {
      System.Diagnostics.Process.Start("http://extreme.pcgameshardware.de/netzteile-und-gehaeuse/351669-poiu-s-kleiner-leitfaden-wie-man-als-laie-ein-guenstiges-von-einem-billigen-netzteil-unterscheidet.html");
    }

    void anzeigeInfo(object sender, System.EventArgs e)
    {
      About b = new About();
      b.ShowDialog();
    }

    void anzeigeFAQ(object sender, System.EventArgs e)
    {
      FAQ b = new FAQ();
      b.ShowDialog();
    }

    //Netzteile in die Zwischenablage Kopieren für ein einfügen im Forum.
    private void cmdCopyToForum_Click(object sender, EventArgs e)
    {
      List<PowerSupply> netzteile = EmpfehleNetzteile(verbrauch);
      StringBuilder sb = new StringBuilder();
      foreach (PowerSupply nt in netzteile)
      {
        sb.Append("[URL=\"");
        sb.Append(nt.AnyPresvergleichLink);
        sb.Append("\"]");
        sb.Append(nt.Name);
        sb.Append("[/URL]\n");
      }
      System.Windows.Forms.Clipboard.SetDataObject(sb.ToString(), true);
    }

    private void deutschToolStripMenuItem_Click(object sender, EventArgs e)
    {

    }

    private void AddPriceComparorsToToolStrip()
    {
      foreach (string comparer in PowerSupply.PriceComparer)
      {

        var toolStripMenuEntrie = new ToolStripMenuItem();
        toolStripMenuEntrie.Name = comparer + "ToolStripMenuItem";
        toolStripMenuEntrie.Size = new System.Drawing.Size(152, 22);
        toolStripMenuEntrie.Text = UppercaseFirst(comparer);
        toolStripMenuEntrie.Click += toolStripMenuEntrie_Click;

        suchmaschineToolStripMenuItem.DropDownItems.Add(toolStripMenuEntrie);
      }
    }

    private string UppercaseFirst(string s)
    {
      if (string.IsNullOrEmpty(s))
      {
        return string.Empty;
      }
      if (s.Length <= 3)
      {
        return s.ToUpper();
      }
      char[] a = s.ToCharArray();
      a[0] = char.ToUpper(a[0]);
      return new string(a);
    }

    private void toolStripMenuEntrie_Click(object sender, EventArgs e)
    {
      var entry = sender as ToolStripMenuItem;
      var set = PSUCalculatorSettings.Get();
      set.SetSearchEngine(entry.Text);
      set.SaveSettings();
      berechneVerbrauch(null, null);
    }

    private void radioPreisvergleich_CheckedChanged(object sender, EventArgs e)
    {
      PSUCalculatorSettings.Get().ShowPriceComparer = true;
      berechneVerbrauch(null, null);
    }

    private void radioTestberichte_CheckedChanged(object sender, EventArgs e)
    {
      PSUCalculatorSettings.Get().ShowPriceComparer = false;
      berechneVerbrauch(null, null);
    }

    private void cmdCopySystem_Click(object sender, EventArgs e)
    {
      Element ele = new Element("Choosen");
      foreach (PcComponent com in ActiveComponents.Get().GetAktiveComponents())
      {
        if (com.IsEmpty())
        {
          continue;
        }
        ele.addElement(com.XML);
      }
      ele.addElement(new Element("Watt", ActiveComponents.Get().GetWattage().ToString()));
      System.Windows.Forms.Clipboard.SetDataObject(ele.getXML(), true);
      StorageMapper.WriteToFilesystem(PSUCalculatorSettings.GetXmlFilePath(PSUCalculatorSettings.MySystem), ele.getXML());
    }

    private string GetItem(object o)
    {
      if (o == null)
      {
        return "";
      }
      return o.ToString();
    }

    void Form1_UpdatePricesEvent(Prices sender, PowerSupply psu)
    {
      SetPsuGuiList(ActiveComponents.Get().EmpfohleneNetzteile());
    }

    private void themeToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Prices.Get().UpdatePricesEvent += Form1_UpdatePricesEvent;
      Prices.Get().Start(this);
    }

    private void cbxConectors_CheckedChanged(object sender, EventArgs e)
    {
      PSUCalculatorSettings.Get().ConnectorsHaveToFit = (sender as CheckBox).Checked;
    }
  }
}
