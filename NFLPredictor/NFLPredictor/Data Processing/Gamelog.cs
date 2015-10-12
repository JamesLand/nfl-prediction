using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFLPredictor.Data_Processing
{
    public class Gamelog
    {
        public int Week { get; set; }
        public String Opponent { get; set; }
        public bool DidTeamWin { get; set; }
        public bool WasAtHome { get; set; }
        public int TeamScore { get; set; }
        public int OpposingTeamScore { get; set; }
        public int OffenseFirstDowns { get; set; }
        public int OffenseTotalYards { get; set; }
        public int OffensePassYards { get; set; }
        public int OffenseRushYards { get; set; }
        public int OffenseTurnovers { get; set; }
        public int DefenseFirstDowns { get; set; }
        public int DefenseTotalYards { get; set; }
        public int DefensePassYards { get; set; }
        public int DefenseRushYards { get; set; }
        public int DefenseTurnovers { get; set; }

    }
}
