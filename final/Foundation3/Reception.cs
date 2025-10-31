namespace Foundation3
{
    public class Reception : Event
    {
        public string RsvpEmail { get; }

        public Reception(string title, string description, System.DateTime when, Address where,
                         string rsvpEmail)
            : base(title, description, when, where)
        {
            RsvpEmail = rsvpEmail;
        }

        public override string FullDetails() =>
$@"--- Full ---
{Title}
{Description}
{When:yyyy-MM-dd @ h:mm tt}
{Where}
Type: Reception
RSVP: {RsvpEmail}";
    }
}
