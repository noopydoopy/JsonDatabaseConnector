using ConsoleApp1.DataContracts;
using JsonDatabaseConnector;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            List<Person> persons = new List<Person>();
            for (int i = 0; i < 100; i++)
            {
                persons.Add(new Person() { Name = "A" + i, Age = i * 2 });
            }



            DataAccessAdapter adapter = new DataAccessAdapter();
            adapter.SaveEntities<Person>(persons, "Person");
            List<Person> test = adapter.GetEntities<Person>("Person");
            Console.WriteLine("At start we have" + string.Join(",", test.Select(c => c.Name)));

            ////Console.ReadLine();

            //Test add new one
            Console.WriteLine("Try to add new Person name G with Id 6");
            adapter.SaveEntity<Person>(new Person() { Name = "G", Age = 1, Id = 6 }, "Person");
            test = adapter.GetEntities<Person>("Person");
            Console.WriteLine("After add we have" + string.Join(",", test.Select(c => c.Name)));
            //Console.ReadLine();

            //// Test Update 
            //Console.WriteLine("Try to Update new Person Id 6 with name Tan");
            //adapter.UpdateEntity<Person>(new Person() { Name = "Tan", Age = 1, Id = 6 }, "Person");
            //test = adapter.GetEntities<Person>("Person");
            //Console.WriteLine("After update we have" + string.Join(",", test.Select(c => c.Name)));
            ////Console.ReadLine();

            //// Test Delete
            //Console.WriteLine("Try to delete new Person Id 6");
            //adapter.DeleteEntity<Person>(new Person() { Name = "G", Age = 1, Id = 6 }, "Person");
            //test = adapter.GetEntities<Person>("Person");
            //Console.WriteLine("After delete we have" + string.Join(",", test.Select(c => c.Name)));
            //Console.ReadLine();

        }

        static IConfiguration ConfigurationManager()
        {
            IConfiguration AppSetting = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
            return AppSetting;
        }

    }
}
