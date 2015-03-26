using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSU_Calculator.DataWorker
{
  public interface IStringSplitter
  {
    /// <summary>
    /// Wert für einen key auslesen.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    string GetValueForKey(string key);
    
    /// <summary>
    /// Gibt es mehr Keys in der Liste?
    /// </summary>
    /// <returns></returns>
    bool HasNext();

    /// <summary>
    /// Gibt den nächsten Key zurück.
    /// </summary>
    /// <returns></returns>
    string Next();

  }
}
