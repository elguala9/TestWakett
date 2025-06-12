using Rates.Core.DTO;
using Rates.Infrastracture.Models.Rates;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

public class RatesService : IRatesService
{
    private readonly IRateRepository _rateRepo;
    private readonly IHttpClientFactory _httpClientFactory;

    public RatesService(IRateRepository rateRepo, IHttpClientFactory httpClientFactory)
    {
        _rateRepo = rateRepo;
        _httpClientFactory = httpClientFactory;
    }

    public async Task FetchAndProcessRatesAsync()
    {
        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", "bdc38d3b-6410-44a6-8c0e-6d748603d2e2");

        // Aggiungi header per accettare JSON
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var response = await client.GetAsync("https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest?convert=USD");

        if (!response.IsSuccessStatusCode) return;

        var json = await response.Content.ReadAsStringAsync();
        var data = ParseRatesFromJson(json); // parsing

        foreach (var rate in data)
        {
            await _rateRepo.AddRateAsync(rate);

            var oldRate = await _rateRepo.GetOldestRateInLast24HoursAsync(rate.Symbol);
            if (oldRate != null)
            {
                var change = (double)Math.Abs((rate.Value - oldRate.Value) / oldRate.Value * 100);
                if (change > 5)
                {
                    var notification = new RateChangeNotificationDTO
                    {
                        Symbol = rate.Symbol,
                        OldRate = oldRate.Value,
                        NewRate = rate.Value,
                        ChangePercent = Convert.ToSingle(change)
                    };

                    try
                    {
                        var httpClient = _httpClientFactory.CreateClient();
                        var response_position = await httpClient.PostAsJsonAsync("http://position-controller:8080/api/Positions/rate-changed", notification);

                        if (!response_position.IsSuccessStatusCode)
                        {
                            // Log errore, oppure retry/logica fallback
                            Console.WriteLine($"Failed to notify PositionsService: {response_position.StatusCode}");
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log exception
                        Console.WriteLine($"Exception while notifying PositionsService: {ex.Message}");
                    }
                }
            }
        }
    }

    public Task<Rate?> GetLatestRateAsync(string symbol) =>
        _rateRepo.GetLatestRateAsync(symbol);

    private IEnumerable<Rate> ParseRatesFromJson(string json)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        using var document = JsonDocument.Parse(json);
        var root = document.RootElement;

        if (!root.TryGetProperty("data", out JsonElement dataElement))
            return Enumerable.Empty<Rate>();

        var rates = new List<Rate>();

        foreach (var item in dataElement.EnumerateArray())
        {
            var symbol = item.GetProperty("symbol").GetString();
            var quote = item.GetProperty("quote");
            var usd = quote.GetProperty("USD");
            var price = usd.GetProperty("price").GetDecimal();
            var lastUpdated = usd.GetProperty("last_updated").GetDateTime();

            rates.Add(new Rate
            {
                Symbol = symbol!,
                Value = price,
                Timestamp = lastUpdated
            });
        }

        return rates;
    }
}
