using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSU_Calculator.DataWorker
{
  public class ComponentsOfPC
  {
    public List<Element> CPUs
    {
      set;
      private get;
    }

    public List<Element> GPUs
    {
      set;
      private get;
    }

    public List<Element> OtherComponents
    {
      set;
      private get;
    }

    /// <summary>
    /// Gibt ne liste der aktuell gewählten elementen
    /// </summary>
    /// <returns></returns>
    public Element getList()
    {
      Element ele = new Element("Components");
      if (CPUs != null)
      {
        foreach (Element e in CPUs)
        {
          ele.addElement(e);
        }
      }
      if (GPUs != null)
      {
        foreach (Element e in GPUs)
        {
          ele.addElement(e);
        }
      }
      if (OtherComponents != null)
      {
        foreach (Element e in OtherComponents)
        {
          ele.addElement(e);
        }
      }
      return ele;
    }
  }
}
