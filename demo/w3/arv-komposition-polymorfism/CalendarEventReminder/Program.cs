using System;
using System.Collections.Generic;

public class CalendarEvent
{
    public string Title { get; set; }
    public DateTime Date { get; set; }
}

public class MeetingEvent : CalendarEvent
{
    public List<string> Participants { get; set; } = new List<string>();
}

public class ReminderService
{
    public void SendReminder(string message) => Console.WriteLine($"Reminder: {message}");
}

class Program
{
    static void Main()
    {
        var meeting = new MeetingEvent { Title = "Projektmöte", Date = DateTime.Now.AddHours(2) };
        meeting.Participants.Add("Anna");
        meeting.Participants.Add("Johan");

        var reminder = new ReminderService();
        reminder.SendReminder($"Mötet '{meeting.Title}' börjar kl {meeting.Date}");
    }
}
