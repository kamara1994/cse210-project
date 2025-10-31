using System;
using System.Collections.Generic;

namespace Foundation3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var events = new List<Event>();

            // 1) Lecture
            var lec = new Lecture(
                title: "Clean Code 101",
                description: "Intro to SOLID",
                when: new DateTime(2025, 10, 30, 10, 0, 0),
                where: new Address("Austin Auditorium", "Rexburg", "ID"),
                speaker: "Dr. Jensen",
                capacity: 120);
            events.Add(lec);

            // 2) Reception
            var rec = new Reception(
                title: "Founders Mixer",
                description: "Meet & Greet",
                when: new DateTime(2025, 11, 2, 18, 0, 0),
                where: new Address("BYU-I Ballroom", "Rexburg", "ID"),
                rsvpEmail: "rsvp@nimbus.example");
            events.Add(rec);

            // 3) Outdoor Gathering
            var outd = new OutdoorGathering(
                title: "Team Hike",
                description: "Scenic loop trail",
                when: new DateTime(2025, 11, 9, 8, 0, 0),
                where: new Address("Kelly Canyon", "Ririe", "ID"),
                forecast: "Sunny, light breeze");
            events.Add(outd);

            // Print
            foreach (var ev in events)
            {
                Console.WriteLine(ev.StandardDetails());
                Console.WriteLine();
                Console.WriteLine(ev.FullDetails());
                Console.WriteLine();
                Console.WriteLine(ev.ShortDescription(ev.GetType().Name.Replace("Gathering","")));
                Console.WriteLine();
            }
        }
    }
}
