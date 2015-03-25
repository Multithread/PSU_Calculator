using PSU_Calculator.Dateizugriffe;
using PSU_Calculator.Komponenten;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
    Thread t = null;
    public Form1()
    {
      string data = "name;LC-Power LC 9550 500W;tdp;500;max;400;quali;40;DE;http://geizhals.de/lc-power-gold-series-lc9550-v2-3-500w-atx-2-3-a861223.html";
      ComponentStringSplitter css = new ComponentStringSplitter(data);
      while (css.HasNext())
      {
        string value = css.GetValueForKey(css.Next());
      }

      InitializeComponent();

      addGPU();

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

      //Nach Updates suchen nachdem das Programm inizialisiert wurde
      t = new Thread(new ParameterizedThreadStart(TestForUpdates));
      t.IsBackground = true;
      t.Start();
    }

    //Updates herunterladen von Github.
    void TestForUpdates(object _data)
    {
      try
      {
        StorageMapper.GetLocalData(addToBoxInvvoke);

        WebClient c = new WebClient();
        LoaderModul m = LoaderModul.getInstance();
        string version = c.DownloadString("https://raw.githubusercontent.com/Multithread/PSU_Calculator/master/" + PSUCalculatorSettings.Version);
        version = version.Replace("\n", "");
        if (Properties.Einstellungen.Default.Version.Equals(version))
        {
          return;
        }

        //CPU liste updaten
        string b = c.DownloadString("https://raw.githubusercontent.com/Multithread/PSU_Calculator/master/" + PSUCalculatorSettings.CPU);
        string[] cpu = b.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
        m.AddCPURange(m.GetComponents(cpu));

        addToBoxInvvoke(true);

        //GPU liste updaten
        b = c.DownloadString("https://raw.githubusercontent.com/Multithread/PSU_Calculator/master/" + PSUCalculatorSettings.GPU);
        string[] gpu = b.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
        m.AddGPURange(m.GetComponents(gpu));

        addToBoxInvvoke(false);

        //Netzteile Liste updaten
        b = c.DownloadString("https://raw.githubusercontent.com/Multithread/PSU_Calculator/master/" + PSUCalculatorSettings.PowerSupplys);
        string[] nt = b.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
        m.AddNetzteilRange(m.GetPowerSupplysFromArray(nt));

        if (StorageMapper.SetLocalData(version, m.GPU, m.CPU, m.Netzteile))
        {
          Properties.Einstellungen.Default.Version = version;
        }
        Properties.Einstellungen.Default.Save();
      }
      catch (Exception ex)
      {
        int b = ex.ToString().Length;
        if (b > 10)
        {
          b = 10;
        }
      }
    }

    //Invoke für das hinzufügen von Elementen zu einer Combobox
    public void addToBoxInvvoke(bool _isCPU)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new boxInvoke(addToBoxInvvoke), new object[] { _isCPU });
        return;
      }
      object o = null;
      //CPU's aktualsisieren
      if (_isCPU)
      {
        o = this.cbxCpu.SelectedItem;
        LoaderModul.getInstance().LoadCPU(this.cbxCpu);
        if (o != null)
        {
          this.cbxCpu.SelectedItem = o;
        }
        o = this.cbxCPU2.SelectedItem;
        LoaderModul.getInstance().LoadCPU(this.cbxCPU2);
        if (o != null)
        {
          this.cbxCPU2.SelectedItem = o;
        }
      }
      else
      {
        //GPU's aktualisieren
        foreach (ComboBox box in cbxGrakaList)
        {
          o = box.SelectedItem;
          LoaderModul.getInstance().LoadGPU(box);
          if (o != null)
          {
            box.SelectedItem = o;
          }
        }
      }
    }
    public delegate void boxInvoke(bool _isCPU);

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

      foreach (Control c in this.gbxDaten.Controls)
      {
        if (c is CheckBox)
        {
          (c as CheckBox).CheckedChanged += new System.EventHandler(this.berechneVerbrauch);
        }
        else if (c is ComboBox)
        {
          (c as ComboBox).SelectedIndexChanged += new System.EventHandler(this.berechneVerbrauch);
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
      OC oc = cbxOverclocking.SelectedItem as OC; //Übertaktung auslesen zur weiteren bearbeitung
      verbrauch = 20;//Verbrauch MB mit Controlern
      CoolingSolution kuehlloesung = (cbxCooling.SelectedItem as CoolingSolution);//Kühlung merken, wichtig für OC

      //Kühllösung Strommverbrauch hinzufügen
      if (kuehlloesung.OnlyOnce)
      {
        verbrauch += kuehlloesung.TDP;
      }
      else
      {
        if (cbxCpu.SelectedItem is PcComponent && (cbxCpu.SelectedItem as PcComponent).TDP > 0)
        {
          verbrauch += kuehlloesung.TDP;
        }
        if (chkdualcpu.Checked && cbxCPU2.SelectedItem is PcComponent && (cbxCPU2.SelectedItem as PcComponent).TDP > 0)
        {
          verbrauch += kuehlloesung.TDP;
        }
      }

      //CPU
      if (cbxCpu.SelectedItem is PcComponent)
      {
        verbrauch += oc.CalculateCPU_OCUsageInWatt((cbxCpu.SelectedItem as PcComponent).TDP, kuehlloesung);
      }
      if (chkdualcpu.Checked && cbxCPU2.SelectedItem is PcComponent)
      {
        verbrauch += oc.CalculateCPU_OCUsageInWatt((cbxCPU2.SelectedItem as PcComponent).TDP, kuehlloesung);
      }

      //GPU
      foreach (ComboBox box in cbxGrakaList)
      {
        if (box.SelectedItem is PcComponent)
        {
          verbrauch += oc.CalculateGPU_OCUsageInWatt((box.SelectedItem as PcComponent).TDP, kuehlloesung);
        }
        if (!chkCFSLI.Checked)
        {
          break;
        }
      }
      if (cbxPhysx.SelectedItem is PcComponent)
      {
        verbrauch += (cbxPhysx.SelectedItem as PcComponent).TDP;
      }

      //Laufwerke/HDD/SSD/Lüfter
      verbrauch += cbxHDD.SelectedIndex * 5;
      verbrauch += cbxSSD.SelectedIndex * 4;
      verbrauch += cbxLaufwerke.SelectedIndex * 5;
      verbrauch += cbxFans.SelectedIndex * 2;

      //Weiteres
      verbrauch += cbxKaltlicht.SelectedIndex * 5;
      verbrauch += cbxLED.SelectedIndex * 3;
      if (soundblaster.Checked)
      {
        verbrauch += 12;
      }
      if (soundblasterwfront.Checked)
      {
        verbrauch += 15;
      }
      if (tvtuner.Checked)
      {
        verbrauch += 11;
      }
      if (raidcard.Checked)
      {
        verbrauch += 8;
      }
      if (fancontroll.Checked)
      {
        verbrauch += 5;
      }
      if (cardreader.Checked)
      {
        verbrauch += 2;
      }
      if (lcd.Checked)
      {
        verbrauch += 4;
      }

      if (verbrauch < 1001)
      {
        pgbEffizienz.Value = verbrauch;
      }
      else
      {
        pgbEffizienz.Value = 1000;
      }

      lblVerbrauch.Text = verbrauch.ToString();

      //EMpfehlenswerte Netzteile anzeigen lassen
      AddToView(EmpfehleNetzteile(verbrauch));
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
        if (nt.UsageLoadMaximum > Watt)
        {
          if (nt.UsageLoadMaximum < (Watt + 100 + (nt.TDP * 0.1)))
          {
            //Empfehlen
            empfehlenswerte.Add(nt);
          }
        }
        else if (Watt > 1000)
        {

        }
      }
      return empfehlenswerte;
    }

    private void AddToView(List<PowerSupply> empfehlenswerte)
    {
      pnlNetzteile.SuspendLayout();
      pnlNetzteile.Controls.Clear();
      foreach (PowerSupply nt in empfehlenswerte)
      {
        LinkLabel l = new LinkLabel();
        l.AutoSize = true;
        l.BackColor = System.Drawing.Color.Transparent;
        l.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        l.ForeColor = System.Drawing.Color.Black;
        l.Location = new System.Drawing.Point(5, 2);
        l.Name = nt.Geizhals;
        l.Size = new System.Drawing.Size(35, 15);
        l.TabIndex = 64;
        l.Text = nt.Name;
        l.Click += gotoGhLink;


        Panel p = new Panel();
        p.AutoSize = true;
        p.Controls.Add(l);

        pnlNetzteile.Controls.Add(p);
      }
      pnlNetzteile.ResumeLayout();
    }

    void gotoGhLink(object sender, System.EventArgs e)
    {
      string url = "";
      if (sender is LinkLabel)
      {
        url = (sender as LinkLabel).Name;
      }

      if (url.StartsWith("http://geizhals.de/"))
      {
        System.Diagnostics.Process.Start(url);
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
        sb.Append(nt.Geizhals);
        sb.Append("\"]");
        sb.Append(nt.Name);
        sb.Append("[/URL]\n");
      }
      System.Windows.Forms.Clipboard.SetDataObject(sb.ToString(), true);
    }
  }
}
