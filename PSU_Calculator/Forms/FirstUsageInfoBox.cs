using PSU_Calculator.DataWorker;
using PSU_Calculator.Dateizugriffe;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PSU_Calculator
{
  public partial class FirstUsageInfoBox : Form
  {
    public FirstUsageInfoBox()
    {
      InitializeComponent();
      StorageMapper.CreateStructure();
    
      var update= new Updater();
      update.UpdateFinishedEvent += update_UpdateFinishedEvent;
      update.RunUpdateAsync();
    }

    void update_UpdateFinishedEvent(Updater sender)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new updateInvoke(update_UpdateFinishedEvent), new object[] { sender });
        return;
      }
      this.Close();
    }

    public delegate void updateInvoke(Updater sender);
  }
}
