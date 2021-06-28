using System;
using System.Threading;

namespace OopConstructors
{
    class Program
    {
        static void Main(string[] args)
        {
            Order order = new Order();

            order.AddOrUpdateItem("Product 1", 1, 10);
            order.AddOrUpdateItem("Product 2", 2, 20);
            order.AddOrUpdateItem("Product 1", 2);
            order.PrintOrder();
        }

        private static void SampleWithStaticCtors()
        {
            DateTime now = DateTime.Now;
            Console.WriteLine($"Now is: {now:yyyy-MM-dd HH:mm:ss.fff}");

            // simulate some long running process
            Thread.Sleep(1000);

            Console.WriteLine($"Before using Application class first time ...");
            Application myApp = new Application("My cool app");
            Console.WriteLine($"App start is: {Application.AppStart:yyyy-MM-dd HH:mm:ss.fff}");
            myApp.Run();

            TimeSpan executionTime = DateTime.Now - Application.AppStart;
            Console.WriteLine($"App finished working in {executionTime.TotalMilliseconds} ms");
        }
    }
}
