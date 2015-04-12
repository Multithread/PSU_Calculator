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
        return GetFromElementAsInt(SteckerType.PCIE6.ToString());
      }
    }

    public int PCIE8
    {
      get
      {
        return GetFromElementAsInt(SteckerType.PCIE8.ToString());
      }
    }

    public int PCIE68
    {
      get
      {
        return GetFromElementAsInt(SteckerType.PCIE68.ToString());
      }
    }

    public int ATX12
    {
      get
      {
        return GetFromElementAsInt(SteckerType.ATX12.ToString());
      }
    }

    public int Sata
    {
      get
      {
        return GetFromElementAsInt(SteckerType.Sata.ToString());
      }
    }

    public int Molex
    {
      get
      {
        return GetFromElementAsInt(SteckerType.Molex.ToString());
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

    public enum SteckerType
    {
      None = 0,
      PCIE8 = 1,
      PCIE68 = 2,
      PCIE6 = 3,
      Molex = 4,
      Sata = 5,
      ATX12 = 6
    }
  }
}
