using PSU_Calculator.Komponenten;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace PSU_Calculator.DataWorker
{
  public class Prices
  {
    private Thread worker;
    private WebClient wc;
    private static Prices instance;
    private bool running = false;
    private Control invokeControl;

    public static Prices Get()
    {
      if (instance == null)
      {
        instance = new Prices();
      }
      return instance;
    }

    private Prices()
    {
      wc = new WebClient();
    }

    public void Start(Control inControl)
    {
      if (!running)
      {
        worker = new Thread(new ThreadStart(run));
        worker.IsBackground = true;
        invokeControl = inControl;
        running = true;
        worker.Start();
      }
    }

    private void run()
    {
    //  foreach (PowerSupply ps in LoaderModul.getInstance().Netzteile)
      foreach (PowerSupply ps in ActiveComponents.Get().LastEmpfohlenePowerSupplys)
      {
        if (GetPricesForPSU(ps))
        {
         // pricesDoneOneUpdate(this, ps);
        }
      }
      running = false;
    }

    public bool GetPricesForPSU(PowerSupply psu)
    {
      if (!string.IsNullOrEmpty(psu.Price))
      {
        return false;
      }
      if (string.IsNullOrEmpty(psu.CurrentPresvergleichLink))
      {
        return false;
      }
      Preisvergleich = psu.CurrentPresvergleichLink;


      if (string.IsNullOrEmpty(""))
      {
        return false;
      }
      //Presivergleich hat sich geändert.
      if (Preisvergleich != psu.CurrentPresvergleichLink)
      {
        return false;
      }
      psu.Price = "";
      return true;
    }

    private string Preisvergleich
    {
      get;
      set;
    }

    void pricesDoneOneUpdate(Prices sender, PowerSupply psu)
    {
      if (invokeControl == null)
      {
        return;
      }
      if (invokeControl.InvokeRequired)
      {
        invokeControl.Invoke(new PricesInvoke(pricesDoneOneUpdate), new object[] { sender, psu });
        return;
      }
      if (UpdatePricesEvent != null)
      {
        UpdatePricesEvent(this, psu);
      }
    }
    public delegate void PricesUpdateDelegate(Prices sender, PowerSupply psu);
    public event PricesUpdateDelegate UpdatePricesEvent;
    public delegate void PricesInvoke(Prices sender, PowerSupply con);
  }
}
