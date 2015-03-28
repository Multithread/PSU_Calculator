using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSU_Calculator.DataWorker
{
  public class StringJoiner: ComponentStringSplitter
  {
    public StringJoiner(string line, bool toLower)
      : base(line, toLower)
    {
    }

    public StringJoiner()
      : base("", false)
    {
    }

    public void Put(string key, string value)
    {
      if (toLoowerString)
      {
        key = key.ToLower();
      }
      if (DataDict.ContainsKey(key))
      {
        DataDict.Remove(key);
      }
      DataDict.Add(key, value);
    }

    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();
      bool notFirst = false;
      foreach (string key in DataDict.Keys)
      {
        if (notFirst)
        {
          sb.Append(splitter);
        }
        sb.Append(key);
        sb.Append(valuesplitter);
        string value="";
        DataDict.TryGetValue(key, out value);
        sb.Append(value);
        notFirst = true;
      }

      return sb.ToString();
    }
  }
}
