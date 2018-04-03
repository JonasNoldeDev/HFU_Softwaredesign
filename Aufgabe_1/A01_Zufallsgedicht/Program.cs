using System;
using System.Linq;

namespace A01_Zufallsgedicht
{
    class Program
    {
        static string[] subjects = 
        { 
            "Harry", 
            "Hermine", 
            "Ron", 
            "Hagrid", 
            "Snape", 
            "Dumbledore" 
        };
        static string[] verbs = 
        { 
            "braut", 
            "liebt", 
            "studiert", 
            "hasst", 
            "zaubert", 
            "zerstört"
        };
        static string[] objects = 
        { 
            "Zaubertränke", 
            "den Grimm", 
            "Lupin", 
            "Hogwards", 
            "die Karte des Rumtreibers", 
            "Dementoren"
        };

        static Random random = new Random();

        static void Main(string[] args)
        {
            string[] poem = new string[5];
            for (int i = 0; i < poem.Length; i++)
            {
                poem[i] = GetVerse();
            }
            
            foreach (string verse in poem)
            {
                Console.WriteLine(verse);
            }
        }

        static string GetVerse()
        {
            string verse_subject = subjects[random.Next(subjects.Length - 1)];
            subjects = subjects.Where(val => val != verse_subject).ToArray();

            string verse_verb = verbs[random.Next(verbs.Length - 1)];
            verbs = verbs.Where(val => val != verse_verb).ToArray();

            string verse_object = objects[random.Next(objects.Length - 1)];
            objects = objects.Where(val => val != verse_object).ToArray();
            
            return verse_subject + " " + verse_verb + " " + verse_object;
        }
    }
}
