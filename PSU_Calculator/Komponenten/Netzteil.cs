using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSU_Calculator.Komponenten
{
  public class PowerSupply : PcComponent
  {
    private Dictionary<String, String> PriceCompareList = new Dictionary<string, string>();
      public PowerSupply(string name, int powerConsumation, int toTDP, string geizhalsLink)
        :this (name,powerConsumation,toTDP,geizhalsLink,50)  
    {
    }

      public PowerSupply(string name, int powerConsumation, int toTDP, string geizhalsLink, int qualitaet)
        : base(name, toTDP, powerConsumation)
      {
        Geizhals = geizhalsLink;
        Quality = qualitaet;
      }

      public PowerSupply(string psuString)
        : base()
      {
        string[] lines = psuString.Split(';');

      }

    public string Geizhals
    {
      get;
      set;
    }

    public int UsageLoadMaximum
    {
      get
      {
        return GPGPU;
      }
    }


    public override string GetOrginalString()
    {
      StringBuilder db = new StringBuilder();
      return string.Format("{0};{1};{2};{3};{4}", Name, TDP, UsageLoadMaximum, Quality, Geizhals);
    }

    public int Quality
    {
      set;
      get;
    }
  }
}
