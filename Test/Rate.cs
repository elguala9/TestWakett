using System.Net.Http.Json;
using Xunit;

public class RatesApiTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public RatesApiTests(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateExternalClient();
    }

    [Fact]
    public async Task FetchRates_ShouldReturnSuccess()
    {
        var response = await _client.PostAsync("/api/Rates/fetch", null);
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GetRateBySymbol_ShouldReturnExpectedSymbol()
    {
        // Arrange: inserisci simbolo noto
        var symbol = "BTC";

        // Act
        var response = await _client.GetAsync($"/api/Rates/{symbol}");
        var json = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Contains(symbol, json);
    }

}
