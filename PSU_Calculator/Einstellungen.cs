using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PSU_Calculator
{
  public class PSUCalculatorSettings
  {
    public static string Version = "Version";
    public static string CPU = "CPUs";
    public static string GPU = "GPUs";
    public static string PowerSupplys = "Netzteile";
    public static string DataPath = "PSU_Calculator_Data";

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
    public static string PowerSupplyPath
    {
      get
      {
        return string.Format("{0}{3}{1}{3}{2}.txt", Application.StartupPath, PSUCalculatorSettings.DataPath, PSUCalculatorSettings.PowerSupplys, Path.DirectorySeparatorChar);
      }
    }
    public static string DirectoryPath
    {
      get
      {
        return string.Format("{0}{2}{1}", Application.StartupPath, PSUCalculatorSettings.DataPath, Path.DirectorySeparatorChar);
      }
    }
  }
}
