# W04 – Inheritance Design Activity (Mindfulness App)

## Class Diagram
```mermaid
classDiagram
    class Activity {
      -string _name
      -string _description
      -int _durationSeconds
      +Activity(string name, string description, int durationSeconds)
      +Start() void
      +ShowIntro() void
      +ShowOutro() void
      +SetDuration(int seconds) void
      +GetDuration() int
      +Spinner(int seconds) void
      +Countdown(int seconds) void
      <<base>>
    }

    class BreathingActivity {
      -int _inhaleSeconds
      -int _exhaleSeconds
      +BreathingActivity(int duration, int inhaleSeconds, int exhaleSeconds)
      +Run() void
    }

    class ReflectionActivity {
      -List~string~ _prompts
      -List~string~ _questions
      +ReflectionActivity(int duration)
      +Run() void
      +LoadDefaultPrompts() void
      +LoadDefaultQuestions() void
      +GetRandomPrompt() string
    }

    class ListingActivity {
      -string _topic
      -List~string~ _items
      +ListingActivity(int duration, string topic)
      +Run() void
      +AddItem(string item) void
      +GetCount() int
    }

    Activity <|-- BreathingActivity
    Activity <|-- ReflectionActivity
    Activity <|-- ListingActivity
