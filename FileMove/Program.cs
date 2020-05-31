using System;
using System.IO;
using System.Linq;

namespace FileMove
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args.Length == 0)
                    return;

                var fileNames = new DirectoryInfo(args[0]).GetFiles();
                fileNames.ToList().ForEach(d =>
                {

                    string name = d.Name;
                    name = name.Replace('(', '_').Replace(')', '_').Replace(' ', '_').Substring(0, name.LastIndexOf('.'));
                    string dir = d.DirectoryName + "/" + name;
                    if (!Directory.Exists(dir))
                        Directory.CreateDirectory(dir);
                    d.MoveTo(d.Directory + "/" + name + "/" + name + d.Extension);
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }
    }
}
