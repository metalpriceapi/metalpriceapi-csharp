using MetalpriceAPI;

string apiKey = "REPLACE_ME";
using var client = new MetalpriceApiClient(apiKey);

// Or use EU server:
// using var client = new MetalpriceApiClient(apiKey, "eu");

var result = await client.FetchSymbolsAsync();
Console.WriteLine(result);

result = await client.FetchLiveAsync("USD", new List<string> { "XAU", "XAG", "XPD", "XPT" }, "troy_oz");
Console.WriteLine(result);

result = await client.FetchHistoricalAsync("2024-02-05", "USD", new List<string> { "XAU", "XAG", "XPD", "XPT" }, "troy_oz");
Console.WriteLine(result);

result = await client.HourlyAsync("USD", "XAU", "troy_oz", "2025-11-03", "2025-11-03");
Console.WriteLine(result);

result = await client.FetchOHLCAsync("USD", "XAU", "2024-02-06", "troy_oz");
Console.WriteLine(result);

result = await client.ConvertAsync("USD", "EUR", 100, "2024-02-05");
Console.WriteLine(result);

result = await client.TimeframeAsync("2024-02-05", "2024-02-06", "USD", new List<string> { "XAU", "XAG", "XPD", "XPT" }, "troy_oz");
Console.WriteLine(result);

result = await client.ChangeAsync("2024-02-05", "2024-02-06", "USD", new List<string> { "XAU", "XAG", "XPD", "XPT" });
Console.WriteLine(result);

result = await client.CaratAsync("USD", "XAU", "2024-02-06");
Console.WriteLine(result);

result = await client.UsageAsync();
Console.WriteLine(result);
