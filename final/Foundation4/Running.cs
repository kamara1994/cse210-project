namespace Foundation4
{
    public class Running : Activity
    {
        private readonly double _distanceKm;

        public Running(System.DateTime date, int minutes, double distanceKm)
            : base(date, minutes)
        {
            _distanceKm = distanceKm;
        }

        public override double GetDistanceKm() => _distanceKm;

        public override double GetSpeedKph()
        {
            var hours = Minutes / 60.0;
            return hours <= 0 ? 0 : _distanceKm / hours;
        }
    }
}
