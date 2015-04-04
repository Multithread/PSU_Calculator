using PSU_Calculator.Dateizugriffe;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PSU_Calculator.DataWorker
{
  public class PSUCalculatorSettings
  {
    private static PSUCalculatorSettings instance = null;
    public static string Version = "Version";
    public static string CPU = "CPUs";
    public static string GPU = "GPUs";
    public static string PowerSupply = "Netzteile";
    public static string MySystem = "MySystem";
    public static string ChoosenComponents = "Components";
    public static string Einstellungen = "Einstellungen";
    public static string DataPath = "PSU_Calculator_Data";
    public static string SearchEngineString = "Suchmaschine";
    public static string DefaultSearchEngine = "de";

    private string searchEngineString = "";

    public double GPUVersion
    {
      get
      {
        return GetAsDouble(GetSetting("Version." + GPU).getAttribut("Version"));
      }
      set
      {
        GetSetting("Version." + GPU).addAttribut("Version", value.ToString());
      }
    }

    public double CPUVersion
    {
      get
      {
        return GetAsDouble(GetSetting("Version." + CPU).getAttribut("Version"));
      }
      set
      {
        GetSetting("Version." + CPU).addAttribut("Version", value.ToString());
      }
    }

    public double PSUVersion
    {
      get
      {
        return GetAsDouble(GetSetting("Version." + PowerSupply).getAttribut("Version"));
      }
      set
      {
        GetSetting("Version." + PowerSupply).addAttribut("Version", value.ToString());
      }
    }

    Dictionary<string, Element> ElementDict = new Dictionary<string, Element>();
    private bool hasChanged = false;
    private Element Settings = null;

    public StringJoiner Daten;

    public static PSUCalculatorSettings Get()
    {
      if (instance == null)
      {
        instance = new PSUCalculatorSettings();
        instance.Load();
      }
      return instance;
    }

    private PSUCalculatorSettings()
    {
      ShowPriceComparer = true;
    }

    private void Load()
    {
      Settings = StorageMapper.GetXML(GetXmlFilePath(Einstellungen));
      Element version;
      if (Settings.getAllEntries().Count == 0)
      {
        Settings.Name = "Settings";
        version = new Element("Version");
        Settings.addElement(version);
        Daten = new StringJoiner(StorageMapper.ReadFromFilesystem(GetFilePath(Einstellungen)), true);

        //Zuerst die Elemente Hinzufügen, erst danach abfragen auf die einzelnen Versionen machen
        version.addElement(Element.New(GPU).addAttribut("Version", Daten.GetValueForKeyAsDouble(GPU).ToString()));
        version.addElement(Element.New(CPU).addAttribut("Version", Daten.GetValueForKeyAsDouble(CPU).ToString()));
        version.addElement(Element.New(PowerSupply).addAttribut("Version", Daten.GetValueForKeyAsDouble(PowerSupply).ToString()));

        Settings.addElement(new Element(SearchEngineString, Daten.GetValueForKey(SearchEngineString)));
        hasChanged = true;
      }
      version = Settings.getElementByName("Version");
      //ElementDict abfüllen.
      ElementDict.Add(GPU, version.getElementByName(GPU));
      ElementDict.Add(CPU, version.getElementByName(CPU));
      ElementDict.Add(PowerSupply, version.getElementByName(PowerSupply));
      ElementDict.Add(SearchEngineString, Settings.getElementByName(SearchEngineString));
      SetSearchEngine(Settings.getElementByName(SearchEngineString).Text);
    }

    private double GetAsDouble(string data)
    {
      double output;
      if (double.TryParse(data, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out output))
      {
        return output;
      }
      return 0.0d;
    }

    public Element GetSetting(string settingName)
    {
      return Settings.getElementByPfadOnCreate(settingName);
    }

    public void SetSearchEngine(string engine)
    {
      OverrideSetting(PSUCalculatorSettings.SearchEngineString, engine);
      string oldEngine = SearchEngine;
      //Daten.Put(SearchEngineString, engine);
      SearchEngine = engine;
      if (oldEngine != null && !oldEngine.Equals(SearchEngine))
      {
        hasChanged = true;
      }
    }

    public void OverrideSetting(string key, string value)
    {
      hasChanged = true;
      Element ele;
      if (!ElementDict.TryGetValue(key, out ele))
      {
        ele = new Element(key);
        Settings.addElement(ele);
        ElementDict.Add(key, ele);
      }
      ele.Text = value;
      //Daten.Put(key, value);
    }

    public void SaveSettings()
    {
      if (hasChanged)
      {
        StorageMapper.WriteToFilesystem(GetXmlFilePath(Einstellungen), Settings.getXML());
        hasChanged = false;
      }
    }

    public bool HasChanged
    {
      get
      {
        return hasChanged;
      }
    }

    public bool ShowPriceComparer
    {
      get;
      set;
    }

    public static string GetFilePath(string filename)
    {
      return GetPath(filename, "data");
    }

    public static string GetXmlFilePath(string filename)
    {
      return GetPath(filename, "xml");
    }

    private static string GetPath(string filename, string ending)
    {
      return string.Format("{0}{1}.{2}", AbsolutDataPath, filename, ending);
    }

    public string SearchEngine
    {
      get
      {
        return searchEngineString;
      }
      private set
      {
        searchEngineString = value;
      }
    }

    private static string AbsolutDataPath
    {
      get
      {
        return string.Format("{0}{2}{1}{2}", Application.StartupPath, PSUCalculatorSettings.DataPath, Path.DirectorySeparatorChar);
      }
    }

    //Pfade im Dateisystem
    public static string CPUPath
    {
      get
      {
        return string.Format("{0}{3}{1}{3}{2}.txt", Application.StartupPath, PSUCalculatorSettings.DataPath, PSUCalculatorSettings.CPU, Path.DirectorySeparatorChar);
      }
    }
    public static string GPUPath
    {
      get
      {
        return string.Format("{0}{3}{1}{3}{2}.txt", Application.StartupPath, PSUCalculatorSettings.DataPath, PSUCalculatorSettings.GPU, Path.DirectorySeparatorChar);
      }
    }
   
    public static string DirectoryPath
    {
      get
      {
        return string.Format("{0}{2}{1}", Application.StartupPath, PSUCalculatorSettings.DataPath, Path.DirectorySeparatorChar);
      }
    }

    public override string ToString()
    {
      return Daten.ToString();
    }
  }
}
