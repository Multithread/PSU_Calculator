using PSU_Calculator.DataWorker;
using PSU_Calculator.Dateizugriffe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PSU_Calculator
{
  static class Program
  {
    /// <summary>
    /// Der Haupteinstiegspunkt für die Anwendung.
    /// </summary>
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      if (!StorageMapper.Existiert(PSUCalculatorSettings.DirectoryPath))
      {
        FirstUsageInfoBox boxie = new FirstUsageInfoBox();
        boxie.ShowDialog();
      }
      Application.Run(new Form1());
    }
  }
}
