using System;
using System.Collections.Generic;

class Comment
{
    public string Author { get; }
    public string Text { get; }
    public Comment(string author, string text) { Author = author; Text = text; }
}
class Video
{
    public string Title { get; }
    public string Author { get; }
    public int LengthSeconds { get; }
    private readonly List<Comment> _comments = new();
    public Video(string title, string author, int lengthSeconds)
    { Title = title; Author = author; LengthSeconds = lengthSeconds; }
    public void AddComment(string author, string text) => _comments.Add(new Comment(author, text));
    public int CommentCount() => _comments.Count;
    public IEnumerable<Comment> Comments() => _comments;
}
class Program
{
    static void Main()
    {
        var vids = new List<Video>
        {
            new Video("Faith & Works", "Joseph A. Kamara", 420),
            new Video("Shipping Demo", "BYU-I CSE210", 300),
            new Video("Planner Walkthrough", "Team Rexburg", 375),
        };
        vids[0].AddComment("Amaka","Loved the scripture tie-in!");
        vids[0].AddComment("Samuel","Clear explanation.");
        vids[1].AddComment("Kofi","Labels looked professional.");
        vids[2].AddComment("Ada","Can you share the slides?");
        vids[2].AddComment("Liam","Great pacing.");
        vids[2].AddComment("Mia","Very helpful tips.");

        foreach (var v in vids)
        {
            Console.WriteLine($"\nTitle: {v.Title}  | Author: {v.Author}  | Length: {v.LengthSeconds}s");
            Console.WriteLine($"Comments ({v.CommentCount()}):");
            foreach (var c in v.Comments())
                Console.WriteLine($"  - {c.Author}: {c.Text}");
        }
    }
}
