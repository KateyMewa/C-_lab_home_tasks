using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace FileSystem
{
    class Program
    {
        private static IEnumerable<char> _separators = new[] { ' ', ',' };
        private static List<string> _odds = new List<string>();
        private static List<string> _evens = new List<string>();
        private static List<string> _equals = new List<string>();
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Ctrl+C to exit");
                string folderPath = SelectFolder();
                var directory = new DirectoryInfo(folderPath);
                GetAllStringsFromFiles(directory);
                var oddsResult = _odds.Select(line =>
                {
                    var numbers = GetNumbersFromString(line);
                    var result = numbers.Aggregate((a, b) =>
                    {
                        return a == 0 ? b : b == 0 ? a : a * b;
                    });
                    return result.ToString();
                });
                var evensResult = _evens.Select(line =>
                {
                    var numbers = GetNumbersFromString(line);
                    var result = numbers.Sum();
                    return result.ToString();
                });
                var equalsResult = _equals.Select(line =>
                {
                    var numbers = GetNumbersFromString(line);
                    var result = string.Join(" ", numbers.OrderBy(number => number).Select(number => number.ToString()));
                    return result;
                });

                var oddsResultFilePath = Path.Combine(Directory.GetCurrentDirectory(), "OddsResults.txt");
                SaveResutsToFile(oddsResultFilePath, oddsResult);
                 
                var evensResultFilePath = Path.Combine(Directory.GetCurrentDirectory(), "EvensResults.txt");
                SaveResutsToFile(evensResultFilePath, evensResult);

                var equalsResultFilePath = Path.Combine(Directory.GetCurrentDirectory(), "EqualsResults.txt");
                SaveResutsToFile(equalsResultFilePath, equalsResult);
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

        private static void GetAllStringsFromFiles(DirectoryInfo directory)
        {
            foreach (var file in directory.EnumerateFiles())
            {
                foreach (var line in ReadAllStringsFromFile(file))
                {
                    var numbers = GetNumbersFromString(line);
                    int evensAmount = numbers.Count(number => number % 2 == 0);
                    int oddsAmount = numbers.Count(number => number % 2 != 0);
                    var comparationResult = evensAmount.CompareTo(oddsAmount);
                    switch (comparationResult)
                    {
                        case -1:
                            {
                                _odds.Add(line);
                                break;
                            }
                        case 0:
                            {
                                _equals.Add(line);
                                break;
                            }
                        case 1:
                            {
                                _evens.Add(line);
                                break;
                            }
                    }
                }
            }
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

        static int[] GetNumbersFromString(string line)
        {
            var separator = GetSeparatorForLine(line);
            var numbers = line.Split(new char[] { separator }, StringSplitOptions.RemoveEmptyEntries).Select(number => Convert.ToInt32(number));
            return numbers.ToArray();
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
