using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace Civic.Txt2Epub
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Guid.NewGuid().ToString().ToLower());
            string[] sections = new[]
            {
                "“金庸作品集”新序",
                //"附录 康熙朝的机密奏折",
                //"　　王鸿绪的奏折",
                //"　　李煦的奏折",
                //"　　李林盛的奏折",
                //"　　注：",
                //"附录一：成吉思汗家族",
                //"附录二：关于“全真教”",
                "释名",
                "后记",
                "附录 陈世骧先生书函",

            };
            //^　{2}第[一二三四五六七八九十]{1,3}章[　\s]{1,8}[\u4e00-\u9fa5]{4,10}$
            //var regex = new Regex(@"^第[一二三四五六七八九十]{1,3}章\s");
            //第一回 危邦行蜀道 乱世坏长城
            var regex = new Regex(@"^[一二三四五六七八九十]{1,3}\s[\u4e00-\u9fa5\s]{1,20}$");
            //string path = @"C:\Users\Administrator\OneDrive\Documents\Story\天龙八部.txt";
            string path = @"D:\Downloads\JY 大合集\金庸小说全集-世纪新修版\天龙八部（世纪新修版）.txt";
            using (StreamReader sr = File.OpenText(path))
            {
                string s;
                string section = null;
                int si = -1;
                var builder = new StringBuilder();
                string template = File.ReadAllText("Template.xhtml");

                while ((s = sr.ReadLine()) != null)
                {
                    if (s == "　　" || s == string.Empty)
                    {
                        continue;
                    }
                    else if (Regex.IsMatch(s, @"^[　\s]{1,10}$"))
                    {
                        continue;
                    }
                    else if (sections.Contains(s) || regex.IsMatch(s))
                    {
                        WriteContent(section, si, builder, template);
                        si++;
                        s = s.TrimStart('　');
                        section = s;
                        Console.WriteLine($"<li><a href=\"Section{si}.xhtml\">{s}</a></li>");
                    }
                    else
                    {
                        if (builder.Length > 0)
                        {
                            builder.Append(Environment.NewLine);
                        }
                        builder
                            .Append("    <p>")
                            .Append(s.Replace("\"", "&quot;").Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;"))
                            .Append("</p>");
                    }
                }
                WriteContent(section, si, builder, template);
            }
            Console.WriteLine(true);
            Console.ReadLine();
        }

        private static void WriteContent(string section, int si, StringBuilder builder, string template)
        {
            if (builder.Length > 0 && si > -1)
            {
                string text = template
                    .Replace("@Section", section)
                    .Replace("@Content", builder.ToString());
                File.WriteAllText($"Section{si}.xhtml", text, Encoding.UTF8);
                builder.Clear();
            }
        }
    }
}
