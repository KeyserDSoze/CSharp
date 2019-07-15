using CSharp.Library.History;
using System;

namespace CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Write a number to see the right version of C# in action.");
            string cSharpVersion = Console.ReadLine();
            do
            {
                IVersion history = HistoryFactory(cSharpVersion);
                history.Test();
                Console.WriteLine("Write a number to see the right version of C# in action. Or Exit to exit program.");
                cSharpVersion = Console.ReadLine();

            } while (cSharpVersion.ToLower() != "exit");
            Console.WriteLine("See you again later. Bye bye....");
        }
        private static IVersion HistoryFactory(string cSharpVersion)
        {
            switch (cSharpVersion)
            {
                case "0":
                case "1":
                default:
                    return new Version1();
                case "2":
                    return new Version2();
                case "3":
                    return new Version3();
                case "4":
                    return new Version4();
                case "5":
                    return new Version5();
                case "6":
                    return new Version6();
                case "7":
                    return new Version7();
                case "7.1":
                case "7.2":
                case "7.3":
                    return new Version7Plus();
                case "8":
                    return new Version8();
            }
        }
    }
}
