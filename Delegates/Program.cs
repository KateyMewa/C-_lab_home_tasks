using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates
{
    class Program
    {
        delegate string StringAction(string line);
        static void Main(string[] args)
        {
            StringAction action;
            action = CutString;
            action += RemoveSpaces;
            action += AddDots;

            string keyword = Console.ReadLine();

            foreach (var func in action.GetInvocationList())
            {
                keyword = func.DynamicInvoke(keyword).ToString();
            }
            Console.WriteLine(keyword);
        }

        public static string CutString(string line)
        {
            return line.Substring(0, 11);
        }

        public static string RemoveSpaces(string line)
        {
            return line.Replace(" ", "");
        }

        public static string AddDots(string line)
        {
            return line + "...";
        }
    }
}
