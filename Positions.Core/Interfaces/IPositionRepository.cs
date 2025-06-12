using Positions.Infrastracture.Models.Positions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPositionRepository
{
    Task<IEnumerable<Position>> GetAllAsync();
    Task<Position?> GetByIdAsync(Guid id);
    Task AddAsync(Position position);
    Task<bool> CloseAsync(Guid id);
    Task<IEnumerable<Position>> GetBySymbolAsync(string symbol);
    Task AddValuationAsync(PositionValuation valuation);
}
