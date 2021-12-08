using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics
{
    public class FileReader<T> where T : IAgeable
    {
        public T IncreaseAge(string filePath)
        {
            using (var streamReader = new StreamReader(filePath))
            {
                var fileContent = streamReader.ReadToEnd();
                var ageable = JsonConvert.DeserializeObject<T>(fileContent);
                ageable.Age++;
                return ageable;
            }
        }
    }

    public class FileReader
    {
        public T IncreaseCount<T>(string filePath) where T : ICountable
        {
            using (var streamReader = new StreamReader(filePath))
            {
                var fileContent = streamReader.ReadToEnd();
                var countable = JsonConvert.DeserializeObject<T>(fileContent);
                countable.Count--;
                return countable;
            }
        }
    }
}


