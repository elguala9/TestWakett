using Rates.Infrastracture.Models.Rates;
using System.Threading.Tasks;

public interface IRatesService
{
    Task FetchAndProcessRatesAsync();
    Task<Rate?> GetLatestRateAsync(string symbol);
}
