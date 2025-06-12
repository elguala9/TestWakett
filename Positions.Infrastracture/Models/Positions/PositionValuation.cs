using System;
using System.Collections.Generic;

namespace Positions.Infrastracture.Models.Positions;

public partial class PositionValuation
{
    public Guid Id { get; set; }

    public Guid Positionid { get; set; }

    public decimal Currentrate { get; set; }

    public decimal Profitloss { get; set; }

    public DateTime Calculatedat { get; set; }

    public virtual Position Position { get; set; } = null!;
}
