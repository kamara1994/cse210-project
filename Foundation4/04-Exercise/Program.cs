using System;
using System.Collections.Generic;
using System.Globalization;

abstract class Exercise
{
    public DateTime Date { get; }
    public int Minutes { get; }
    protected Exercise(DateTime date, int minutes) { Date = date; Minutes = minutes; }
    public abstract double GetDistanceKm();
    public abstract double GetSpeedKph();
    public double GetPaceMinPerKm() => Minutes / Math.Max(GetDistanceKm(), 0.0001);
    public virtual string Summary()
    {
        var d = GetDistanceKm(); var s = GetSpeedKph(); var p = GetPaceMinPerKm();
        return $"{Date:dd MMM yyyy} {GetType().Name} ({Minutes} min) - Distance: {d:0.0} km, Speed: {s:0.0} kph, Pace: {p:0.0} min/km";
    }
}
class Running : Exercise
{
    private readonly double _distanceKm;
    public Running(DateTime date, int minutes, double distanceKm) : base(date, minutes) { _distanceKm = distanceKm; }
    public override double GetDistanceKm() => _distanceKm;
    public override double GetSpeedKph() => (_distanceKm / Minutes) * 60.0;
}
class Cycling : Exercise
{
    private readonly double _speedKph;
    public Cycling(DateTime date, int minutes, double speedKph) : base(date, minutes) { _speedKph = speedKph; }
    public override double GetDistanceKm() => _speedKph * (Minutes / 60.0);
    public override double GetSpeedKph() => _speedKph;
}
class Swimming : Exercise
{
    private readonly int _laps; // 50m pool
    public Swimming(DateTime date, int minutes, int laps) : base(date, minutes) { _laps = laps; }
    public override double GetDistanceKm() => (_laps * 50.0) / 1000.0;
    public override double GetSpeedKph() => GetDistanceKm() / (Minutes / 60.0);
}
class Program
{
    static void Main()
    {
        var log = new List<Exercise>
        {
            new Running(DateTime.Parse("2025-10-26", CultureInfo.InvariantCulture), 30, 5.0),
            new Cycling(DateTime.Parse("2025-10-27", CultureInfo.InvariantCulture), 40, 22.0),
            new Swimming(DateTime.Parse("2025-10-28", CultureInfo.InvariantCulture), 25, 30)
        };
        foreach (var ex in log) Console.WriteLine(ex.Summary());
    }
}
