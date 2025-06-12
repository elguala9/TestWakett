using System.Net.Http.Json;
using System.Text.Json;
using Xunit;

public class PositionsApiTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public PositionsApiTests(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateAndGetPosition_ShouldSucceed()
    {
        var newPosition = new
        {
            InstrumentId = "BTC/USD",
            Quantity = 1.5m,
            InitialRate = 45000.00m,
            Side = 1
        };

        var postResponse = await _client.PostAsJsonAsync("/api/positions", newPosition);
        postResponse.EnsureSuccessStatusCode();

        var getResponse = await _client.GetAsync("/api/positions");
        var list = await getResponse.Content.ReadAsStringAsync();

        Assert.Contains("BTC/USD", list);
    }

    [Fact]
    public async Task DeletePosition_ShouldReturnSuccess()
    {
        // crea posizione
        var create = new
        {
            InstrumentId = "ETH/USD",
            Quantity = 2.0m,
            InitialRate = 3000.00m,
            Side = -1
        };

        var post = await _client.PostAsJsonAsync("/api/positions", create);
        var content = await post.Content.ReadAsStringAsync();
        var createdId = ExtractId(content); // implementa helper JSON parsing

        var delResponse = await _client.DeleteAsync($"/api/positions/{createdId}");
        delResponse.EnsureSuccessStatusCode();
    }

    private Guid ExtractId(string json)
    {
        using var doc = JsonDocument.Parse(json);
        return doc.RootElement.GetProperty("id").GetGuid();
    }
}
