using PSU_Calculator.DataWorker;
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
    static string Einstellungen = "https://github.com/Multithread/PSU_Calculator/blob/master/Einstellungen";
    static string PSUlist = "https://github.com/Multithread/PSU_Calculator/blob/master/Netzteile";
    static string GPUList = "https://github.com/Multithread/PSU_Calculator/blob/master/GPUs";
    static string CPUList = "https://github.com/Multithread/PSU_Calculator/blob/master/CPUs";
    Thread downloader;
    public Updater()
    {
      downloader = new Thread(new ThreadStart(run));
      downloader.IsBackground = true;
      downloader.Start();
    }

    private void run()
    {
      string data = DownloadFromSource(Einstellungen);
      ComponentStringSplitter css = new ComponentStringSplitter(data, true);
    }

    public string DownloadFromSource(string url)
    {
      WebClient c = new WebClient();
      LoaderModul m = LoaderModul.getInstance();
      string data = c.DownloadString(url);
      return data;
    }
  }
}
