using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Bet
    {
        public string Color { get; set; }

        public Nullable<int> Number { get; set; }

        public float BetMoney { get; set; }
        public string IdClient { get; set; }


    }
}
