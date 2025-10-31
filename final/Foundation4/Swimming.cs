namespace Foundation4
{
    public class Swimming : Activity
    {
        private readonly int _laps;
        private readonly int _lapMeters;

        public Swimming(System.DateTime date, int minutes, int laps, int lapMeters = 50)
            : base(date, minutes)
        {
            _laps = laps;
            _lapMeters = lapMeters;
        }

        public override double GetDistanceKm() => _laps * _lapMeters / 1000.0;

        public override double GetSpeedKph()
        {
            var hours = Minutes / 60.0;
            var km = GetDistanceKm();
            return hours <= 0 ? 0 : km / hours;
        }
    }
}
