using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Civic.V2rayProxyFilter
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string path = @"C:\Users\Administrator\Desktop\Untitled-1.txt";
            string[] lines = await File.ReadAllLinesAsync(path);
            List<string> list = new();
            foreach (var item in lines)
            {
                string[] row = item.Split(' ');
                if (row[^1] == "[proxy]")
                {
                    //2021/02/22 15:34:05 127.0.0.1:58470 accepted //play.google.com:443 [proxy]
                    if (!list.Contains(row[^2]))
                    {
                        list.Add(row[^2]);
                        Console.WriteLine(row[^2]);
                    }
                }
            }
            Console.ReadLine();
        }
    }
}
