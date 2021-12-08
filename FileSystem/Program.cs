using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Please enter file format: txt or csv");
                string formatInput = Console.ReadLine();
                if (formatInput == null 
                    || (!formatInput.Equals("txt", StringComparison.InvariantCultureIgnoreCase)
                    && !formatInput.Equals("csv", StringComparison.InvariantCultureIgnoreCase)))
                {
                    throw new Exception("Please enter txt or csv only.");
                }
                formatInput = formatInput.ToLower();
                string path = $@"E:\Docs\EPAM_lab\C#\StreamsAdaptersSamples\FileSystem\NewTextDocument.{formatInput}";
                using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        string fileLine = reader.ReadLine();
                        while (fileLine != null)
                        {
                            var delimiter = formatInput == "txt" ? ' ' : ',';
                            var gotNumbers = fileLine.Split(delimiter).Select(number => int.Parse(number)).ToArray();
                            if (gotNumbers.Count() != 2)
                            {
                                throw new Exception("Each line should contain two numbers");
                            }
                            Console.WriteLine($"Processing line: {fileLine}");
                            Console.WriteLine($"x + y = {gotNumbers[0] + gotNumbers[1]}");
                            Console.WriteLine($"x * y = {gotNumbers[0] * gotNumbers[1]}");
                            if (gotNumbers[1] == 0)
                            {
                                throw new Exception("Cannot divide by zero");
                            }
                            Console.WriteLine($"x / y = {gotNumbers[0] / (double)gotNumbers[1]}");
                            fileLine = reader.ReadLine();
                        }
                    }
                }
            }
            catch (FormatException exception)
            {
                Console.WriteLine("Please check the file, as the program cannot parse Int into string");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
