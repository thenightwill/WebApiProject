using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Roulette
    {
        public string IdRoulette { get; set; }
        public bool RouletteOpenState { get; set; }
        public bool RouletteEndGameState { get; set; }
        public List<Bet> BetsList { get; set; }
        public string WinnerColor { get; set; }
        public int WinnerNumber { get; set; }
        public float TotalMoney { get; set; }
        public string DateOpenState { get; set; }
        public string DateEndGame { get; set; }


    }
}
