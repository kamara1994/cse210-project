using System;

abstract class Event
{
    public string Title, Description, Date, Time, Address;
    protected Event(string title, string desc, string date, string time, string address)
    { Title = title; Description = desc; Date = date; Time = time; Address = address; }
    public string StandardDetails() => $"{Title}\n{Description}\n{Date} @ {Time}\n{Address}";
    public abstract string FullDetails();
    public abstract string ShortDescription();
}
class Lecture : Event
{
    public string Speaker; public int Capacity;
    public Lecture(string title, string desc, string date, string time, string address, string speaker, int cap)
        : base(title, desc, date, time, address) { Speaker = speaker; Capacity = cap; }
    public override string FullDetails() => $"{StandardDetails()}\nType: Lecture\nSpeaker: {Speaker}\nCapacity: {Capacity}";
    public override string ShortDescription() => $"Lecture — {Title} — {Date}";
}
class Reception : Event
{
    public string RsvpEmail;
    public Reception(string title, string desc, string date, string time, string address, string email)
        : base(title, desc, date, time, address) { RsvpEmail = email; }
    public override string FullDetails() => $"{StandardDetails()}\nType: Reception\nRSVP: {RsvpEmail}";
    public override string ShortDescription() => $"Reception — {Title} — {Date}";
}
class Outdoor : Event
{
    public string Weather;
    public Outdoor(string title, string desc, string date, string time, string address, string weather)
        : base(title, desc, date, time, address) { Weather = weather; }
    public override string FullDetails() => $"{StandardDetails()}\nType: Outdoor\nForecast: {Weather}";
    public override string ShortDescription() => $"Outdoor — {Title} — {Date}";
}
class Program
{
    static void Main()
    {
        Event[] evts = {
            new Lecture("Clean Code 101","Intro to SOLID","2025-10-30","10:00 AM","Austin Auditorium","Dr. Jensen",120),
            new Reception("Founders Mixer","Meet & Greet","2025-11-02","6:00 PM","BYU-I Ballroom","rsvp@nimbus.example"),
            new Outdoor("Team Hike","Scenic loop trail","2025-11-09","8:00 AM","Kelly Canyon","Sunny, light breeze")
        };
        foreach (var e in evts)
        {
            Console.WriteLine("\n--- Standard ---");
            Console.WriteLine(e.StandardDetails());
            Console.WriteLine("\n--- Full ---");
            Console.WriteLine(e.FullDetails());
            Console.WriteLine("\n--- Short ---");
            Console.WriteLine(e.ShortDescription());
        }
    }
}
