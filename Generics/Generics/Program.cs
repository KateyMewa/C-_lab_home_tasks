using Newtonsoft.Json;
using System;
using System.IO;

namespace Generics
{
    class Program
    {
        private const string TestDataFolderName = "TestData";
        private const string PersonFileName = "Person";
        private const string FurnitureFileName = "Furniture";

        static void Main(string[] args)
        {
            var testDataDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), TestDataFolderName);

            ProcessPerson(testDataDirectoryPath);
            ProcessFurniture(testDataDirectoryPath);

            System.Console.WriteLine("Processing completed.");
        }

        private static void ProcessFurniture(string testDataDirectoryPath)
        {
            Execute(() =>
            {
                var furnitureFileReader = new FileReader();
                string furnitureFilePath = Path.Combine(testDataDirectoryPath, FurnitureFileName);
                var furniture = furnitureFileReader.IncreaseCount<Furniture>(furnitureFilePath);

                SaveResultToFile($"{furnitureFilePath}_result", furniture);
            });
        }

        private static void ProcessPerson(string testDataDirectoryPath)
        {
            Execute(() =>
            {
                var personFileReader = new FileReader<Person>();
                string personFilePath = Path.Combine(testDataDirectoryPath, PersonFileName);
                var person = personFileReader.IncreaseAge(personFilePath);

                SaveResultToFile($"{personFilePath}_result", person);
            });
        }

        private static void SaveResultToFile<T>(string fileName, T result)
        {
            using (var fileStream = File.Create(fileName))
            {
                using (var streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.Write(JsonConvert.SerializeObject(result));
                }
            }
        }

        private static void Execute(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
