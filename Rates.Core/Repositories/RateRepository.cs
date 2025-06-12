using Rates.Infrastracture.Models.Rates;
using Microsoft.EntityFrameworkCore;

public class RateRepository : IRateRepository
{
    private readonly RatesContext _context;

    public RateRepository(RatesContext context)
    {
        _context = context;
    }

    public async Task AddRateAsync(Rate rate)
    {
        //rate.Id = Guid.Empty;
        _context.Rates.Add(rate);
        await _context.SaveChangesAsync();
    }

    public async Task<Rate?> GetLatestRateAsync(string symbol)
    {
        return await _context.Rates
            .Where(r => r.Symbol == symbol)
            .OrderByDescending(r => r.Timestamp)
            .FirstOrDefaultAsync();
    }

    public async Task<Rate?> GetOldestRateInLast24HoursAsync(string symbol)
    {
        var since = DateTime.UtcNow.AddHours(-24);
        return await _context.Rates
            .Where(r => r.Symbol == symbol && r.Timestamp >= since)
            .OrderBy(r => r.Timestamp)
            .FirstOrDefaultAsync();
    }
}
