using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates
{
    class Program
    {
        public static List<User> users;
        static void Main(string[] args)
        {
            Initialize();
            Console.WriteLine("Please select the gender:");
            Console.WriteLine(string.Join(" ", users.Select(user => user.Gender).Distinct().ToArray()));
            var inputGender= Console.ReadLine();

            int minAge = users.Min(user => user.Age);
            int maxAge = users.Max(user => user.Age);
            Console.WriteLine($"Please select the minimal and maximal age from {minAge} to {maxAge}:");
            var inputMinAge = int.Parse(Console.ReadLine());
            var inputMaxAge = int.Parse(Console.ReadLine());
            Func<User, bool> condition = user => user.Gender == inputGender
                && user.Age <= inputMaxAge 
                && user.Age >= inputMinAge;
            Console.WriteLine(string.Join("\n", Filter(users, condition)));
        }

        private static void Initialize()
        {
            users = new List<User>()
            {
                new User { Age = 19, Name = "Sam", Gender = "M" },
                new User { Age = 17, Name = "Jam", Gender = "F" },
                new User { Age = 25, Name = "Tom", Gender = "M" },
                new User { Age = 15, Name = "Pam", Gender = "F" },
                new User { Age = 22, Name = "Sarah", Gender = "F" },
                new User { Age = 41, Name = "Michael", Gender = "M" },
                new User { Age = 19, Name = "Edwin", Gender = "M" },
                new User { Age = 24, Name = "Molly", Gender = "F"},
            };
        }

        private static List<User> Filter(List<User> users, Func<User, bool> func)
        {
            return users.Where(func).ToList();            
        }
    }
}
