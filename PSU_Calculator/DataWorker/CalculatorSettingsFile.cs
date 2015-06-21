using PSU_Calculator.Dateizugriffe;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace PSU_Calculator.DataWorker
{
  public class CalculatorSettingsFile
  {
    #region singelton
    private static CalculatorSettingsFile instance = null;
    private static string BoolValues = "Booleans";

    public static CalculatorSettingsFile Get()
    {
      if (instance == null)
      {
        instance = new CalculatorSettingsFile();
        instance.Load();
      }
      return instance;
    }

    private CalculatorSettingsFile()
    {
      updater = new Updater();
    }
    #endregion

    private Element settings = null;
    private Dictionary<string, string> updateFileDict = new Dictionary<string, string>();
    private Dictionary<string, double> versionFileDict = new Dictionary<string, double>();
    private Updater updater;
    private bool hasChanged = false;

    public bool HasChanged
    {
      get
      {
        return hasChanged;
      }
      set
      {
        hasChanged = value;
      }
    }

    public void Load()
    {
      settings = new Element("");
      if (!FileExists(PSUCalculatorSettings.Einstellungen))
      {
        new Updater().RunUpdateSyncroniced();
      }
      settings = StorageMapper.GetXML(PSUCalculatorSettings.GetFilePath(PSUCalculatorSettings.Einstellungen));
      
      //Laden der Versionen und DownloadLinks
      foreach (Element ele in settings.getAlleElementeByName("File"))
      {
        string name = ele.getAttribut("Name");
        AddDownloadableFile(name, ele.Text.Trim());
        string version = settings.getElementByPfadOnCreate(PSUCalculatorSettings.Version).getElementByPfadOnCreate(name).getAttribut(PSUCalculatorSettings.Version);
        double ver;
        if (double.TryParse(version, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out ver))
        {
          versionFileDict.Add(name, ver);
        }
      }
      if (settings.getElementByName(PSUCalculatorSettings.Einstellungen)!=null)
      {
        if (!string.IsNullOrWhiteSpace(settings.getElementByName(PSUCalculatorSettings.Einstellungen).Text))
        {
          AddDownloadableFile(PSUCalculatorSettings.Einstellungen, settings.getElementByName(PSUCalculatorSettings.Einstellungen).Text.Trim());
        }
      }
      updater.FileVersions = versionFileDict;
      updater.FilesToUpdate = updateFileDict;

    }

    /// <summary>
    /// gibt einen Boolschen wert aus dem Settings Element zurück.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool GetBoolValue(string key)
    {
      Element ele = Settings.getElementByPfadOnCreate(BoolValues);
      Boolean output = false;
      if (Boolean.TryParse(ele.getAttribut(key), out output))
      {
        return output;
      }
      return true;
    }

    /// <summary>
    /// Setzt einen Boolschen Wert in das Setting Element
    /// </summary>
    /// <param name="key">Name des Boolschen wertes</param>
    /// <param name="value">Wert</param>
    public void SetBoolValue(string key, bool value)
    {
      Element ele = Settings.getElementByPfadOnCreate(BoolValues);
      ele.addAttribut(key, value.ToString());
      OverrideSetting(BoolValues, ele);
    }

    public void OverrideSetting(string key, Element newElement)
    {
      Settings.removeElement(Settings.getElementByName(key));
      Settings.addElement(newElement);
      hasChanged = true;
    }

    public void SaveSettings()
    {
      if (hasChanged)
      {
        StorageMapper.WriteToFilesystem(PSUCalculatorSettings.GetFilePath(PSUCalculatorSettings.Einstellungen), Settings.getXML());
        hasChanged = false;
      }
    }

    public double GetVersionForFile(string filename) 
    {
      double outvalue;
      string value= settings.getElementByPfadOnCreate(PSUCalculatorSettings.Version).
        getElementByPfadOnCreate(filename).
        getAttribut(PSUCalculatorSettings.Version);
      if (Double.TryParse(value, out outvalue))
      {
        return outvalue;
      }
      return 0.0d;
    }

    public void SetVersionForFile(string filename, double version)
    {
      settings.getElementByPfadOnCreate(PSUCalculatorSettings.Version).
        getElementByPfadOnCreate(filename).
        addAttribut(PSUCalculatorSettings.Version, 
        version.ToString(CultureInfo.InvariantCulture));
      hasChanged=true;
    }

    public void RunUpdater()
    {
      updater.RunUpdateAsync();
    }

    public Element Settings
    {
      get
      {
        return settings;
      }
    }

    public void AddDownloadableFile(string filename, string url)
    {
      if (string.IsNullOrEmpty(filename) || string.IsNullOrEmpty(url))
      {
        return;
      }
      if (updateFileDict.ContainsKey(filename))
      {
        updateFileDict.Remove(filename);
      }
      updateFileDict.Add(filename, url);
      hasChanged = true;
    }

    public bool IsValid()
    {
      if (settings == null)
      {
        return false;
      }
      if (!settings.hasElement)
      {
        return false;
      }
      foreach (Element ele in settings.getAlleElementeByName("File"))
      {
        if (string.IsNullOrEmpty(ele.getAttribut("Name")))
        {
          return false;
        }
        if (!FileExists(ele.getAttribut("Name")))
        {
          return false;
        }
      }
      return true;
    }

    private bool FileExists(string filename)
    {
      if (!StorageMapper.Existiert(PSUCalculatorSettings.Get().DirectoryPath))
      {
        return false;
      }
      if (!StorageMapper.Existiert(PSUCalculatorSettings.GetFilePath(filename)))
      {
        return false;
      }
      return true;
    }
  }
}
