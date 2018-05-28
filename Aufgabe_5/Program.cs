using System;
using System.Collections.Generic;

namespace Aufgabe_5
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    class Person
    {
        public string Name;
        public int Age;
    }

    class Lecturer : Person
    {
        public string Office;
        public string Consultation;
        public List<Course> Courses;

        public void ShowCourses ()
        {
            Console.WriteLine(this.Name + "'s Courses:");

            foreach (var course in Courses)
            {
                Console.WriteLine(course.Title);
            }
        }

        public void ShowParticipants ()
        {
            Console.WriteLine(this.Name + "'s Participants:");

            List<Participant> participants = new List<Participant>();

            foreach (var course in Courses)
            {
                foreach (var participant in course.Participants)
                {
                    if (!participants.Contains(participant))
                    {
                        participants.Add(participant);
                    }
                }
            }

            foreach (var participant in participants)
            {
                Console.WriteLine(participant.Name);
            }
        }
    }

    class Participant : Person
    {
        public int MatriculationNumber;

        public List<Course> Courses;
    }

    class Course
    {
        public string Title;
        public string Date;
        public string Room;
        public Lecturer Lecturer;
        public List<Participant> Participants;
        
        public void ShowInformation ()
        {
            Console.WriteLine("Course: " + Title);
            Console.WriteLine("Date: " + Date);
            Console.WriteLine("Room: " + Room);
        }
    }
}
