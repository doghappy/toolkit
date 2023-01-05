using System;
using System.Linq;
using System.Text;

namespace Civic.PdfParagraphFormatter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter the special characters that need to be filtered. If there are more than one, please use commas to separate them.");
            var characters = Console.ReadLine()
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Where(s => s.Length == 1)
                .Select(s => s[0])
                .ToList();
            Console.WriteLine();
            Console.WriteLine("Special characters to be filtered: ");
            foreach (var item in characters)
            {
                Console.Write(item + "\t");
            }
            Console.WriteLine();
            Console.WriteLine("-----------");
            while (true)
            {
                var builder = new StringBuilder();
                Console.WriteLine("Please enter the pdf paragraph to be formatted:");
                Console.WriteLine();
                while (true)
                {
                    string input = Console.ReadLine();
                    if (string.IsNullOrEmpty(input))
                    {
                        break;
                    }
                    else
                    {
                        builder.AppendLine(input);
                    }
                }
                string text = builder.ToString().Replace(Environment.NewLine, " ");
                var output = new StringBuilder();
                foreach (var item in text)
                {
                    if (!characters.Contains(item))
                    {
                        output.Append(item);
                    }
                }
                Console.WriteLine();
                var defaultColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine(output.ToString());
                Console.ForegroundColor = defaultColor;
                Console.WriteLine();
            }
        }
    }
}
