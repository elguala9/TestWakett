using Positions.Core.DTO;
using Positions.Infrastracture.Models.Positions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Positions.Core.Interfaces
{
    public interface IPositionsService
    {
        Task<IEnumerable<Position>> GetAllAsync();
        Task<Position?> GetByIdAsync(Guid id);
        Task<Position> CreateAsync(PositionCreateRequest request);
        Task<bool> CloseAsync(Guid id);
        Task RecalculatePositionsAsync(RateChangeNotification rateChange);
    }


}
