using PSU_Calculator.DataWorker;
using PSU_Calculator.Dateizugriffe;
using PSU_Calculator.Komponenten;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace PSU_Calculator
{
  public class Updater
  {
    private static string Einstellungen = "https://raw.githubusercontent.com/Multithread/PSU_Calculator/master/Einstellungen.xml";
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

    public Control InvokeControl { get; set; }
    private finishedDelegateHandler finishedDelegate;

    public bool IsUpdating
    {
      get;
      set;
    }

    public Updater()
    {
      IsUpdating = false;
      finishedDelegate = new finishedDelegateHandler(callFinishedEvent);
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
      HasChanged = false;
      string data = DownloadFromSource(GetSettingsDownloadPath());
      XmlDocument doc = new XmlDocument();
      try
      {
        doc.LoadXml(data);
      }
      catch (XmlException)
      {
        return;
        //throw new Exception("XML ist Korupted");
      }
      Element tmpSettings = new Element(doc.FirstChild);
      Element versionen = tmpSettings.getElementByName(PSUCalculatorSettings.Version);
      foreach (Element ele in tmpSettings.getAlleElementeByName("File"))
      {
        string key = ele.getAttribut("Name");
        //Einstellungen Key Ignorieren, den haben wir bereits
        if (PSUCalculatorSettings.Einstellungen.Equals(key))
        {
          continue;
        }
        //Version auslesen
        double version = 1;
        if (!Double.TryParse(versionen.getElementByPfadOnCreate(key).getAttribut(PSUCalculatorSettings.Version), out version))
        {
          version = 1;
        }

        //Herunterladen wenn die ServerVersion neuer ist.
        if (CalculatorSettingsFile.Get().GetVersionForFile(key) < version)
        {
          //Herunterladen der Daten.
          string url = ele.Text.Trim();
          IsUpdating = true;
          data = DownloadFromSource(url);
          if (!string.IsNullOrEmpty(data))
          {
            string path = PSUCalculatorSettings.GetFilePath(key);
            StorageMapper.WriteToFilesystem(path, data);
            CalculatorSettingsFile.Get().SetVersionForFile(key, version);
            HasChanged = HasChanged || CalculatorSettingsFile.Get().HasChanged;
            CalculatorSettingsFile.Get().SaveSettings();
          }
          IsUpdating = false;
        }
      }
      callFinishedEvent();
    }

    private delegate void finishedDelegateHandler();
    /// <summary>
    /// Invoken des Events, je anchdem mit contro.Invoke
    /// </summary>
    public void callFinishedEvent()
    {
      if (UpdateFinishedEvent != null)
      {
        if (InvokeControl != null)
        {
          if (InvokeControl.InvokeRequired)
          {
            InvokeControl.Invoke(finishedDelegate);
            return;
          }
        }
        UpdateFinishedEvent(this);
      }
    }

    public bool HasChanged { get; set; }

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
      c.Encoding = Encoding.UTF8;
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
