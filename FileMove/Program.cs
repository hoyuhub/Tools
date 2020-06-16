using System;
using System.Collections.Generic;
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

                /*
                 * 以下内容为获取指定文件夹中所有文件
                 * 1 对文件重命名，主要去掉文件名中的殊字符
                 * 2 以重命名后的文件名创建文件夹
                 * 3 移动文件到创建的文件夹中
                 */
                var fileNames = new DirectoryInfo(args[0]).GetFiles();
                fileNames.ToList().ForEach(d =>
                {

                    string name = d.Name;
                    name = fileRename(name, '_').Substring(0, name.LastIndexOf('.'));

                    string dir = d.DirectoryName + "/" + name;
                    if (!Directory.Exists(dir))
                        Directory.CreateDirectory(dir);
                    d.MoveTo(d.Directory + "/" + name + "/" + name + d.Extension);
                });

                Console.WriteLine("恭喜，你的工作我做完了：）");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }

        // 特殊字符库
        private static List<char> charLibrary = new List<char>() { '(', ')', ' ' };

        /// <summary>
        /// 替换文件名中的特殊字符
        /// </summary>
        /// <param name="name"></param>
        /// <param name="replaceVal"></param>
        /// <returns></returns>
        private static string fileRename(string name, char replaceVal)
        {
            charLibrary.ForEach(d => name.Replace(d, replaceVal));
            return name;
        }
    }
}
