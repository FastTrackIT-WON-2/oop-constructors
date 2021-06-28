using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopConstructors
{
    public class Application
    {
        static Application()
        {
            Console.WriteLine("Called static ctor ...");
            AppStart = DateTime.Now;
        }

        public Application(string name)
        {
            Name = name;
        }

        public static DateTime AppStart { get; }

        public string Name { get; }

        public void Run()
        {
            Console.WriteLine($"Executing application '{Name}'");
        }
    }
}
