using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuParser
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("File missing");
                return;
            }

            string filepath = args[0];
            if (!File.Exists(filepath))
            {
                Console.WriteLine($"File doesn't exist: {filepath}");
                return;
            }

            string[] lines = File.ReadAllLines(filepath);

            int gridId = 0;
            StreamWriter currentWriter = null;

            for (int lineIdx = 0; lineIdx < lines.Length; lineIdx++)
            {
                if (lines[lineIdx].Contains("Grid"))
                {
                    gridId++;

                    currentWriter?.Dispose();
                    var currentFile = new FileStream($"grid{gridId}.txt", FileMode.Append);
                    currentWriter = new StreamWriter(currentFile);
                }
                else
                {
                    currentWriter?.WriteLine(
                        ParseLine(lines[lineIdx]));
                }
            }
        }

        private static string ParseLine(string line)
        {
            string ret = "";
            foreach (short s in line.
                Select(c => Int16.Parse(c.ToString())))
            {
                if (s == 0)
                    ret += $" *";
                else
                    ret += $" {s}";
            }
            return ret;
        }
    }
}
