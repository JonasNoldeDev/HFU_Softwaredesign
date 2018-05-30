using System;
using System.Collections.Generic;

namespace DesPatternSingleton
{

    public class Person
    {
        public string Name;
        public int Age;
        private int Id;
        public static List<Person> Persons = new List<Person>();

        public Person(string Name, int Age)
        {
            this.Name = Name;
            this.Age = Age;
            this.Id = IDGenerator.Instance.GibMirNeId();
            Persons.Add(this);
        }

        public override string ToString()
        {
            return "Name: " + Name + ", Age: " + Age + ", " + "Id: " + Id;
        }

        public static void ListAllPersons()
        {
            foreach (var person in Persons)
                Console.WriteLine(person);
        }
    }

    /*
    class GlobalVariables
    {
        // public static int letzteID = 1;
        public static IDGenerator DerIdMacher = new IDGenerator();
    }
    */

    public class IDGenerator
    {
        private IDGenerator()
        {
            letzteID = 1;
        }

        private static IDGenerator _instance;

        public static IDGenerator Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new IDGenerator();
                return _instance;
            }
        }

        private int letzteID;
        public int GibMirNeId()
        {
            return letzteID++;
        }
    } 

    class Program
    {

        static void Main(string[] args)
        {
            // Eine Stelle, an der Personen angelegt werden
            new Person("Dieter", 44);
            new Person("Horst", 45);
            new Person("Walter", 33);
            new Person("Karl-Heinz", 22);

            // Eine ANDERE Stelle, an der Personen angelegt werden
            new Person("Brunhilde", 56);
            new Person("Maria", 75);
            new Person("Kunigunde", 22);
            new Person("Tusnelda", 12);

            Person.ListAllPersons();
        }
    }
}
