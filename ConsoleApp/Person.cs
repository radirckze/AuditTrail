using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    /// <summary>
    /// A simple class used to test the AuditTrail package
    /// </summary>
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public Person(string firstName, string lastName, int age)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Age = age;
        }

        // Clone constructor
        public Person(Person other)
        {
            this.FirstName = other.FirstName;
            this.LastName = other.LastName;
            this.Age = other.Age;
        }

    }
}
