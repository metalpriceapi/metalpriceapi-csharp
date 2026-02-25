using System.Net.Http;
using System.Text.Json;
using System.Web;

namespace MetalpriceAPI;

public class MetalpriceApiClient : IDisposable
{
    private static readonly Dictionary<string, string> Servers = new()
    {
        ["us"] = "https://api.metalpriceapi.com/v1",
        ["eu"] = "https://api-eu.metalpriceapi.com/v1"
    };

    private readonly string _apiKey;
    private readonly HttpClient _httpClient;
    private string _baseUrl;

    public MetalpriceApiClient(string apiKey, string server = "us")
    {
        _apiKey = apiKey;
        _baseUrl = Servers.GetValueOrDefault(server, Servers["us"])!;
        _httpClient = new HttpClient();
    }

    public void SetServer(string server)
    {
        _baseUrl = Servers.GetValueOrDefault(server, Servers["us"])!;
    }

    public async Task<JsonElement> FetchSymbolsAsync()
    {
        return await GetAsync("/symbols", new Dictionary<string, string>());
    }

    public async Task<JsonElement> FetchLiveAsync(string baseVal = "", List<string>? currencies = null,
        string unit = "", string purity = "", string math = "")
    {
        var parameters = RemoveEmpty(new Dictionary<string, string>
        {
            ["base"] = baseVal,
            ["currencies"] = currencies != null ? string.Join(",", currencies) : "",
            ["unit"] = unit,
            ["purity"] = purity,
            ["math"] = math
        });
        return await GetAsync("/latest", parameters);
    }

    public async Task<JsonElement> FetchHistoricalAsync(string date, string baseVal = "",
        List<string>? currencies = null, string unit = "")
    {
        var parameters = RemoveEmpty(new Dictionary<string, string>
        {
            ["base"] = baseVal,
            ["currencies"] = currencies != null ? string.Join(",", currencies) : "",
            ["unit"] = unit
        });
        return await GetAsync("/" + date, parameters);
    }

    public async Task<JsonElement> HourlyAsync(string baseVal = "", string currency = "", string unit = "",
        string startDate = "", string endDate = "", string math = "", string dateType = "")
    {
        var parameters = RemoveEmpty(new Dictionary<string, string>
        {
            ["base"] = baseVal,
            ["currency"] = currency,
            ["unit"] = unit,
            ["start_date"] = startDate,
            ["end_date"] = endDate,
            ["math"] = math,
            ["date_type"] = dateType
        });
        return await GetAsync("/hourly", parameters);
    }

    public async Task<JsonElement> FetchOHLCAsync(string baseVal = "", string currency = "", string date = "",
        string unit = "", string dateType = "")
    {
        var parameters = RemoveEmpty(new Dictionary<string, string>
        {
            ["base"] = baseVal,
            ["currency"] = currency,
            ["date"] = date,
            ["unit"] = unit,
            ["date_type"] = dateType
        });
        return await GetAsync("/ohlc", parameters);
    }

    public async Task<JsonElement> ConvertAsync(string fromCurrency = "", string toCurrency = "",
        object? amount = null, string date = "", string unit = "")
    {
        var parameters = RemoveEmpty(new Dictionary<string, string>
        {
            ["from"] = fromCurrency,
            ["to"] = toCurrency,
            ["amount"] = amount?.ToString() ?? "",
            ["date"] = date,
            ["unit"] = unit
        });
        return await GetAsync("/convert", parameters);
    }

    public async Task<JsonElement> TimeframeAsync(string startDate, string endDate, string baseVal = "",
        List<string>? currencies = null, string unit = "")
    {
        var parameters = RemoveEmpty(new Dictionary<string, string>
        {
            ["start_date"] = startDate,
            ["end_date"] = endDate,
            ["base"] = baseVal,
            ["currencies"] = currencies != null ? string.Join(",", currencies) : "",
            ["unit"] = unit
        });
        return await GetAsync("/timeframe", parameters);
    }

    public async Task<JsonElement> ChangeAsync(string startDate, string endDate, string baseVal = "",
        List<string>? currencies = null, string dateType = "")
    {
        var parameters = RemoveEmpty(new Dictionary<string, string>
        {
            ["start_date"] = startDate,
            ["end_date"] = endDate,
            ["base"] = baseVal,
            ["currencies"] = currencies != null ? string.Join(",", currencies) : "",
            ["date_type"] = dateType
        });
        return await GetAsync("/change", parameters);
    }

    public async Task<JsonElement> CaratAsync(string baseVal = "", string currency = "", string date = "")
    {
        var parameters = RemoveEmpty(new Dictionary<string, string>
        {
            ["base"] = baseVal,
            ["currency"] = currency,
            ["date"] = date
        });
        return await GetAsync("/carat", parameters);
    }

    public async Task<JsonElement> UsageAsync()
    {
        return await GetAsync("/usage", new Dictionary<string, string>());
    }

    private static Dictionary<string, string> RemoveEmpty(Dictionary<string, string> parameters)
    {
        return parameters
            .Where(kvp => !string.IsNullOrEmpty(kvp.Value))
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
    }

    private async Task<JsonElement> GetAsync(string endpoint, Dictionary<string, string> parameters)
    {
        var query = HttpUtility.ParseQueryString(string.Empty);
        foreach (var kvp in parameters)
        {
            query[kvp.Key] = kvp.Value;
        }
        query["api_key"] = _apiKey;

        var url = $"{_baseUrl}{endpoint}?{query}";
        var response = await _httpClient.GetStringAsync(url);
        return JsonDocument.Parse(response).RootElement;
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }
}
