using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Txs.Core;

namespace TxsService
{
    class Program
    {
        static void Main(string[] args)
        {
            UserService service = new UserService();

            while (true)
            {
                var list = service.Top(10);

                WriteFile(list, "users");

                var value = service.Total();

                WriteFile(value, "total");
                Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")} 加载成功！");
                Thread.Sleep(TimeSpan.FromSeconds(5));
            }
           
        }

        static bool WriteFile(object value, string name)
        {
            string root = System.Configuration.ConfigurationManager.AppSettings["Directory"];
            string filePath = Path.Combine(root, $"{name}.json");
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(value);

            FileWriter.WriteText(filePath, json, Encoding.UTF8);

            return true;
        }
    }
}
