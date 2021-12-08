using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace FileSystem
{
    class Program
    {
        private static IEnumerable<char> _separators = new []{' ', ','};

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Ctrl+C to exit");
                string folderPath = SelectFolder();
                var directory = new DirectoryInfo(folderPath);
                IEnumerable<string> resultLines = GetResults(directory);
                var resultFilePath = Path.Combine(directory.FullName, "Results.txt");
                SaveResutsToFile(resultFilePath, resultLines);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private static string SelectFolder()
        {
            string folderPath = string.Empty;
            do
            {
                Console.WriteLine("Please select the folder from the list below:");
                DirectoryInfo currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
                string availableDirectories = string.Join("\n", currentDirectory.GetDirectories().Select(d => d.Name));
                Console.WriteLine(availableDirectories);

                string folderName = Console.ReadLine();
                folderPath = Path.Combine(currentDirectory.FullName, folderName);
            }
            while (!Directory.Exists(folderPath));
            return folderPath;
        }

        private static IEnumerable<string> GetResults(DirectoryInfo directory)
        {
            var stringsFromFiles = new List<string>();
            foreach (var file in directory.EnumerateFiles())
            {
                stringsFromFiles.AddRange(ReadAllStringsFromFile(file));
            }

            var resultLines = stringsFromFiles.Select(GetResultForString);
            return resultLines;
        }

        static IEnumerable<string> ReadAllStringsFromFile(FileInfo file)
        {
            using (var textStream = file.OpenText())
            {
                while (!textStream.EndOfStream)
                {
                    yield return textStream.ReadLine();
                }
            }
        }

        static string GetResultForString(string line)
        {
            try
            {
                var separator = GetSeparatorForLine(line);
                var operands = line.Split(new char[] {separator}, StringSplitOptions.RemoveEmptyEntries).Select(operand => Convert.ToInt32(operand));
                var sum = operands.Sum();
                var multiplication = operands.Aggregate((a, b) => a * b);
                return $"sum = {sum}, multiplication = {multiplication}";
            }
            catch (Exception)
            {
                return "Result cannot be calculated, please check input values.";
            }
        }

        private static char GetSeparatorForLine(string line)
        {
            foreach (var separator in _separators)
            {
                if (line.Contains(separator))
                {
                    return separator;
                }
            }

            return ' ';
        }

        private static void SaveResutsToFile(string resultFilePath, IEnumerable<string> resultLines)
        {
            using (var resultFile = File.Create(resultFilePath))
            {
                using (var stream = new StreamWriter(resultFile))
                {
                    foreach (var resultLine in resultLines)
                    {
                        stream.WriteLine(resultLine);
                    }
                }
            }
        }
    }
}
