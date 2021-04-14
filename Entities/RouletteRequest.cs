using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class RouletteRequest
    {
        public string RouletteID { get; set; }
        public string BetColor { get; set; }
        public Nullable<int> BetNumber { get; set; }
        public float Betmoney { get; set; }
    }
}
