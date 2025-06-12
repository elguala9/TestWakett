using Positions.Infrastracture.Models.Positions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Positions.Core.DTO
{
    public class PositionCreateRequest
    {
        public string InstrumentId { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public decimal InitialRate { get; set; }
        public int Side { get; set; } // +1: BUY, -1: SELL

        static public Position ToPosition(PositionCreateRequest request)
        {
            var position = new Position
            {
                Id = Guid.NewGuid(),
                Instrumentid = request.InstrumentId,
                Quantity = request.Quantity,
                Initialrate = request.InitialRate,
                Side = request.Side,
                Openedat = DateTime.UtcNow,
                Isclosed = false
            };
            return position;
        }
    }
}
