using Positions.Core.DTO;
using Positions.Infrastracture.Models.Positions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPositionsService
{
    Task<IEnumerable<Position>> GetAllAsync();
    Task<Position?> GetByIdAsync(Guid id);
    Task<Position> CreateAsync(Position request);
    Task<bool> CloseAsync(Guid id);
    Task RecalculatePositionsAsync(RateChangeNotification rateChange);
}
