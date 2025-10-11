public class Assignment
{
    private string _studentName;
    private string _topic;

    public Assignment(string studentName, string topic)
    {
        _studentName = studentName;
        _topic = topic;
    }

    public string GetSummary() => $"{_studentName} - {_topic}";

    // Expose student name for derived classes
    public string GetStudentName() => _studentName;
}
