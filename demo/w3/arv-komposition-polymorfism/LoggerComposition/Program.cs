using System;

public class Logger
{
    public void Log(string message) => Console.WriteLine($"LOG: {message}");
}

public class UserService
{
    private readonly Logger _logger;

    public UserService(Logger logger)
    {
        _logger = logger;
    }

    public void CreateUser(string name)
    {
        Console.WriteLine($"User {name} created.");
        _logger.Log($"User {name} was created.");
    }
}

class Program
{
    static void Main()
    {
        var service = new UserService(new Logger());
        service.CreateUser("Anna");
    }
}
