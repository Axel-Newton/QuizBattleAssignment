using System;
using System.Collections.Generic;

public interface IReportGenerator
{
    void GenerateReport();
}

public class SalesReportGenerator : IReportGenerator
{
    public void GenerateReport() => Console.WriteLine("Generating Sales Report...");
}

public class InventoryReportGenerator : IReportGenerator
{
    public void GenerateReport() => Console.WriteLine("Generating Inventory Report...");
}

class Program
{
    static void Main()
    {
        var reports = new List<IReportGenerator>
        {
            new SalesReportGenerator(),
            new InventoryReportGenerator()
        };

        foreach (var report in reports)
        {
            report.GenerateReport();
        }
    }
}
