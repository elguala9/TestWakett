using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rates.Core.DTO
{
    public class RateChangeNotificationDTO
    {
        public string Symbol { get; set; } = string.Empty;
        public decimal OldRate { get; set; }
        public decimal NewRate { get; set; }
        public float ChangePercent { get; set; }
    }
}
