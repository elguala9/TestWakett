using Rates.Infrastracture.Models.Rates;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRateRepository
{
    Task AddRateAsync(Rate rate);
    Task<Rate?> GetLatestRateAsync(string symbol);
    Task<Rate?> GetOldestRateInLast24HoursAsync(string symbol);
}
