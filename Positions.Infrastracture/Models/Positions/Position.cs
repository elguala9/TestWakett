using System;
using System.Collections.Generic;

namespace Positions.Infrastracture.Models.Positions;

public partial class Position
{
    public Guid Id { get; set; }

    public string Instrumentid { get; set; } = null!;

    public decimal Quantity { get; set; }

    public decimal Initialrate { get; set; }

    public int Side { get; set; }

    public DateTime Openedat { get; set; }

    public bool Isclosed { get; set; }

    public virtual ICollection<PositionValuation> PositionValuations { get; set; } = new List<PositionValuation>();
}
