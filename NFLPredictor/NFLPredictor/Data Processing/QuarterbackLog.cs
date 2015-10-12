using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFLPredictor.Data_Processing
{
    public class QuarterbackLog
    {
        public int Week { get; set; }
        public String Opponent { get; set; }
        public int PassingAttempts { get; set; }
        public int PassingCompletions { get; set; }
        public int PassingYards { get; set; }
        public int Touchdowns { get; set; }
        public int Interceptions { get; set; }
    }
}
