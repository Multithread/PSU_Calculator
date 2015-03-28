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
    static string Einstellungen = "https://raw.githubusercontent.com/Multithread/PSU_Calculator/master/Einstellungen.data";
    static string PSUlist = "https://raw.githubusercontent.com/Multithread/PSU_Calculator/master/Netzteile.data";
    static string GPUList = "https://raw.githubusercontent.com/Multithread/PSU_Calculator/master/GPUs.data";
    static string CPUList = "https://raw.githubusercontent.com/Multithread/PSU_Calculator/master/CPUs.data";
    Thread downloader;
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
      string data = DownloadFromSource(Einstellungen);
      ComponentStringSplitter css = new ComponentStringSplitter(data, true);
      if (PSUCalculatorSettings.Get().PSUVersion < css.GetValueForKeyAsDouble(PSUCalculatorSettings.PowerSupply))
      {
        IsUpdating = true;
        data = DownloadFromSource(PSUlist);
        if (!string.IsNullOrEmpty(data))
        {
          string path = PSUCalculatorSettings.GetFilePath(PSUCalculatorSettings.PowerSupply);
          StorageMapper.WriteToFilesystem(path, data);
          PSUCalculatorSettings.Get().OverrideSetting(PSUCalculatorSettings.PowerSupply, css.GetValueForKeyAsDouble(PSUCalculatorSettings.PowerSupply).ToString());
        }
        IsUpdating = false;
      }
      if (PSUCalculatorSettings.Get().CPUVersion < css.GetValueForKeyAsDouble(PSUCalculatorSettings.CPU))
      {
        IsUpdating = true;
        data = DownloadFromSource(CPUList);
        if (!string.IsNullOrEmpty(data))
        {
          string path = PSUCalculatorSettings.GetFilePath(PSUCalculatorSettings.CPU);
          StorageMapper.WriteToFilesystem(path, data);
          PSUCalculatorSettings.Get().OverrideSetting(PSUCalculatorSettings.CPU, css.GetValueForKeyAsDouble(PSUCalculatorSettings.CPU).ToString());
        }
        IsUpdating = false;
      }
      if (PSUCalculatorSettings.Get().GPUVersion < css.GetValueForKeyAsDouble(PSUCalculatorSettings.GPU))
      {
        IsUpdating = true;
        data = DownloadFromSource(GPUList);
        if (!string.IsNullOrEmpty(data))
        {
          string path = PSUCalculatorSettings.GetFilePath(PSUCalculatorSettings.GPU);
          StorageMapper.WriteToFilesystem(path, data);
          PSUCalculatorSettings.Get().OverrideSetting(PSUCalculatorSettings.GPU, css.GetValueForKeyAsDouble(PSUCalculatorSettings.GPU).ToString());
        }
        IsUpdating = false;
      }
      PSUCalculatorSettings.Get().SaveSettings();
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
      catch (Exception ex)
      {

      }
      return data;
    }
  }
}
