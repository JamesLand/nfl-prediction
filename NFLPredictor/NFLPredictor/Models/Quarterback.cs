using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFLPredictor
{
    public class Quarterback
    {
        public string Name { get; set; }
        public int PassAttempts { get; set; }
        public int PassCompletions { get; set; }
        public int PassingYards { get; set; }
        public int Touchdowns { get; set; }
        public int Interceptions { get; set; }
        public bool IsStarting { get; set; }

        public double CompletionPercentage
        {
            get
            {
                return PassCompletions / PassAttempts;
            }
        }

        public Quarterback()
        {
            this.Name = "";
            this.PassAttempts = 0;
            this.PassCompletions = 0;
            this.PassingYards = 0;
            this.Touchdowns = 0;
            this.Interceptions = 0;
            this.IsStarting = false;
        }

        public Quarterback(string name, int passAtt, int passComp, int passYrds, int td, 
            int inter, bool isStart)
        {
            this.Name = name;
            this.PassAttempts = passAtt;
            this.PassCompletions = passComp;
            this.PassingYards = passYrds;
            this.Touchdowns = td;
            this.Interceptions = inter;
            this.IsStarting = isStart;
        }
    }
}
