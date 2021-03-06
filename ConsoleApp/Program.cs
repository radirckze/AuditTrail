﻿using AuditTrail;
using System;
using System.Threading.Tasks;

namespace ConsoleApp
{
    /// <summary>
    /// 
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            IAuditManager auditManager = new FileAuditManager();

            // Test logging a create operation
            Person person = new Person("Jon",  "Doe", 19);
            Task task = auditManager.LogOperationAsync("User001", Operation.Create, person);
            task.Wait();

            // Test logging an update operation (Need copy of original so cloning original)
            Person updatedPerson = new Person(person);
            updatedPerson.FirstName = "John";
            auditManager.LogOperation("User001", Operation.Update, person, updatedPerson);

            // Test logging a delete operation.
            auditManager.LogOperation("User001", Operation.Delete, updatedPerson);
            
        }
    }
}
