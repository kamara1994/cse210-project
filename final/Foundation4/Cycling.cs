namespace Foundation4
{
    public class Cycling : Activity
    {
        private readonly double _speedKph;

        public Cycling(System.DateTime date, int minutes, double speedKph)
            : base(date, minutes)
        {
            _speedKph = speedKph;
        }

        public override double GetDistanceKm()
        {
            var hours = Minutes / 60.0;
            return _speedKph * hours;
        }

        public override double GetSpeedKph() => _speedKph;
    }
}
