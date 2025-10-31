namespace Foundation3
{
    public class Lecture : Event
    {
        public string Speaker { get; }
        public int Capacity { get; }

        public Lecture(string title, string description, System.DateTime when, Address where,
                       string speaker, int capacity)
            : base(title, description, when, where)
        {
            Speaker = speaker;
            Capacity = capacity;
        }

        public override string FullDetails() =>
$@"--- Full ---
{Title}
{Description}
{When:yyyy-MM-dd @ h:mm tt}
{Where}
Type: Lecture
Speaker: {Speaker}
Capacity: {Capacity}";
    }
}
