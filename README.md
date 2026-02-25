# MetalpriceAPI

MetalpriceAPI is the official .NET wrapper for MetalpriceAPI.com. This allows you to quickly integrate our metal price API and foreign exchange rate API into your application. Check https://metalpriceapi.com documentation for more information.

## Installation

### NuGet

```
dotnet add package MetalpriceAPI
```

Or add to your `.csproj`:

```xml
<PackageReference Include="MetalpriceAPI" Version="1.3.0" />
```

## Usage

```csharp
using MetalpriceAPI;

string apiKey = "SET_YOUR_API_KEY_HERE";
using var client = new MetalpriceApiClient(apiKey);

// Or use EU server:
// using var client = new MetalpriceApiClient(apiKey, "eu");
```
---
## Server Regions

MetalpriceAPI provides two regional endpoints. Choose the one closest to your servers for optimal performance.

| Region | Base URL |
|--------|----------|
| United States (default) | `https://api.metalpriceapi.com/v1` |
| Europe | `https://api-eu.metalpriceapi.com/v1` |

```csharp
using MetalpriceAPI;

// Default (US)
using var client = new MetalpriceApiClient("SET_YOUR_API_KEY_HERE");

// Europe
using var client = new MetalpriceApiClient("SET_YOUR_API_KEY_HERE", "eu");
```

---
## Documentation

#### FetchSymbolsAsync()
```csharp
await client.FetchSymbolsAsync();
```

