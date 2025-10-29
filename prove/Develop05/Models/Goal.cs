namespace EternalQuest.Models;

// Base class shared by all goals.
// Encapsulation: fields are private/protected; exposed via properties.
public abstract class Goal
{
    private int _id;
    private string _name = "";
    private int _points;
    private string _notes = "";

    // Shared progress counter for types that repeat
    protected int TimesCompleted { get; set; }

    // Public properties (read-only where appropriate)
    public int Id { get => _id; set => _id = value; }
    public string Name { get => _name; set => _name = value; }
    public int Points { get => _points; init => _points = value; }
    public string Notes { get => _notes; set => _notes = value; }

    // Polymorphic surface – children must override these
    public abstract bool IsComplete { get; }
    public abstract int Record();     // returns points earned for this attempt
    public abstract string Display(); // one-line status for menus

// Compatibility helper if any callers expect this name:
public string GetDisplay() => Display();

// Convenience mirror for UIs that read "Completed"
public bool Completed => IsComplete; // one-line status for menus

    // Simple persistence hooks (CSV-style). Derived classes may append fields.
    public virtual string Serialize() =>
        $"{GetType().Name}|{Id}|{Name}|{Points}|{Notes}|{TimesCompleted}";

    public virtual void Deserialize(string[] parts)
    {
        // parts: [Type, Id, Name, Points, Notes, TimesCompleted, ...childFields]
        Id = int.Parse(parts[1]);
        Name = parts[2];
        _points = int.Parse(parts[3]);
        _notes = parts[4];
        TimesCompleted = int.Parse(parts[5]);
    }
}

