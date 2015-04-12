using PSU_Calculator.DataWorker;
using PSU_Calculator.DataWorker.Elementworker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSU_Calculator.Komponenten
{
  public class PowerSupply : PcComponent
  {
    public static List<string> PriceComparer = new List<string>(); //Liste der Preisvergleiche
    private Dictionary<string, string> PriceDict = new Dictionary<string, string>();

    private Dictionary<string, string> PriceCompareDict = new Dictionary<string, string>();
    private List<string> tests = new List<string>();

    public PowerSupply(Element ele)
    {
      Price = "";
      this.XML = ele;
    }

    private bool AddToPreisvergleich(string key)
    {
      if (!PriceComparer.Contains(key))
      {
        PriceComparer.Add(key);
        return true;
      }
      return false;
    }

    public string CurrentPresvergleichLink
    {
      get
      {
        string output = "";
        if (!PriceCompareDict.TryGetValue(PSUCalculatorSettings.Get().SearchEngine.ToUpper(), out output))
        {
          return "";
        }
        return output;
      }
    }

    public List<string> Testberichte
    {
      get
      {
        return tests;
      }
    }

    /// <summary>
    /// gibt einen Preisvergleichslink zurück, bevorzugt den aktuel gewählten, bzw. den Default wert (DE)
    /// </summary>
    public string AnyPresvergleichLink
    {
      get
      {
        string output = "";
        if (PriceCompareDict.TryGetValue(PSUCalculatorSettings.Get().SearchEngine.ToUpper(), out output))
        {
          return output;
        }
        if (PriceCompareDict.TryGetValue(PSUCalculatorSettings.DefaultSearchEngine.ToUpper(), out output))
        {
          return output;
        }
        foreach (string key in PriceCompareDict.Keys)
        {
          if (PriceCompareDict.TryGetValue(key, out output))
          {
            return output;
          }
        }
        return "";
      }
    }

    public override Element XML
    {
      get
      {
        Element e = new Element("Netzteil");
        e.addAttribut("Name", Name);
        Element daten = new Element("Daten");
        daten.addAttribut("Min", UsageLoadMinimum.ToString());
        daten.addAttribut("Max", UsageLoadMaximum.ToString());
        daten.addAttribut("Quali", Quality.ToString());

        e.addElement(daten);
        //Data element hinzufügen
        if (Data != null)
        {
          e.addElement(Data);
        }

        daten = new Element("Preisvergleiche");
        foreach (string key in PriceCompareDict.Keys)
        {
          string output;
          PriceCompareDict.TryGetValue(key, out output);
          daten.addElement(new Element(key, output));
        }
        e.addElement(daten);

        daten = new Element("Testberichte");
        foreach (string test in tests)
        {
          daten.addElement(new Element("Test", test));
        }
        e.addElement(daten);
        return e;
      }
      set
      {
        PriceCompareDict.Clear();
        tests.Clear();

        Element e = value;
        Name = e.getAttribut("Name");
        Element data = e.getElement("Daten");
        UsageLoadMinimum = getIntForString(data.getAttribut("Min"));
        UsageLoadMaximum = getIntForString(data.getAttribut("Max"));
        Quality = getIntForString(data.getAttribut("Quali"));

        Data = e.getElementByName("Data");

        data = e.getElement("Preisvergleiche");
        if (data != null)
        {
          foreach (Element ele in data.getAllEntries())
          {
            AddToPreisvergleich(ele.Name);
            PriceCompareDict.Add(ele.Name, ele.Text.Trim());
          }
        }

        data = e.getElement("Testberichte");
        if (data != null)
        {
          foreach (Element ele in data.getAllEntries())
          {
            tests.Add(ele.Text.Trim());
          }
        }
      }
    }

    public string Geizhals
    {
      get;
      set;
    }

    public int UsageLoadMaximum
    {
      get;
      set;
    }

    public int UsageLoadMinimum
    {
      get;
      set;
    }

    public string Price
    {
      get
      {
        string price;
        if (PriceDict.TryGetValue(CurrentPresvergleichLink, out price))
        {
          return price;
        }
        return "";
      }
      set
      {
        if (PriceDict.ContainsKey(CurrentPresvergleichLink))
        {
          PriceDict.Remove(CurrentPresvergleichLink);
        }
        PriceDict.Add(CurrentPresvergleichLink, value);
      }
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

    public override string ToString()
    {
      if (string.IsNullOrEmpty(Price))
      {
        return base.ToString();
      }
      return string.Format("{0}  {1:0.00}", Name, Price);
    }
  }
}
