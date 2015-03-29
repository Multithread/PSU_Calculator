using PSU_Calculator.DataWorker;
using PSU_Calculator.Komponenten;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PSU_Calculator.Dateizugriffe
{
  public class StorageMapper
  {
    public static bool SetLocalData(string _version, List<PcComponent> _gpu, List<PcComponent> _cpu, List<PcComponent> _nt)
    {
      //erstesmal?
      if (!Properties.Einstellungen.Default.AskSaveLocal)
      {
        if (!StorageMapper.Existiert(PSUCalculatorSettings.DirectoryPath))
        {
          DialogResult dialogResult = MessageBox.Show("Wollen Sie die geupdateten Daten vom Server lokal bei sich Speichern?", "Speichern", MessageBoxButtons.YesNoCancel);
          if (dialogResult == DialogResult.Yes)
          {
            StorageMapper.erstelleOrdner(PSUCalculatorSettings.DirectoryPath);
          }
          else if (dialogResult == DialogResult.No)
          {

          }
          else
          {
            return false;
          }
          Properties.Einstellungen.Default.AskSaveLocal = true;
        }

      }
      //Daten schreiben wenn Ordner vorhanden
      if (StorageMapper.Existiert(PSUCalculatorSettings.DirectoryPath))
      {
        //wenn alles geschrieben werden konnte ist alles io:)
        if(addZeilen(PSUCalculatorSettings.GPUPath, _gpu)
        && addZeilen(PSUCalculatorSettings.CPUPath, _cpu))
        {
          return true;
        }
      }
      return false;
    }

    public static void GetLocalData(PSU_Calculator.Form1.boxInvoke del)
    {
      if (!StorageMapper.Existiert(PSUCalculatorSettings.DirectoryPath))
      {
        return;
      }
      LoaderModul m = LoaderModul.getInstance();
      string[] daten = null;
      //CPU's euinlesen
      daten = leseZeilen(PSUCalculatorSettings.CPUPath);
      if (daten != null)
      {
        m.AddCPURange(m.GetComponents(daten));
        del(true);
      }

      //GPU's einlesen
      daten = leseZeilen(PSUCalculatorSettings.GPUPath);
      if (daten != null)
      {
        m.AddGPURange(m.GetComponents(daten));
        del(false);
      }

      //Netzteile einlesen
      daten = leseZeilen(PSUCalculatorSettings.PowerSupplyPath);
      if (daten != null)
      {
        m.AddNetzteilRange(m.GetPowerSupplysFromArray(daten));
      }
    }

    public static bool Existiert(string pfad)
    {
      if (File.Exists(pfad))
      {
        return true;
      }
      else if (Directory.Exists(pfad))
      {
        return true;
      }
      return false;
    }

    private static bool addZeilen(string pfad, List<PcComponent> zeilen)
    {
      try
      {
        StringBuilder sb = new StringBuilder();
        foreach (PcComponent s in zeilen)
        {
          sb.AppendLine(s.GetOrginalString());
        }
        WriteToFilesystem(pfad, sb.ToString());
        return true;
      }
      catch (Exception)
      {
        return false;
      }
    }

    public static bool erstelle(string pfad)
    {
      try
      {
        FileStream fs = File.Create(pfad);
        fs.Close();
        return true;
      }
      catch (Exception)
      {
        return false;
      }
    }

    private static bool erstelleOrdner(string pfad)
    {
      try
      {
        DirectoryInfo fs = Directory.CreateDirectory(pfad);
        return true;
      }
      catch (Exception)
      {
        return false;
      }
    }

    private static string[] getOrdnerliste(string pfad)
    {
      try
      {
        return Directory.GetDirectories(pfad);
      }
      catch (Exception)
      {
        return null;
      }
    }

    private List<string> getFilelist(string pfad)
    {
      List<string> outp = new List<string>();
      try
      {
        string[] Files = System.IO.Directory.GetFiles(pfad);

        for (int i = 0; i < Files.Length; i++)
        {
          outp.Add(Files[i].ToString());

          //if (SubFolders == true)
          //{
          //  string[] Folders = System.IO.Directory.GetDirectories(pfad);
          //  for (int i = 0; i < Folders.Length; i++)
          //  {
          //    FileArray.AddRange(GetFileList(Folders[i], SubFolders));
          //  }
          //}
        }
      }
      catch (Exception)
      {
      }
      return outp;
    }

    private static string[] leseZeilen(string dateipfad)
    {
      List<string> output = new List<string>();
      string line = "";
      try
      {
        StreamReader sr = new StreamReader(dateipfad);
        while ((line = sr.ReadLine()) != null)
        {
          output.Add(line);
        }
        sr.Close();
      }
      catch (Exception)
      {
        return null;
      }
      return output.ToArray();
    }

    public static string ReadFromFilesystem(string pfad)
    {
      string output = "";
      try
      {
        StreamReader sr = new StreamReader(pfad);
        output = sr.ReadToEnd();
        sr.Close();
      }
      catch (Exception)
      {
        return "";
      }
      return output;
    }

    public static bool WriteToFilesystem(string pfad, string daten)
    {
      if (!Existiert(pfad))
      {
        erstelle(pfad);
      }
      StreamWriter sw = null;
      try
      {
        sw = new StreamWriter(pfad);
        sw.Write(daten);
        sw.Close();
      }
      catch (Exception ex)
      {
        try
        {
          sw.Close();
        }
        catch (Exception)
        {

        }
        return false;
      }
      return true;
    }

    public static void CreateStructure()
    {
      Directory.CreateDirectory(PSUCalculatorSettings.DirectoryPath);
    }
  }
}
