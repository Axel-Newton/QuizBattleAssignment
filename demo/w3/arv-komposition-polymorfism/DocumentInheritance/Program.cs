using System;

public class Document
{
    public string Title { get; set; }
    public string Content { get; set; }
}

public class PdfDocument : Document
{
    public void ExportToPdf()
    {
        Console.WriteLine($"Exporting '{Title}' to PDF...");
    }
}

class Program
{
    static void Main()
    {
        PdfDocument pdf = new PdfDocument { Title = "Rapport", Content = "Innehåll här" };
        pdf.ExportToPdf();
    }
}
