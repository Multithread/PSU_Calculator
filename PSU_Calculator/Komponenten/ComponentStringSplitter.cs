using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSU_Calculator.Komponenten
{
  public class ComponentStringSplitter
  {
    private Dictionary<string, string> DataDict = new Dictionary<string, string>();
    private char escape = '\\';
    private char splitter = ';';
    private List<string> UnusedKeys = new List<string>();
    public ComponentStringSplitter(string line)
    {
      int startpos = 0;
      while (startpos <= line.Length)
      {
        startpos = AddToData(line, startpos);
      }
    }

    private int AddToData(string line, int startpos)
    {
      StringBuilder sb = new StringBuilder();
      string key = "";
      string value = "";
      startpos = GetDatPart(line, startpos, sb);
      key = sb.ToString();
      UnusedKeys.Add(key);
      sb.Clear();
      startpos = GetDatPart(line, startpos, sb);
      value = sb.ToString();
      DataDict.Add(key, value);
      return startpos;
    }

    /// <summary>
    /// Gibt einen Teilstring zurück aufgrund des Splitters und des escape char's
    /// </summary>
    /// <param name="data">zeichenkette des grundinhaltes, welche Zeichenweise abtgerolt werdne soll</param>
    /// <param name="startpos">pos an der angefangen wird mir abrollen.</param>
    /// <param name="sb">Stringoulder, beinhaltet nachher das Ergebnis</param>
    /// <returns></returns>
    private int GetDatPart(string data, int startpos, StringBuilder sb)
    {
      int pos = startpos;
      bool isEscape = false;
      for (; pos < data.Length; pos++)
      {
        if (isEscape)
        {
          isEscape = false;
          sb.Append(data[pos]);
          continue;
        }
        if (data[pos] == splitter)
        {
          break;
        }
        else if (data[pos] == escape)
        {
          isEscape = true;
          continue;
        }
        else
        {
          sb.Append(data[pos]);
        }
      }
      return pos + 1;
    }

    public string GetValueForKey(string key)
    {
      UnusedKeys.Remove(key);
      string data = "";
      if (Data.TryGetValue(key, out data))
      {
        return data;
      }
      return "";
    }

    public Dictionary<string, string> Data
    {
      get
      {
        return DataDict;
      }
    }


    /// <summary>
    /// Gibt es mehr unbenutze Keys?
    /// </summary>
    /// <returns></returns>
    public bool HasNext()
    {
      return UnusedKeys.Count > 0;
    }

    public string Next()
    {
      return UnusedKeys[0];
    }

  }
}
