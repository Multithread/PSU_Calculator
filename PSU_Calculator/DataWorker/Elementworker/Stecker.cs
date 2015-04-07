using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSU_Calculator.DataWorker
{
  public class Stecker
  {
    private Element Daten;

    public Stecker(Element data)
    {
      Daten = data;
    }

    public int PCIE6
    {
      get
      {
        return GetFromElementAsInt("PCIE6");
      }
    }

    public int PCIE8
    {
      get
      {
        return GetFromElementAsInt("PCIE8");
      }
    }

    public int PCIE68
    {
      get
      {
        return GetFromElementAsInt("PCIE68");
      }
    }

    public int Sata
    {
      get
      {
        return GetFromElementAsInt("Sata");
      }
    }

    public int Molex
    {
      get
      {
        return GetFromElementAsInt("Molex");
      }
    }

    private int GetFromElementAsInt(string elementname)
    {
      int output = 0;
      string attr= Daten.getAttribut(elementname);
      if (int.TryParse(attr, out output))
      {
        return output;
      }
      return 0;
    }

    /// <summary>
    /// Gibt an ob mindestens gleich viele Stecker vorhanden Sind wie am übergebenen Stecker.
    /// </summary>
    /// <param name="inStecker"></param>
    /// <returns></returns>
    public bool HasMoreOrEqualPlugsAs(Stecker inStecker)
    {
      if (!Daten.hasAttribut || !inStecker.Daten.hasAttribut)
      {
        return true;
      }
      if (PCIE8 < inStecker.PCIE8)
      {
        if (PCIE8 + PCIE68 < inStecker.PCIE8)
        {
          return false;
        }
        if ((PCIE8 + PCIE68 - inStecker.PCIE8) + PCIE6 < inStecker.PCIE6)
        {
          return false;
        }
      }
      else if (PCIE6 < inStecker.PCIE6)
      {
        return false;
      }
      if (Sata < inStecker.Sata)
      {
        return false;
      }
      if (Molex < inStecker.Molex)
      {
        return false;
      }
      return true;
    }
  }
}
