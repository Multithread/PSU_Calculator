using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSU_Calculator.DataWorker.Elementworker
{
  public class Energy
  {
    private Element Daten;

    public Energy(Element data)
    {
      Daten = data;
    }

    public int PCIE
    {
      get
      {
        return GetFromElementAsInt("PCIE");
      }
    }

    public int Watt
    {
      get
      {
        return GetFromElementAsInt("PCIE") * 11;
      }
    }

    public int Voltage
    {
      get
      {
        return GetFromElementAsInt("Voltage");
      }
    }

    private int GetFromElementAsInt(string elementname)
    {
      int output = 0;
      string attr = Daten.getAttribut(elementname);
      if (int.TryParse(attr, out output))
      {
        return output;
      }
      return 0;
    }
  }
}
