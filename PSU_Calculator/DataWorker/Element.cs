using System.Collections.Generic;
using System.Xml;

namespace PSU_Calculator.DataWorker
{
  public class Element
  {
    private static char[] xmlescaping = { '<', '>', '\'', '\"', '&' };
    List<Element> elemente = new List<Element>();
    Dictionary<string, string> attribute = new Dictionary<string, string>();

    #region Konstruktoren
    public Element(string _name)
    {
      Name = _name;
      Text = "";
    }

    public Element(string _name, string _value)
    {
      Name = _name;
      Text = _value;
    }

    public Element(XmlNode xmlNode)
    {
      fromXML(xmlNode);
    }

    public static Element New(string name)
    {
      return new Element(name);
    }
    #endregion

    public Element addElement(Element ele)
    {
      elemente.Add(ele);
      hasElement = true;
      return this;
    }

    public Element addAttribut(string name, string value)
    {
      if (attribute.ContainsKey(name))
      {
        attribute.Remove(name);
      }
      attribute.Add(name, value);
      hasAttribut = true;
      return this;
    }

    public Element getElement(string name)
    {
      return getElementByName(name);
    }

    public Element getElementByPfad(string pfad)
    {
      Element output = null;
      output = getElementByName(pfad.Split('.')[0]);
      if (output == null)
      {
        return null;
      }
      if (pfad.IndexOf('.') == -1)
      {
        return output;
      }
      return output.getElementByPfad(pfad.Substring(pfad.Split('.')[0].Length + 1));
    }

    public Element getElementByPfadOnCreate(string pfad)
    {
      Element output = null;
      output = getElementByName(pfad.Split('.')[0]);
      if (output == null)
      {
        output = new Element(pfad.Split('.')[0]);
        addElement(output);
      }
      if (pfad.IndexOf('.') == -1)
      {
        return output;
      }
      return output.getElementByPfadOnCreate(pfad.Substring(pfad.Split('.')[0].Length + 1));
    }

    public string getAttribut(string name)
    {
      string output = "";
      if (!attribute.TryGetValue(name, out output))
      {
        output = "";
      }
      return output;
    }

    public int Length
    {
      get
      {
        return elemente.Count;
      }
    }

    public List<Element> getAllEntries()
    {
      return elemente;
    }

    public Element getElementByName(string name)
    {
      foreach (Element ele in elemente)
      {
        if (ele.Name.Equals(name))
        {
          return ele;
        }
      }
      return null;
    }

    public bool removeElement(Element element)
    {
      elemente.Remove(element);
      return true;
    }

    public void setElementByName(string name, Element newele)
    {
      int elenr = 0;
      foreach (Element ele in elemente)
      {
        if (ele.Name.Equals(name))
        {
          elemente[elenr] = newele;
          return;
        }
        elenr++;
      }
      addElement(newele);
    }

    public List<Element> getAlleElementeByName(string name)
    {
      List<Element> daten = new List<Element>();
      foreach (Element ele in elemente)
      {
        if (ele.Name.Equals(name))
        {
          daten.Add(ele);
        }
      }
      return daten;
    }

    //Erstellt Elemente von einem XML File her
    public void fromXML(XmlNode xml)
    {
      if (xml == null)
      {
        return;
      }
      Name = xml.Name;
      Text = (xml.Value == null ? "" : xml.Value);
      if (xml.Attributes != null && xml.Attributes.Count > 0)
      {
        XmlAttributeCollection att = xml.Attributes;
        for (int a = 0; a < att.Count; a++)
        {
          attribute.Add(att.Item(a).Name, att.Item(a).Value);
        }
        hasAttribut = true;
      }
      if (xml.ChildNodes.Count > 0)
      {
        XmlNodeList kinder = xml.ChildNodes;
        for (int a = 0; a < kinder.Count; a++)
        {
          if (kinder.Item(a).NodeType == XmlNodeType.Text)
          {
            this.Text = kinder.Item(a).Value.Replace("\r\n", "");
            if (Text.Length < 2)
            {
              Text = "";
            }
            else
            {
              Text = Text.TrimStart(' ').TrimEnd(' ');
            }
            continue;
          }
          Element neu = new Element(kinder.Item(a));
          elemente.Add(neu);
        }
        hasElement = true;
      }
    }

    //Erstellt aus sich selber und allen Unterelementen wieder ein XML    
    public string getXML()
    {
      return getXML("", "  ");
    }

    public string getXML(string deep, string deepAdd)
    {
      if (Name.Length == 0)
      {
        return "";
      }
      string xml = "";
      if (!hasElement && Text.Length == 0)
      {
        xml = deep + "<" + Name + getAttributeAsXml() + "/>\r\n";
        return xml;
      }
      xml = deep + "<" + Name + getAttributeAsXml() + ">\r\n";
      if (!string.IsNullOrEmpty(Text))
      {
        if (Text.IndexOfAny(xmlescaping, 0) != -1)
        {
          xml = string.Format("{0}{1}{2}{3}\r\n", xml, deep, deepAdd, Text.Replace("&", "&amp;").
            Replace("<", "&lt;").Replace(">", "&gt;").Replace("\'", "&apos;").Replace("\"", "&quot;"));
        }
        else
        {
          xml = string.Format("{0}{1}{2}{3}\r\n", xml, deep, deepAdd, Text);
          //xml += deep + deepAdd + Text + "\r\n";
        }
      }
      if (hasElement)
      {
        Element[] kinder = getAllEntries().ToArray();
        for (int a = 0; a < kinder.Length; a++)
        {
          xml += kinder[a].getXML(deep + deepAdd, deepAdd);
        }
      }
      xml += deep + "</" + Name + ">\r\n";
      return xml;
    }

    private string getAttributeAsXml()
    {
      string back = "";
      if (hasAttribut)
      {
        foreach (KeyValuePair<string, string> att in attribute)
        {
          back += " " + att.Key + "=\"" + att.Value + "\"";
        }
      }
      return back;
    }

    public bool Bezeichnung
    {
      private set;
      get;
    }

    public bool hasElement
    {
      private set;
      get;
    }

    public bool hasAttribut
    {
      private set;
      get;
    }

    public string Name
    {
      get;
      set;
    }

    public string Text
    {
      get;
      set;
    }

    public override string ToString()
    {
      return Name;
    }
  }
}
