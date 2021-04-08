using System;
using System.Collections.Generic;
using System.IO;

namespace aria2cTools
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                List<string> listStr = new List<string>();
                using (var sr = new StreamReader(""))
                    while (sr.Peek() > 0)
                    {
                        var line = sr.ReadLine();
                        if (line.Contains("bt-tracker="))
                            break;
                        listStr.Add(line);
                    }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
