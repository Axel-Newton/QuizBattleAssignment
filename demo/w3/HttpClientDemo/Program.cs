
using System.Net.Http.Json;
using System.Text.Json;
using HttpClientDemo.Models;

// Enkla inställningar
var baseUrl = Environment.GetEnvironmentVariable("DEMO_API_BASE") ?? "http://localhost:5280"; // matchar API:ets http-port
var http = new HttpClient { BaseAddress = new Uri(baseUrl) };

Console.WriteLine($"Bas-URL: {http.BaseAddress}");
Console.WriteLine("
Meny:
1) Hämta alla items (GET /items)
2) Hämta item som ej finns (GET /items/9999)
3) Skapa item (POST /items)
4) Skapa duplikat (POST /items med redan taget namn)
5) Timeout-demo (GET /items/slow?ms=5000) med 2s timeout
6) Cancellation-demo (avbryt pågående request)
0) Avsluta");

while (true)
{
    Console.Write("
Val: ");
    var key = Console.ReadLine();
    if (key == "0" || key?.Equals("exit", StringComparison.OrdinalIgnoreCase) == true) break;

    try
    {
        switch (key)
        {
            case "1":
                await GetAll();
                break;
            case "2":
                await GetNotFound();
                break;
            case "3":
                await PostCreate("Min nya uppgift " + DateTime.Now.ToString("HHmmss"));
                break;
            case "4":
                await PostDuplicate("Läsa dokumentation"); // finns i seed
                break;
            case "5":
                await GetWithTimeout();
                break;
            case "6":
                await GetWithCancellation();
                break;
            default:
                Console.WriteLine("Okänt val.");
                break;
        }
    }
    catch (HttpRequestException ex)
    {
        Console.WriteLine($"HTTP-fel: {ex.Message}");
    }
    catch (TaskCanceledException ex)
    {
        Console.WriteLine($"Avbrutet/Timeout: {ex.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Oväntat fel: {ex.Message}");
    }
}

async Task GetAll()
{
    Console.WriteLine("→ GET /items");
    var resp = await http.GetAsync("/items");
    await Print(resp);
}

async Task GetNotFound()
{
    Console.WriteLine("→ GET /items/9999");
    var resp = await http.GetAsync("/items/9999");
    await Print(resp);
}

async Task PostCreate(string name)
{
    Console.WriteLine("→ POST /items (skapa nytt)");
    var payload = new { name, isDone = false };
    var resp = await http.PostAsJsonAsync("/items", payload);
    await Print(resp);
}

async Task PostDuplicate(string name)
{
    Console.WriteLine("→ POST /items (duplikat)");
    var payload = new { name, isDone = false };
    var resp = await http.PostAsJsonAsync("/items", payload);
    await Print(resp);
}

async Task GetWithTimeout()
{
    Console.WriteLine("→ GET /items/slow?ms=5000 med 2s timeout");
    using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(2));
    var resp = await http.GetAsync("/items/slow?ms=5000", cts.Token);
    await Print(resp);
}

async Task GetWithCancellation()
{
    Console.WriteLine("→ GET /items/slow?ms=5000 — tryck valfri tangent för att avbryta…");
    using var cts = new CancellationTokenSource();
    var requestTask = http.GetAsync("/items/slow?ms=5000", cts.Token);
    var cancelTask = Task.Run(() => { Console.ReadKey(true); cts.Cancel(); });
    var completed = await Task.WhenAny(requestTask, cancelTask);

    if (completed == cancelTask)
    {
        Console.WriteLine("Anrop avbrutet av användaren.");
        return;
    }

    var resp = await requestTask; // om inte avbrutet
    await Print(resp);
}

static async Task Print(HttpResponseMessage resp)
{
    Console.WriteLine($"Status: {(int)resp.StatusCode} {resp.StatusCode}");
    string body = string.Empty;
    try
    {
        body = await resp.Content.ReadAsStringAsync();
        if (!string.IsNullOrWhiteSpace(body))
        {
            using var jdoc = JsonDocument.Parse(body);
            body = JsonSerializer.Serialize(jdoc, new JsonSerializerOptions { WriteIndented = true });
        }
    }
    catch { }

    if (!string.IsNullOrWhiteSpace(body))
    {
        Console.WriteLine("Body:
" + body);
    }

    Console.WriteLine("Rekommenderad handling på klienten:");
    if ((int)resp.StatusCode >= 200 && (int)resp.StatusCode < 300)
        Console.WriteLine("✓ Lyckat – uppdatera UI/data.");
    else if ((int)resp.StatusCode == 404)
        Console.WriteLine("✗ NotFound – visa saknad resurs och föreslå åtgärd.");
    else if ((int)resp.StatusCode == 409)
        Console.WriteLine("⚠ Conflict – visa duplikatkonflikt och be om annat namn.");
    else if ((int)resp.StatusCode >= 400 && (int)resp.StatusCode < 500)
        Console.WriteLine("⚠ Klientfel – visa felmeddelande och validera indata.");
    else if ((int)resp.StatusCode >= 500)
        Console.WriteLine("⚠ Serverfel – visa ursäkt och försök igen senare.");
}
