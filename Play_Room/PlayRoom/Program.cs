using System;
using System.Collections.Generic;
using System.Linq;

namespace PlayRoom
{
    class Program
    {
        static void Main(string[] args)
        {
            var toys = GetInitialToysSet();

            Console.WriteLine("Please enter the age of the child:");
            bool shouldRetry;
            int age = 0;
            do
            {
                age = InputAge(out shouldRetry);
            }
            while (shouldRetry);

            Console.WriteLine("Enter child's gender: Boy or Girl or Unisex");
            ToyUserGender? genderValue = null;
            do
            {
                genderValue = InputGender(out shouldRetry);
            }
            while (shouldRetry);
            
            var filteredToys = toys
                .Where(toy => toy.SuitsTheAge(age) && toy.SuitsUserGender(genderValue.Value))
                .OrderByDescending(toy => toy.Price)
                .ThenBy(toy => toy.Name);

            if (!filteredToys.Any())
            {
                Console.WriteLine("No toys are found for your request.");
            }            

            foreach (var item in filteredToys)
            {
                Console.WriteLine(item.ToString());
            }
        }

        private static List<Toy> GetInitialToysSet()
        {
            var toys = new List<Toy>();
            toys.Add(new Toy("Doll_1", 10.55m, "medium", 5, 8, ToyType.Doll));
            toys.Add(new Toy("Doll_2", 7.20m, "small", 8, 12, ToyType.Doll));
            toys.Add(new Toy("Doll_3", 50.00m, "big", 5, 12, ToyType.Doll));
            toys.Add(new Toy("Car_1", 35.25m, "medium", 3, 12, ToyType.Car));
            toys.Add(new Toy("Car_2", 15.00m, "small", 9, 12, ToyType.Car));
            toys.Add(new Toy("Car_1", 7.00m, "medium", 2, 5, ToyType.Car));
            toys.AddRange(new Toy[]
            {
            new Toy("Constructor", 80.00m, "big", 5, 12, ToyType.Constructor),
            new Toy("Blocks", 55.00m, "big", 2, 5, ToyType.Constructor),
            new Toy("Ball", 20.35m, "medium", 2, 5, ToyType.Ball),
            new Toy("Soccer_ball", 25.85m, "medium", 6, 12, ToyType.Ball),
            new Toy("Basketball_ball", 32.00m, "medium", 10, 12, ToyType.Ball),
            new Toy("Rocket", 50.50m, "big", 10, 12, ToyType.Mechanism),
            new Toy("Robot", 43.15m, "medium", 7, 9, ToyType.Mechanism),
            new Toy("Teddy_bear", 25.00m, "medium", 5, 9, ToyType.Soft),
            new Toy("Hippo", 13.00m, "small", 2, 4, ToyType.Soft),
            new Toy("Cat", 15.75m, "medium", 5, 8, ToyType.Soft),
            new Toy("Dog", 30, "big", 8, 12, ToyType.Soft)
            });

            return toys;
        }

        private static int InputAge(out bool shouldRetry)
        {
            var age = 0;
            try
            {
                age = int.Parse(Console.ReadLine());
                if (age < 0 || age > 200)
                {
                    throw new ArgumentOutOfRangeException("age", "Age can be only from 0 to 200 years old.");
                }
                shouldRetry = false;
            }
            catch (FormatException exception)
            {
                Console.WriteLine("Please use only numbers.");
                shouldRetry = true;
            }
            catch (ArgumentOutOfRangeException exception)
            {
                Console.WriteLine(exception.Message);
                shouldRetry = true;
            }
            return age;
        }

        private static ToyUserGender InputGender(out bool shouldRetry)
        {
            ToyUserGender? genderValue = null;
            try
            {
                string gender = Console.ReadLine();
                genderValue = (ToyUserGender)Enum.Parse(typeof(ToyUserGender), gender);
                shouldRetry = false;
            }
            catch (Exception exception)
            {
                Console.WriteLine("Gender is incorrect, enter again");
                shouldRetry = true;
            }
            return genderValue.Value;
        }
    }
}