[Link](https://metalpriceapi.com/documentation#api_symbol)

---
#### SetServer(server)

- `server` <[string]> Pass `"eu"` to use the EU server (`api-eu.metalpriceapi.com`), or `"us"` for the US server. Defaults to US if not specified.

```csharp
client.SetServer("eu");
```

---
#### FetchLiveAsync(baseVal, currencies, unit, purity, math)

- `baseVal` <[string]> Optional. Pass in a base currency, defaults to USD.
- `currencies` <[List]<[string]>> Optional. Pass in a list of currencies to return values for.
- `unit` <[string]> Optional. Pass in a unit for metal prices (e.g. `troy_oz`, `gram`, `kilogram`).
- `purity` <[string]> Optional. Pass in a purity level for metal prices.
- `math` <[string]> Optional. Pass in a math expression to apply to the rates.

```csharp
await client.FetchLiveAsync("USD", new List<string> { "XAU", "XAG", "XPD", "XPT" }, "troy_oz");
```

[Link](https://metalpriceapi.com/documentation#api_realtime)

---
#### FetchHistoricalAsync(date, baseVal, currencies, unit)

- `date` <[string]> Required. Pass in a string with format `YYYY-MM-DD`
- `baseVal` <[string]> Optional. Pass in a base currency, defaults to USD.
- `currencies` <[List]<[string]>> Optional. Pass in a list of currencies to return values for.
- `unit` <[string]> Optional. Pass in a unit for metal prices (e.g. `troy_oz`, `gram`, `kilogram`).

```csharp
await client.FetchHistoricalAsync("2024-02-05", "USD", new List<string> { "XAU", "XAG", "XPD", "XPT" }, "troy_oz");
```

[Link](https://metalpriceapi.com/documentation#api_historical)

---
#### HourlyAsync(baseVal, currency, unit, startDate, endDate, math, dateType)

- `baseVal` <[string]> Optional. Pass in a base currency, defaults to USD.
- `currency` <[string]> Required. Specify currency you would like to get hourly rates for.
- `unit` <[string]> Optional. Pass in a unit for metal prices (e.g. `troy_oz`, `gram`, `kilogram`).
- `startDate` <[string]> Required. Specify the start date using the format `YYYY-MM-DD`.
- `endDate` <[string]> Required. Specify the end date using the format `YYYY-MM-DD`.
- `math` <[string]> Optional. Pass in a math expression to apply to the rates.
- `dateType` <[string]> Optional. Pass in a date type, overrides date parameters if passed in.

```csharp
await client.HourlyAsync("USD", "XAU", "troy_oz", "2025-11-03", "2025-11-03");
```

[Link](https://metalpriceapi.com/documentation#api_hourly)

---
#### FetchOHLCAsync(baseVal, currency, date, unit, dateType)

- `baseVal` <[string]> Optional. Pass in a base currency, defaults to USD.
- `currency` <[string]> Required. Specify currency you would like to get OHLC for.
- `date` <[string]> Required. Specify date to get OHLC for specific date using format `YYYY-MM-DD`.
- `unit` <[string]> Optional. Pass in a unit, defaults to troy_oz.
- `dateType` <[string]> Optional. Pass in a date type, overrides date parameter if passed in.

```csharp
await client.FetchOHLCAsync("USD", "XAU", "2024-02-05", "troy_oz");
```

[Link](https://metalpriceapi.com/documentation#api_ohlc)

---
#### ConvertAsync(fromCurrency, toCurrency, amount, date, unit)

- `fromCurrency` <[string]> Optional. Pass in a base currency, defaults to USD.
- `toCurrency` <[string]> Required. Specify currency you would like to convert to.
- `amount` <[number]> Required. The amount to convert.
- `date` <[string]> Optional. Specify date to use historical midpoint value for conversion with format `YYYY-MM-DD`. Otherwise, it will use live exchange rate date if value not passed in.
- `unit` <[string]> Optional. Pass in a unit for metal prices (e.g. `troy_oz`, `gram`, `kilogram`).

```csharp
await client.ConvertAsync("USD", "EUR", 100, "2024-02-05");
```

[Link](https://metalpriceapi.com/documentation#api_convert)

---
#### TimeframeAsync(startDate, endDate, baseVal, currencies, unit)

- `startDate` <[string]> Required. Specify the start date of your timeframe using the format `YYYY-MM-DD`.
- `endDate` <[string]> Required. Specify the end date of your timeframe using the format `YYYY-MM-DD`.
- `baseVal` <[string]> Optional. Pass in a base currency, defaults to USD.
- `currencies` <[List]<[string]>> Optional. Pass in a list of currencies to return values for.
- `unit` <[string]> Optional. Pass in a unit for metal prices (e.g. `troy_oz`, `gram`, `kilogram`).

```csharp
await client.TimeframeAsync("2024-02-05", "2024-02-06", "USD", new List<string> { "XAU", "XAG", "XPD", "XPT" }, "troy_oz");
```

[Link](https://metalpriceapi.com/documentation#api_timeframe)

---
#### ChangeAsync(startDate, endDate, baseVal, currencies, dateType)

- `startDate` <[string]> Required. Specify the start date of your timeframe using the format `YYYY-MM-DD`.
- `endDate` <[string]> Required. Specify the end date of your timeframe using the format `YYYY-MM-DD`.
- `baseVal` <[string]> Optional. Pass in a base currency, defaults to USD.
- `currencies` <[List]<[string]>> Optional. Pass in a list of currencies to return values for.
- `dateType` <[string]> Optional. Pass in a date type, overrides date parameters if passed in.

```csharp
await client.ChangeAsync("2024-02-05", "2024-02-06", "USD", new List<string> { "XAU", "XAG", "XPD", "XPT" });
```

[Link](https://metalpriceapi.com/documentation#api_change)

---
#### CaratAsync(baseVal, currency, date)

- `baseVal` <[string]> Optional. Pass in a base currency, defaults to USD.
- `currency` <[string]> Optional. Pass in a metal code to get carat prices for (defaults to XAU).
- `date` <[string]> Optional. Specify date to get Carat for specific date using format `YYYY-MM-DD`. If not specified, uses live rates.

```csharp
await client.CaratAsync("USD", "XAU", "2024-02-05");
```

[Link](https://metalpriceapi.com/documentation#api_carat)

---
#### UsageAsync()
```csharp
await client.UsageAsync();
```

[Link](https://metalpriceapi.com/documentation#api_usage)

---
**[Official documentation](https://metalpriceapi.com/documentation)**

---
## FAQ

- How do I get an API Key?

    Free API Keys are available [here](https://metalpriceapi.com).

- I want more information

    Checkout our FAQs [here](https://metalpriceapi.com/faq).


## Support

For support, get in touch using [this form](https://metalpriceapi.com/contact).


[List]: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'List'
[number]: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/numeric-types 'Number'
[string]: https://learn.microsoft.com/en-us/dotnet/api/system.string 'String'
