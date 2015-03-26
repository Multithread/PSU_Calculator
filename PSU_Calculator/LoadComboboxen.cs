using PSU_Calculator.DataWorker;
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

    private List<PcComponent> gpuComponentList;
    private List<PcComponent> cpuComponentList;
    private List<PowerSupply> powersupplyList;
    private string getAssemblyText(string _name)
    {
      string back = "";
      try
      {
        Assembly ass = Assembly.GetExecutingAssembly();
        StreamReader _textStreamReader = new StreamReader(ass.GetManifestResourceStream("PSU_Calculator.Resources." + _name));
        back = _textStreamReader.ReadToEnd();
      }
      catch (Exception)
      {
        return "";
      }
      return back;
    }

    public List<PcComponent> GPU
    {
      get
      {
        return gpuComponentList;
      }
    }

    public List<PcComponent> CPU
    {
      get
      {
        return cpuComponentList;
      }
    }

    public List<PcComponent> Netzteile
    {
      get
      {
        return new List<PcComponent>(powersupplyList);
      }
    }

    /// <summary>
    /// Cooling solutions laden
    /// </summary>
    /// <param name="_box"></param>
    public void LoadCoolingSolutions(ComboBox _box)
    {
      _box.Items.Clear();
      _box.Items.AddRange(new object[] {
            new CoolingSolution("Luftkühlung (1-Fan)", 5, false,CoolingSolution.CoolingTyp.Air),
            new CoolingSolution("Luftkühlung (2-Fan)", 10, false,CoolingSolution.CoolingTyp.Air),
            new CoolingSolution("AiO WaKü", 10, false, CoolingSolution.CoolingTyp.Water),
            new CoolingSolution("WaKü", 10, true, CoolingSolution.CoolingTyp.Water, true),
            new CoolingSolution("LN2", 0, true, CoolingSolution.CoolingTyp.LN2, true)});
    }

    /// <summary>
    /// OC möglichkeiten laden
    /// </summary>
    /// <param name="_box"></param>
    public void LoadOCVariations(ComboBox _box)
    {
      _box.Items.Clear();
      _box.Items.AddRange(new object[] {
            new OC("Kein OC", false, false),
            new OC("Ja nur CPU", true, false),
            new OC("Ja nur GPU", false, true),
            new OC("GPU und CPU", true, true)});
    }

    /// <summary>
    /// GPU's in eine combobox laden, allerdings nur solche von NVidia, ev. speziellen Parameter der sowas abdeckt machen.
    /// </summary>
    /// <param name="_box"></param>
    public void LoadPhysXKarten(ComboBox _box)
    {
      List<PcComponent> physx = new List<PcComponent>();
      foreach (PcComponent gpu in GetGPUComponents())
      {
        if (string.IsNullOrEmpty(gpu.Name) || gpu.Name.Contains("Nvidia"))
        {
          physx.Add(gpu);
        }
      }
      _box.AutoCompleteSource = AutoCompleteSource.ListItems;
      _box.Items.Clear();
      _box.Items.AddRange(physx.ToArray());
    }

    public void setPhysXSearchEvent(ComboBox _box)
    {
      _box.KeyUp += (sender, args) =>
      {
        //Nichts machen wenn das Zeichen kein Zeichen oder kein Buchstabe ist
        if (!char.IsLetterOrDigit((char)args.KeyValue) && !Keys.Back.Equals(args.KeyCode) && !Keys.Delete.Equals(args.KeyCode) && !Keys.NumPad0.Equals(args.KeyCode))
        {
          return;
        }

        _box.Items.Clear();
        IEnumerable<PcComponent> suggestions = string.IsNullOrWhiteSpace(_box.Text) ? gpuComponentList : gpuComponentList.Where(el => el.Name.Contains("Nvidia") && el.Name.ToLower().Contains(_box.Text.ToLower()));
        if (suggestions.Count() == 0)
        {
          suggestions = gpuComponentList;
        }

        if (suggestions.Count() == 1)
        {
          _box.Items.AddRange(gpuComponentList.ToArray());
          _box.SelectedItem = suggestions.First();
          _box.SelectAll();
        }
        else
        {
          _box.Items.AddRange(suggestions.ToArray());
          _box.Select(_box.Text.Length, 0);
          _box.SelectedItem = null;
          //liste.DroppedDown = true;
        }
      };
    }



    //Range hinzufügen zu einer Liste von PC Komponenten, sofern möglich akurat
    private void addRangeAt(List<PcComponent> _listOfInserts, List<PcComponent> _mainList)
    {
      bool beforebestmatch, tempbeforematch;
      int bestmatch, matchcount, tempcount;
      foreach (PcComponent komp in _listOfInserts)
      {
        bestmatch = -1;
        matchcount = 0;
        beforebestmatch = false;
        for (int i = 0; i < _mainList.Count; i++)
        {
          if (komp.Name.Equals(_mainList[i].Name))
          {
            bestmatch = -2;
            break;
          }
          tempbeforematch = true;
          tempcount = 0;
          //zeichen für zeichen vergleich für best match findung
          for (int m = 0; m < komp.Name.Length; m++)
          {
            if (_mainList[i].Name.Length <= m)
            {
              tempbeforematch = false;
              break;
            }
            int temp = 0;
            temp = _mainList[i].Name.Substring(m, 1).CompareTo(komp.Name.Substring(m, 1));

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
          _mainList.Add(komp);
        }
        else if (bestmatch == -2)
        {
        }
        else
        {
          if (beforebestmatch)
          {
            _mainList.Insert(bestmatch, komp);
          }
          else
          {
            _mainList.Insert(bestmatch + 1, komp);
          }
        }
      }
    }

    /// <summary>
    /// Hinzufügen von weiteren elementen zur GPU Liste, Sync, um reienfolge sicherzustellen
    /// Weitere Elemente sind solche die aus dem Web oder lokal geladen wurden
    /// </summary>
    /// <param name="_rangeToInsert"></param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void AddGPURange(List<PcComponent> _rangeToInsert)
    {
      addRangeAt(_rangeToInsert, gpuComponentList);
    }

    /// <summary>
    /// Hinzufügen von weiteren elementen zur CPU Liste, Sync, um reienfolge sicherzustellen
    /// Weitere Elemente sind solche die aus dem Web oder lokal geladen wurden
    /// </summary>
    /// <param name="_rangeToInsert"></param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void AddCPURange(List<PcComponent> _rangeToInsert)
    {
      addRangeAt(_rangeToInsert, cpuComponentList);
    }

    public void AddNetzteilRange(List<PowerSupply> _rangeToInsert)
    {
      if (powersupplyList == null)
      {
        powersupplyList = GetPowerSupplys();
      }
      powersupplyList.AddRange(_rangeToInsert);
      powersupplyList.Sort(delegate(PowerSupply n1, PowerSupply n2)
      {
        return n1.Quality.CompareTo(n2.Quality) * -1;
      });
    }

    /// <summary>
    /// GPU's in eine combobox laden
    /// Setztern dews Keyxup events für suche
    /// </summary>
    /// <param name="_box"></param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void LoadGPU(ComboBox _box)
    {
      _box.AutoCompleteSource = AutoCompleteSource.ListItems;
      _box.Items.Clear();
      _box.Items.AddRange(GetGPUComponents().ToArray());
    }

    public void setGPUSearchEvent(ComboBox _box)
    {
      _box.KeyUp += (sender, args) =>
      {
        //Nichts machen wenn das Zeichen kein Zeichen oder kein Buchstabe ist
        if (!char.IsLetterOrDigit((char)args.KeyValue) && !Keys.Back.Equals(args.KeyCode) && !Keys.Delete.Equals(args.KeyCode) && !Keys.NumPad0.Equals(args.KeyCode))
        {
          return;
        }

        _box.Items.Clear();
        IEnumerable<PcComponent> suggestions = string.IsNullOrWhiteSpace(_box.Text) ? gpuComponentList : gpuComponentList.Where(el => el.Name.ToLower().Contains(_box.Text.ToLower()));
        if (suggestions.Count() == 0)
        {
          suggestions = gpuComponentList;
        }

        if (suggestions.Count() == 1)
        {
          _box.Items.AddRange(gpuComponentList.ToArray());
          _box.SelectedItem = suggestions.First();
          _box.SelectAll();
        }
        else
        {
          _box.Items.AddRange(suggestions.ToArray());
          _box.Select(_box.Text.Length, 0);
          _box.SelectedItem = null;
          //liste.DroppedDown = true;
        }
      };
    }

    /// <summary>
    /// CPU's in eine combobox laden
    /// Setztern dews Keyxup events für suche
    /// </summary>
    /// <param name="_box"></param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void LoadCPU(ComboBox _box)
    {
      _box.AutoCompleteSource = AutoCompleteSource.ListItems;
      _box.Items.Clear();
      _box.Items.AddRange(GetCPUComponents().ToArray());
    }

    public void setCPUSearchEvent(ComboBox _box)
    {
      _box.KeyUp += (sender, args) =>
      {
        //Nichts machen wenn das Zeichen kein Zeichen oder kein Buchstabe ist
        if (!char.IsLetterOrDigit((char)args.KeyValue) && !Keys.Back.Equals(args.KeyCode) && !Keys.Delete.Equals(args.KeyCode) && !Keys.NumPad0.Equals(args.KeyCode))
        {
          return;
        }

        _box.Items.Clear();
        IEnumerable<PcComponent> suggestions = string.IsNullOrWhiteSpace(_box.Text) ? cpuComponentList : cpuComponentList.Where(el => el.Name.ToLower().Contains(_box.Text.ToLower()));
        if (suggestions.ToArray().Length == 0)
        {
          suggestions = cpuComponentList;
        }

        if (suggestions.Count() == 1)
        {
          _box.Items.AddRange(cpuComponentList.ToArray());
          _box.SelectedItem = suggestions.First();
          _box.SelectAll();
        }
        else
        {
          _box.Items.AddRange(suggestions.ToArray());
          _box.Select(_box.Text.Length, 0);
          _box.SelectedItem = null;
          //liste.DroppedDown = true;
        }
      };
    }

    /// <summary>
    /// Get Komponenten aufgrund einer Zeilenliste
    /// </summary>
    /// <param name="_rows"></param>
    /// <returns></returns>
    public List<PcComponent> GetComponents(string[] _rows)
    {
      List<PcComponent> componentList = new List<PcComponent>();
      string[] columns;
      int tdp, benchmark;
      foreach (string row in _rows)
      {
        columns = row.Split(':');
        switch (columns.Length)
        {
          case 0:
          case 1:
            componentList.Add(new PcComponent("", 0, 0));
            break;
          case 2:
            int.TryParse(columns[1], out tdp);
            componentList.Add(new PcComponent(columns[0], tdp, tdp));
            break;
          case 3:
            int.TryParse(columns[1], out tdp);
            int.TryParse(columns[2], out benchmark);
            componentList.Add(new PcComponent(columns[0], tdp, benchmark));
            break;
        }
      }
      return componentList;
    }

    /// <summary>
    /// Laden der verschiedenen CPU's
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    private List<PcComponent> GetCPUComponents()
    {
      if (cpuComponentList == null)
      {
        string[] zeilen = getAssemblyText("CPUs.txt").Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

        cpuComponentList = GetComponents(zeilen);
      }
      return cpuComponentList;
    }

    /// <summary>
    /// Laden der verschiedenen GPU's
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    private List<PcComponent> GetGPUComponents()
    {
      if (gpuComponentList == null)
      {
        string[] zeilen = getAssemblyText("GPUs.txt").Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

        gpuComponentList = GetComponents(zeilen);
      }
      return gpuComponentList;
    }

    /// <summary>
    /// Laden der verschiedenen Netzteile
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public List<PowerSupply> GetPowerSupplys()
    {
      if (powersupplyList == null)
      {
        string[] rows = getAssemblyText("Netzteile.txt").Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

        powersupplyList = GetPowerSupplysFromArray(rows);
      }
      return powersupplyList;
    }

    /// <summary>
    /// Netzteile aus einem Zeilen array generieren
    /// </summary>
    /// <param name="_rows"></param>
    /// <returns></returns>
    public List<PowerSupply> GetPowerSupplysFromArray(string[] _rows)
    {
      List<PowerSupply> parts = new List<PowerSupply>();
      string[] columns;
      int tdp, power, quali;
      foreach (string row in _rows)
      {
        columns = row.Split(';');
        switch (columns.Length)
        {
          case 0:
          case 1:
            parts.Add(new PowerSupply("", 0, 0, ""));
            break;
          case 2:
            int.TryParse(columns[1], out tdp);
            parts.Add(new PowerSupply(columns[0], tdp, tdp, ""));
            break;
          case 3:
            int.TryParse(columns[1], out tdp);
            parts.Add(new PowerSupply(columns[0], tdp, tdp, columns[2]));
            break;
          case 4:
            int.TryParse(columns[1], out tdp);
            int.TryParse(columns[2], out power);
            parts.Add(new PowerSupply(columns[0], power, tdp, columns[3]));
            break;
          case 5:
            int.TryParse(columns[1], out tdp);
            int.TryParse(columns[2], out power);
            int.TryParse(columns[3], out quali);
            parts.Add(new PowerSupply(columns[0], power, tdp, columns[4], quali));
            break;
          default:
            parts.Add(new PowerSupply(new ComponentStringSplitter(row, true)));
            break;
        }
      }
      parts.Sort(delegate(PowerSupply n1, PowerSupply n2)
      {
        return n1.Quality.CompareTo(n2.Quality) * -1;
      });
      return parts;
    }
  }
}
