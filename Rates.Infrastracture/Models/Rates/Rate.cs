using System;
using System.Collections.Generic;

namespace Rates.Infrastracture.Models.Rates;

public partial class Rate
{
    public Guid Id { get; set; }

    public string Symbol { get; set; } = null!;

    public decimal Value { get; set; }

    public DateTime Timestamp { get; set; }
}
