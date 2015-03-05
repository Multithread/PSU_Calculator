using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PSU_Calculator
{
  public class Einstellungen
  {
    public static string Version = "Version";
    public static string CPU = "CPUs";
    public static string GPU = "GPUs";
    public static string Netzteile = "Netzteile";
    public static string DataOrdner = "PSU_Calculator_Data";

    //Pfade im Dateisystem
    public static string CPUPfad
    {
      get
      {
        return string.Format("{0}{3}{1}{3}{2}.txt", Application.StartupPath, Einstellungen.DataOrdner, Einstellungen.CPU, Path.DirectorySeparatorChar);
      }
    }
    public static string GPUPfad
    {
      get
      {
        return string.Format("{0}{3}{1}{3}{2}.txt", Application.StartupPath, Einstellungen.DataOrdner, Einstellungen.GPU, Path.DirectorySeparatorChar);
      }
    }
    public static string NetzteilPfad
    {
      get
      {
        return string.Format("{0}{3}{1}{3}{2}.txt", Application.StartupPath, Einstellungen.DataOrdner, Einstellungen.Netzteile, Path.DirectorySeparatorChar);
      }
    }
    public static string OrdnerPfad
    {
      get
      {
        return string.Format("{0}{2}{1}", Application.StartupPath, Einstellungen.DataOrdner, Path.DirectorySeparatorChar);
      }
    }
  }
}
