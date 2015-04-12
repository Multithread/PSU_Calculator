using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSU_Calculator.DataWorker.Elementworker
{
  public class ElementDataContainer
  {
    public ElementDataContainer(Element ele)
    {
      if (ele == null)
      {
        Data = new Element("Empty");
        return;
      }
      if (ele.getElement("Data") != null)
      {
        Data = ele.getElement("Data");
        if (Data == null)
        {
          Data = new Element("Empty");
        }
        return;
      }
      Data = ele;
    }

    public Element Data
    {
      get;
      private set;
    }

    public Stecker Conectors
    {
      get
      {
        Element e = Data.getElement("Stecker");
        if (e == null)
        {
          return new Stecker(new Element("Empty"));
        }
        return new Stecker(e);
      }
    }

    public Energy GpuEnergy
    {
      get
      {
        Element e = Data.getElement("Energy");
        if (e == null)
        {
          return new Energy(new Element("Empty"));
        }
        return new Energy(e);
      }
    }
  }
}
