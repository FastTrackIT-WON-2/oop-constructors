using System;

namespace OopConstructors
{
    public partial class Person
    {
        public void Print()
        {
            // Console.WriteLine($"{FirstName} {LastName} {DateOfBirth:yyyy-MM-dd}");
            PrintToConsole($"{FirstName} {LastName} {DateOfBirth:yyyy-MM-dd}");
        }

        static partial void PrintToConsole(string message);
    }
}
