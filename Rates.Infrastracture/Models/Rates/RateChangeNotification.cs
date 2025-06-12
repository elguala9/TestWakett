using System;
using System.Collections.Generic;

namespace Rates.Infrastracture.Models.Rates;

public partial class RateChangeNotification
{
    public Guid Id { get; set; }

    public string Symbol { get; set; } = null!;

    public decimal Oldrate { get; set; }

    public decimal Newrate { get; set; }

    public double Changepercent { get; set; }

    public DateTime Notifiedat { get; set; }
}
