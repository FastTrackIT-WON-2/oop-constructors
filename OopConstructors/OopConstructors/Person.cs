using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopConstructors
{
    public class Person
    {
        /*
        public Person(string firstName, string lastName, DateTime dateOfBirth)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
        }
        */

        /*
        public Person(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
        */

        public string FirstName { get; init; }

        public string LastName { get; init; }

        public DateTime DateOfBirth { get; init; }

        public void Print()
        {
            Console.WriteLine($"{FirstName} {LastName} {DateOfBirth:yyyy-MM-dd}");
        }
    }

    public class Persons
    {
        public List<Person> PersonsCollection { get; init; }
    }
}
