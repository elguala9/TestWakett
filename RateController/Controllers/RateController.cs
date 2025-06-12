using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[controller]")]
public class RatesController : ControllerBase
{
    private readonly IRatesService _ratesService;

    public RatesController(IRatesService ratesService)
    {
        _ratesService = ratesService;
    }

    [HttpPost("fetch")]
    public async Task<IActionResult> FetchRates()
    {
        await _ratesService.FetchAndProcessRatesAsync();
        return Ok("Rates fetched and processed.");
    }

    [HttpGet("{symbol}")]
    public async Task<IActionResult> GetLatestRate(string symbol)
    {
        var rate = await _ratesService.GetLatestRateAsync(symbol);
        if (rate == null)
            return NotFound();

        return Ok(rate);
    }
}
