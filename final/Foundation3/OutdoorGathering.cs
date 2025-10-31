namespace Foundation3
{
    public class OutdoorGathering : Event
    {
        public string Forecast { get; }

        public OutdoorGathering(string title, string description, System.DateTime when, Address where,
                                string forecast)
            : base(title, description, when, where)
        {
            Forecast = forecast;
        }

        public override string FullDetails() =>
$@"--- Full ---
{Title}
{Description}
{When:yyyy-MM-dd @ h:mm tt}
{Where}
Type: Outdoor
Forecast: {Forecast}";
    }
}
