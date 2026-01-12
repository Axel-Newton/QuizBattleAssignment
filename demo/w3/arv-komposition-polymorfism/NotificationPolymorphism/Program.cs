using System;
using System.Collections.Generic;

public interface INotification
{
    void Send(string message);
}

public class EmailNotification : INotification
{
    public void Send(string message) => Console.WriteLine($"Email sent: {message}");
}

public class SmsNotification : INotification
{
    public void Send(string message) => Console.WriteLine($"SMS sent: {message}");
}

class Program
{
    static void Main()
    {
        var notifications = new List<INotification>
        {
            new EmailNotification(),
            new SmsNotification()
        };

        foreach (var n in notifications)
        {
            n.Send("System update at 18:00");
        }
    }
}
