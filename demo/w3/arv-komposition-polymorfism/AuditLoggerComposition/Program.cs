using System;

public class AuditLogger
{
    public void LogChange(string message) => Console.WriteLine($"AUDIT: {message}");
}

public class OrderService
{
    private readonly AuditLogger _auditLogger;

    public OrderService(AuditLogger auditLogger)
    {
        _auditLogger = auditLogger;
    }

    public void PlaceOrder(int orderId)
    {
        Console.WriteLine($"Order {orderId} placed.");
        _auditLogger.LogChange($"Order {orderId} was placed.");
    }
}

class Program
{
    static void Main()
    {
        var service = new OrderService(new AuditLogger());
        service.PlaceOrder(123);
    }
}
