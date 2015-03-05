using PSU_Calculator.Komponenten;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PSU_Calculator
{
  public class LoaderModul
  {
    private static LoaderModul instance = null;
    public static LoaderModul getInstance()
    {
      if (instance == null)
      {
        instance = new LoaderModul();
      } return instance;
    }
    private LoaderModul()
    {

    }

    private List<PcKomponente> gpuKomponenten;
    private List<PcKomponente> cpuKomponenten;
    private List<Netzteil> netzteile;
    private string getAssemblyText(string name)
    {
      string back = "";
      try
      {
        Assembly ass = Assembly.GetExecutingAssembly();
        StreamReader _textStreamReader = new StreamReader(ass.GetManifestResourceStream("PSU_Calculator.Resources." + name));
        back = _textStreamReader.ReadToEnd();
      }
      catch (Exception)
      {
        return "";
      }
      return back;
    }

    public List<PcKomponente> GPU
    {
      get
      {
        return gpuKomponenten;
      }
    }

    public List<PcKomponente> CPU
    {
      get
      {
        return cpuKomponenten;
      }
    }

    public List<PcKomponente> Netzteile
    {
      get
      {
        return new List<PcKomponente>(netzteile);
      }
    }

    /// <summary>
    /// Cooling solutions laden
    /// </summary>
    /// <param name="liste"></param>
    public void LoadCoolingSolutions(ComboBox liste)
    {
      liste.Items.Clear();
      liste.Items.AddRange(new object[] {
            new Kuehlung("Luftkühlung (1-Fan)", 5, false,Kuehlung.CoolingTyp.Luft),
            new Kuehlung("Luftkühlung (2-Fan)", 10, false,Kuehlung.CoolingTyp.Luft),
            new Kuehlung("AiO WaKü", 10, false, Kuehlung.CoolingTyp.Wasser),
            new Kuehlung("WaKü", 10, true, Kuehlung.CoolingTyp.Wasser, true),
            new Kuehlung("LN2", 0, true, Kuehlung.CoolingTyp.LN2, true)});
    }

    /// <summary>
    /// OC möglichkeiten laden
    /// </summary>
    /// <param name="liste"></param>
    public void LoadOCVariations(ComboBox liste)
    {
      liste.Items.Clear();
      liste.Items.AddRange(new object[] {
            new OC("Kein OC", false, false),
            new OC("Ja nur CPU", true, false),
            new OC("Ja nur GPU", false, true),
            new OC("GPU und CPU", true, true)});
    }

    /// <summary>
    /// GPU's in eine combobox laden
    /// </summary>
    /// <param name="liste"></param>
    public void LoadPhysXKarten(ComboBox liste)
    {
      List<PcKomponente> physx = new List<PcKomponente>();
      foreach (PcKomponente gpu in getGPUKomponenten())
      {
        if (string.IsNullOrEmpty(gpu.Bezeichnung) || gpu.Bezeichnung.Contains("Nvidia"))
        {
          physx.Add(gpu);
        }
      }
      liste.AutoCompleteSource = AutoCompleteSource.ListItems;
      liste.Items.Clear();
      liste.Items.AddRange(physx.ToArray());
    }

    public void setPhysXSearchEvent(ComboBox liste)
    {
      liste.KeyUp += (sender, args) =>
      {
        //Nichts machen wenn das Zeichen kein Zeichen oder kein Buchstabe ist
        if (!char.IsLetterOrDigit((char)args.KeyValue) && !Keys.Back.Equals(args.KeyCode) && !Keys.Delete.Equals(args.KeyCode) && !Keys.NumPad0.Equals(args.KeyCode))
        {
          return;
        }

        liste.Items.Clear();
        IEnumerable<PcKomponente> suggestions = string.IsNullOrWhiteSpace(liste.Text) ? gpuKomponenten : gpuKomponenten.Where(el => el.Bezeichnung.Contains("Nvidia") && el.Bezeichnung.ToLower().Contains(liste.Text.ToLower()));
        if (suggestions.Count() == 0)
        {
          suggestions = gpuKomponenten;
        }

        if (suggestions.Count() == 1)
        {
          liste.Items.AddRange(gpuKomponenten.ToArray());
          liste.SelectedItem = suggestions.First();
          liste.SelectAll();
        }
        else
        {
          liste.Items.AddRange(suggestions.ToArray());
          liste.Select(liste.Text.Length, 0);
          liste.SelectedItem = null;
          //liste.DroppedDown = true;
        }
      };
    }



    //Range hinzufügen zu einer Liste von PC Komponenten, sofern möglich akurat
    private void addRangeAt(List<PcKomponente> range, List<PcKomponente> liste)
    {
      bool beforebestmatch, tempbeforematch;
      int bestmatch, matchcount, tempcount;
      foreach (PcKomponente komp in range)
      {
        bestmatch = -1;
        matchcount = 0;
        beforebestmatch = false;
        for (int i = 0; i < liste.Count; i++)
        {
          if (komp.Bezeichnung.Equals(liste[i].Bezeichnung))
          {
            bestmatch = -2;
            break;
          }
          tempbeforematch = true;
          tempcount = 0;
          //zeichen für zeichen vergleich für best match findung
          for (int m = 0; m < komp.Bezeichnung.Length; m++)
          {
            if (liste[i].Bezeichnung.Length <= m)
            {
              tempbeforematch = false;
              break;
            }
            int temp = 0;
            temp = liste[i].Bezeichnung.Substring(m, 1).CompareTo(komp.Bezeichnung.Substring(m, 1));

            if (temp == 0)
            {
              tempcount++;
            }
            else if (temp < 0)
            {
              tempbeforematch = false;
              break;
            }
            else
            {
              tempbeforematch = true;
              break;
            }

          }
          if (matchcount < tempcount)
          {
            matchcount = tempcount;
            bestmatch = i;
            beforebestmatch = tempbeforematch;
          }
        }

        //EInfügen des neuen Elementes
        if (bestmatch == -1)
        {
          liste.Add(komp);
        }
        else if (bestmatch == -2)
        {
        }
        else
        {
          if (beforebestmatch)
          {
            liste.Insert(bestmatch, komp);
          }
          else
          {
            liste.Insert(bestmatch + 1, komp);
          }
        }
      }
    }

    /// <summary>
    /// Hinzufügen von weiteren elementen zur GPU Liste, Sync, um reienfolge sicherzustellen
    /// Weitere Elemente sind solche die aus dem Web oder lokal geladen wurden
    /// </summary>
    /// <param name="range"></param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void AddGPURange(List<PcKomponente> range)
    {
      addRangeAt(range, gpuKomponenten);
    }

    /// <summary>
    /// Hinzufügen von weiteren elementen zur CPU Liste, Sync, um reienfolge sicherzustellen
    /// Weitere Elemente sind solche die aus dem Web oder lokal geladen wurden
    /// </summary>
    /// <param name="range"></param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void AddCPURange(List<PcKomponente> range)
    {
      addRangeAt(range, cpuKomponenten);
    }

    public void AddNetzteilRange(List<Netzteil> range)
    {
      if (netzteile == null)
      {
        netzteile = getNetzteile();
      }
      netzteile.AddRange(range);
      netzteile.Sort(delegate(Netzteil n1, Netzteil n2)
      {
        return n1.Qualitaet.CompareTo(n2.Qualitaet) * -1;
      });
    }

    /// <summary>
    /// GPU's in eine combobox laden
    /// Setztern dews Keyxup events für suche
    /// </summary>
    /// <param name="liste"></param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void LoadGPU(ComboBox liste)
    {
      liste.AutoCompleteSource = AutoCompleteSource.ListItems;
      liste.Items.Clear();
      liste.Items.AddRange(getGPUKomponenten().ToArray());
    }

    public void setGPUSearchEvent(ComboBox liste)
    {
      liste.KeyUp += (sender, args) =>
      {
        //Nichts machen wenn das Zeichen kein Zeichen oder kein Buchstabe ist
        if (!char.IsLetterOrDigit((char)args.KeyValue) && !Keys.Back.Equals(args.KeyCode) && !Keys.Delete.Equals(args.KeyCode) && !Keys.NumPad0.Equals(args.KeyCode))
        {
          return;
        }

        liste.Items.Clear();
        IEnumerable<PcKomponente> suggestions = string.IsNullOrWhiteSpace(liste.Text) ? gpuKomponenten : gpuKomponenten.Where(el => el.Bezeichnung.ToLower().Contains(liste.Text.ToLower()));
        if (suggestions.Count() == 0)
        {
          suggestions = gpuKomponenten;
        }

        if (suggestions.Count() == 1)
        {
          liste.Items.AddRange(gpuKomponenten.ToArray());
          liste.SelectedItem = suggestions.First();
          liste.SelectAll();
        }
        else
        {
          liste.Items.AddRange(suggestions.ToArray());
          liste.Select(liste.Text.Length, 0);
          liste.SelectedItem = null;
          //liste.DroppedDown = true;
        }
      };
    }

    /// <summary>
    /// CPU's in eine combobox laden
    /// Setztern dews Keyxup events für suche
    /// </summary>
    /// <param name="liste"></param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void LoadCPU(ComboBox liste)
    {
      liste.AutoCompleteSource = AutoCompleteSource.ListItems;
      liste.Items.Clear();
      liste.Items.AddRange(getCPUKomponenten().ToArray());
    }

    public void setCPUSearchEvent(ComboBox liste)
    {
      liste.KeyUp += (sender, args) =>
      {
        //Nichts machen wenn das Zeichen kein Zeichen oder kein Buchstabe ist
        if (!char.IsLetterOrDigit((char)args.KeyValue) && !Keys.Back.Equals(args.KeyCode) && !Keys.Delete.Equals(args.KeyCode) && !Keys.NumPad0.Equals(args.KeyCode))
        {
          return;
        }

        liste.Items.Clear();
        IEnumerable<PcKomponente> suggestions = string.IsNullOrWhiteSpace(liste.Text) ? cpuKomponenten : cpuKomponenten.Where(el => el.Bezeichnung.ToLower().Contains(liste.Text.ToLower()));
        if (suggestions.ToArray().Length == 0)
        {
          suggestions = cpuKomponenten;
        }

        if (suggestions.Count() == 1)
        {
          liste.Items.AddRange(cpuKomponenten.ToArray());
          liste.SelectedItem = suggestions.First();
          liste.SelectAll();
        }
        else
        {
          liste.Items.AddRange(suggestions.ToArray());
          liste.Select(liste.Text.Length, 0);
          liste.SelectedItem = null;
          //liste.DroppedDown = true;
        }
      };
    }

    /// <summary>
    /// Get Komponenten aufgrund einer Zeilenliste
    /// </summary>
    /// <param name="zeilen"></param>
    /// <returns></returns>
    public List<PcKomponente> getKomponenten(string[] zeilen)
    {
      List<PcKomponente> komponenten = new List<PcKomponente>();
      string[] spalten;
      int tdp, benchmark;
      foreach (string zeile in zeilen)
      {
        spalten = zeile.Split(':');
        switch (spalten.Length)
        {
          case 0:
          case 1:
            komponenten.Add(new PcKomponente("", 0, 0));
            break;
          case 2:
            int.TryParse(spalten[1], out tdp);
            komponenten.Add(new PcKomponente(spalten[0], tdp, tdp));
            break;
          case 3:
            int.TryParse(spalten[1], out tdp);
            int.TryParse(spalten[2], out benchmark);
            komponenten.Add(new PcKomponente(spalten[0], tdp, benchmark));
            break;
        }
      }
      return komponenten;
    }

    /// <summary>
    /// Laden der verschiedenen CPU's
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    private List<PcKomponente> getCPUKomponenten()
    {
      if (cpuKomponenten == null)
      {
        string[] zeilen = getAssemblyText("CPUs.txt").Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

        cpuKomponenten = getKomponenten(zeilen);
      }
      return cpuKomponenten;
    }

    /// <summary>
    /// Laden der verschiedenen GPU's
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    private List<PcKomponente> getGPUKomponenten()
    {
      if (gpuKomponenten == null)
      {
        string[] zeilen = getAssemblyText("GPUs.txt").Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

        gpuKomponenten = getKomponenten(zeilen);
      }
      return gpuKomponenten;
    }

    /// <summary>
    /// Laden der verschiedenen Netzteile
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public List<Netzteil> getNetzteile()
    {
      if (netzteile == null)
      {
        string[] zeilen = getAssemblyText("Netzteile.txt").Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

        netzteile = getNetzteileFromArray(zeilen);
      }
      return netzteile;
    }

    /// <summary>
    /// Netzteile aus einem Zeilen array generieren
    /// </summary>
    /// <param name="zeilen"></param>
    /// <returns></returns>
    public List<Netzteil> getNetzteileFromArray(string[] zeilen)
    {
      List<Netzteil> teile = new List<Netzteil>();
      string[] spalten;
      int tdp, power, quali;
      foreach (string zeile in zeilen)
      {
        spalten = zeile.Split(';');
        switch (spalten.Length)
        {
          case 0:
          case 1:
            teile.Add(new Netzteil("", 0, 0, ""));
            break;
          case 2:
            int.TryParse(spalten[1], out tdp);
            teile.Add(new Netzteil(spalten[0], tdp, tdp, ""));
            break;
          case 3:
            int.TryParse(spalten[1], out tdp);
            teile.Add(new Netzteil(spalten[0], tdp, tdp, spalten[2]));
            break;
          case 4:
            int.TryParse(spalten[1], out tdp);
            int.TryParse(spalten[2], out power);
            teile.Add(new Netzteil(spalten[0], power, tdp, spalten[3]));
            break;
          case 5:
            int.TryParse(spalten[1], out tdp);
            int.TryParse(spalten[2], out power);
            int.TryParse(spalten[3], out quali);
            teile.Add(new Netzteil(spalten[0], power, tdp, spalten[4], quali));
            break;
        }
      }
      teile.Sort(delegate(Netzteil n1, Netzteil n2)
      {
        return n1.Qualitaet.CompareTo(n2.Qualitaet) * -1;
      });
      return teile;
    }
  }
}
