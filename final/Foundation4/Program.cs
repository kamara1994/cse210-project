using System;
using System.Collections.Generic;

namespace Foundation4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var activities = new List<Activity>
            {
                // Matches your sample outputs
                new Running(new DateTime(2025,10,26), 30, 5.0),      // 5.0 km
                new Cycling(new DateTime(2025,10,27), 40, 22.0),     // 22.0 kph
                new Swimming(new DateTime(2025,10,28), 25, 30)       // 30 laps * 50m = 1.5 km
            };

            foreach (var a in activities)
                Console.WriteLine(a.Summary());
        }
    }
}
