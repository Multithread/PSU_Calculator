using PSU_Calculator.DataWorker;
using PSU_Calculator.Dateizugriffe;
using PSU_Calculator.Komponenten;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace PSU_Calculator
{
  public class Updater
  {
    private static string Einstellungen = "https://raw.githubusercontent.com/Multithread/PSU_Calculator/master/Einstellungen.data";
     Thread downloader;

    public delegate void UpdateFinishedDelegate(Updater sender);
    public event UpdateFinishedDelegate UpdateFinishedEvent;

    private Dictionary<string, string> updateFileDict = new Dictionary<string, string>();
    private Dictionary<string, double> versionFileDict = new Dictionary<string, double>();

    public Dictionary<string, string> FilesToUpdate
    {
      get { return updateFileDict; }
      set { updateFileDict = value; }
    }

    public Dictionary<string, double> FileVersions
    {
      get { return versionFileDict; }
      set { versionFileDict = value; }
    }

    public bool IsUpdating
    {
      get;
      set;
    }

    public Updater()
    {
      IsUpdating = false;
    }

    public void RunUpdateAsync()
    {
      if (downloader == null)
      {
        downloader = new Thread(new ThreadStart(run));
        downloader.IsBackground = true;
        downloader.Start();
      }
    }

    public void RunUpdateSyncroniced()
    {
      run();
    }

    /// <summary>
    /// ev. download in eigene Fuinktion mit 2 Parametern.
    /// </summary>
    private void run()
    {
      string data = DownloadFromSource(GetSettingsDownloadPath());
      ComponentStringSplitter css = new ComponentStringSplitter(data, true);
      foreach (string key in updateFileDict.Keys)
      {
        //EInstellungen Key Ignorieren, den haben wir bereits
        if (PSUCalculatorSettings.Einstellungen.Equals(key))
        {
          continue;
        }

        //Version auslesen
        double version;
        versionFileDict.TryGetValue(key, out version);
        if (CalculatorSettingsFile.Get().GetVersionForFile(key) < css.GetValueForKeyAsDouble(key))
        {
          //Herunterladen der Daten.
          string url;
          updateFileDict.TryGetValue(key, out url);
          IsUpdating = true;
          data = DownloadFromSource(url);
          if (!string.IsNullOrEmpty(data))
          {
            string path = PSUCalculatorSettings.GetFilePath(key);
            StorageMapper.WriteToFilesystem(path, data);
            CalculatorSettingsFile.Get().SetVersionForFile(key, css.GetValueForKeyAsDouble(key));
            LoaderModul.getInstance().ReloadPowerSupplys();
          }
          IsUpdating = false;
        }
      }
      //if (PSUCalculatorSettings.Get().PSUVersion < css.GetValueForKeyAsDouble(PSUCalculatorSettings.PowerSupply))
      //{
      //  IsUpdating = true;
      //  data = DownloadFromSource(PSUlist);
      //  if (!string.IsNullOrEmpty(data))
      //  {
      //    string path = PSUCalculatorSettings.GetFilePath(PSUCalculatorSettings.PowerSupply);
      //    StorageMapper.WriteToFilesystem(path, data);
      //    PSUCalculatorSettings.Get().PSUVersion = css.GetValueForKeyAsDouble(PSUCalculatorSettings.PowerSupply);
      //    LoaderModul.getInstance().ReloadPowerSupplys();
      //  }
      //  IsUpdating = false;
      //}
      //if (PSUCalculatorSettings.Get().CPUVersion < css.GetValueForKeyAsDouble(PSUCalculatorSettings.CPU))
      //{
      //  IsUpdating = true;
      //  data = DownloadFromSource(CPUList);
      //  if (!string.IsNullOrEmpty(data))
      //  {
      //    string path = PSUCalculatorSettings.GetFilePath(PSUCalculatorSettings.CPU);
      //    StorageMapper.WriteToFilesystem(path, data);
      //    PSUCalculatorSettings.Get().CPUVersion = css.GetValueForKeyAsDouble(PSUCalculatorSettings.CPU);
      //  }
      //  IsUpdating = false;
      //}
      //if (PSUCalculatorSettings.Get().GPUVersion < css.GetValueForKeyAsDouble(PSUCalculatorSettings.GPU))
      //{
      //  IsUpdating = true;
      //  data = DownloadFromSource(GPUList);
      //  if (!string.IsNullOrEmpty(data))
      //  {
      //    string path = PSUCalculatorSettings.GetFilePath(PSUCalculatorSettings.GPU);
      //    StorageMapper.WriteToFilesystem(path, data);
      //    PSUCalculatorSettings.Get().GPUVersion = css.GetValueForKeyAsDouble(PSUCalculatorSettings.GPU);
      //  }
      //  IsUpdating = false;
      //}
      CalculatorSettingsFile.Get().SaveSettings();
      if (UpdateFinishedEvent != null)
      {
        UpdateFinishedEvent(this);
      }
    }

    public string GetSettingsDownloadPath()
    {
      string tmpPath;
      if (!updateFileDict.TryGetValue(PSUCalculatorSettings.Einstellungen, out tmpPath))
      {
        tmpPath = Einstellungen;
      }
      return tmpPath.Trim();
    }

    public string DownloadFromSource(string url)
    {
      WebClient c = new WebClient();
      LoaderModul m = LoaderModul.getInstance();
      string data = "";
      try
      {
        data= c.DownloadString(url);
      }
      catch (Exception)
      {

      }
      return data;
    }
  }
}
