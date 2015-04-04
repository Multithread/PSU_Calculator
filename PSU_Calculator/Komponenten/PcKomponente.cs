using PSU_Calculator.DataWorker;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSU_Calculator.Komponenten
{
  public class PcComponent
  {
    public static string ComponentString = "Component";
    public PcComponent(string name, int tdp, string type)
    {
      Name = name;
      TDP = tdp;
      Type = type;
    }

    public PcComponent(Element ele)
    {
      XML = ele;
    }

    protected PcComponent()
    {
      Name = "";
      TDP = 0;
      Type = "None";
    }

    public string Name
    {
      get;
      set;
    }

    public virtual string Type
    {
      get;
      set;
    }

    public virtual int TDP
    {
      get;
      set;
    }

    public Element Data
    {
      get;
      set;
    }

    public virtual Element XML
    {
      get
      {
        Element e = new Element(ComponentString);
        e.addAttribut("Name", Name);
        e.addAttribut("TDP", TDP.ToString());
        e.addAttribut("Type",Type);
        if (Data != null)
        {
          e.addElement(Data);
        }
        return e;
      }
      set
      {
        Element tmpValue = value;
        Name = tmpValue.getAttribut("Name");
        Type = tmpValue.getAttribut("Type");
        TDP = getIntForString(tmpValue.getAttribut("TDP"));
        Data = tmpValue.getElement("Data");
      }
    }

    protected int getIntForString(string data)
    {
      int value = 0;
      if (int.TryParse(data, out value))
      {
        return value;
      }
      return 0;
    }

    public virtual string GetOrginalString()
    {
      return string.Format("{0}:{1}", Name, TDP);
    }

    public override string ToString()
    {
      return Name;
    }

    public bool IsEmpty()
    {
      if ("Empty".Equals(Type))
      {
        if (TDP == 0)
        {
          return true;
        }
      }
      return false;
    }

    public readonly static PcComponent Empty = new PcComponent("", 0, "Empty");
  }
}
