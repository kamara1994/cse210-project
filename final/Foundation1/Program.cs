using System;
using System.Collections.Generic;

namespace Foundation1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var videos = new List<Video>();

            var v1 = new Video("Faith & Works", "Joseph A. Kamara", 420);
            v1.AddComment(new Comment("Amaka", "Loved the scripture tie-in!"));
            v1.AddComment(new Comment("Samuel", "Clear explanation."));
            videos.Add(v1);

            var v2 = new Video("Shipping Demo", "BYU-I CSE210", 300);
            v2.AddComment(new Comment("Kofi", "Labels looked professional."));
            videos.Add(v2);

            var v3 = new Video("Planner Walkthrough", "Team Rexburg", 375);
            v3.AddComment(new Comment("Ada", "Can you share the slides?"));
            v3.AddComment(new Comment("Liam", "Great pacing."));
            v3.AddComment(new Comment("Mia", "Very helpful tips."));
            videos.Add(v3);

            foreach (var vid in videos)
            {
                Console.WriteLine(vid.ToDisplayString());
                Console.WriteLine();
            }
        }
    }
}
