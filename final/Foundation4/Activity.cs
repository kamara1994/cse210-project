using System;

namespace Foundation4
{
    public abstract class Activity
    {
        public DateTime Date { get; }
        public int Minutes { get; }

        protected Activity(DateTime date, int minutes)
        {
            Date = date;
            Minutes = minutes;
        }

        public abstract double GetDistanceKm();
        public abstract double GetSpeedKph();

        public virtual double GetPaceMinPerKm()
        {
            var dist = GetDistanceKm();
            return dist <= 0 ? 0 : Minutes / dist;
        }

        public virtual string Summary()
        {
            return $"{Date:dd MMM yyyy} {GetType().Name} ({Minutes} min) - " +
                   $"Distance: {GetDistanceKm():0.0} km, " +
                   $"Speed: {GetSpeedKph():0.0} kph, " +
                   $"Pace: {GetPaceMinPerKm():0.0} min/km";
        }
    }
}
