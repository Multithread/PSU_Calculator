using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSU_Calculator.DataWorker
{
  public class ComponentStringSplitter: IStringSplitter
  {
    protected Dictionary<string, List<string>> DataDict = new Dictionary<string, List<string>>();
    protected char escape = '\\';
    protected char splitter = ';';
    protected char valuesplitter = '=';
    protected bool toLoowerString = true;
    protected List<string> UnusedKeys = new List<string>();

    public ComponentStringSplitter(string line, bool toLower)
    {
      toLoowerString = toLower;
      int startpos = 0;
      while (startpos <= line.Length)
      {
        startpos = AddToData(line, startpos);
      }
    }

    /// <summary>
    /// Fügt dem Dictionary die einzelnen Werte hinzu.
    /// </summary>
    /// <param name="line"></param>
    /// <param name="startpos"></param>
    /// <returns></returns>
    private int AddToData(string line, int startpos)
    {
      StringBuilder sb = new StringBuilder();
      string key = "";
      List<string> value = new List<string>();
      int pos = startpos;
      bool isEscape = false;
      for (; pos < line.Length; pos++)
      {
        if (isEscape)
        {
          isEscape = false;
          sb.Append(line[pos]);
          continue;
        }
        if (line[pos] == splitter)
        {
          break;
        }
        else
          if (line[pos] == valuesplitter)
          {
            if (key.Length == 0)
            {
              key = sb.ToString();
            }
            else
            {
              value.Add(sb.ToString());
            }
            sb.Clear();
            continue;
          }
          else if (line[pos] == escape)
          {
            isEscape = true;
            continue;
          }
          else
          {
            sb.Append(line[pos]);
          }
      }
      value.Add(sb.ToString());
      if (value.Count > 0 && key.Length > 0)
      {
        if (!DataDict.ContainsKey(key))
        {
          if (toLoowerString)
          {
            key = key.ToLower();
          }
          UnusedKeys.Add(key);
          DataDict.Add(key, value);
        }
      }
      return pos + 1;
    }

    public string GetValueForKey(string key)
    {
      if (toLoowerString)
      {
        key = key.ToLower();
      }
      UnusedKeys.Remove(key);
      List<string> data;
      if (Data.TryGetValue(key, out data))
      {
        return data[0];
      }
      return "";
    }

    public List<string> GetValueForKeyList(string key)
    {
      if (toLoowerString)
      {
        key = key.ToLower();
      }
      UnusedKeys.Remove(key);
      List<string> data;
      if (Data.TryGetValue(key, out data))
      {
        return data;
      }
      return new List<string>();
    }

    public double GetValueForKeyAsDouble(string key)
    {
      string data = GetValueForKey(key);
      double output;
      if (double.TryParse(data, out output))
      {
        return output;
      }
      return 0.0d;
    }

    public Dictionary<string, List<string>> Data
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
      string output = "";
      output = UnusedKeys[0];
      UnusedKeys.RemoveAt(0);
      return output;
    }
  }
}
