using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Txs.Core
{
    public class FileWriter
    {
        public static void WriteText(string path, string str, Encoding encode, bool append = false)
        {
            var directory = Path.GetDirectoryName(path);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using(StreamWriter sw = new StreamWriter(path, append, encode))
            {
                if (!Directory.Exists(path.Substring(0, path.LastIndexOf("\\"))))
                {
                    Directory.CreateDirectory(path.Substring(0, path.LastIndexOf("\\")));
                }

                sw.Write(str);
                sw.Close();
                sw.Dispose();
            }
           


        }
    }
}
