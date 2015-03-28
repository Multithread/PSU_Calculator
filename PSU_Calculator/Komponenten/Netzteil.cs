using PSU_Calculator.DataWorker;
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
        UsageLoadMinimum = -1;
        Geizhals = geizhalsLink;
        Quality = qualitaet;
      }

      public PowerSupply(IStringSplitter ss)
        : base()
      {
        Name = ss.GetValueForKey("Name");
        UsageLoadMinimum = getIntForString(ss.GetValueForKey("Min"));
        GPGPU = getIntForString(ss.GetValueForKey("Max"));
        Quality = getIntForString(ss.GetValueForKey("Quali"));

        if(UsageLoadMinimum == 0)
        {
          UsageLoadMinimum = -1; 
        }
        string key = "";
        while (ss.HasNext())
        {
          key = ss.Next();
          PriceCompareList.Add(key.ToUpper(), ss.GetValueForKey(key));
        }
      }

      private int getIntForString(string data)
      {
        int value = 0;
        if (int.TryParse(data, out value))
        {
          return value;
        }
        return 0;
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

    public int UsageLoadMinimum
    {
      get;
      set;
    }

    public override string GetOrginalString()
    {
      StringBuilder db = new StringBuilder();
      return string.Format("name={0};tdp={1};max={2};quali={3};DE={4}", Name, TDP, UsageLoadMaximum, Quality, Geizhals);
    }

    public int Quality
    {
      set;
      get;
    }
  }
}
