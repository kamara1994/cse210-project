using System;

namespace Foundation3
{
    public abstract class Event
    {
        public string Title { get; }
        public string Description { get; }
        public DateTime When { get; }
        public Address Where { get; }

        protected Event(string title, string description, DateTime when, Address where)
        {
            Title = title;
            Description = description;
            When = when;
            Where = where;
        }

        public virtual string StandardDetails() =>
$@"--- Standard ---
{Title}
{Description}
{When:yyyy-MM-dd @ h:mm tt}
{Where}";

        public abstract string FullDetails();

        public virtual string ShortDescription(string type) =>
$@"--- Short ---
{type} — {Title} — {When:yyyy-MM-dd}";
    }
}
