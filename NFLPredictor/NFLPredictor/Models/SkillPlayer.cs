using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFLPredictor
{
    public class SkillPlayer
    {
        public string Name { get; set; }
        public int Yards { get; set; }
        public int Touchdowns { get; set; }
        public int Fumbles { get; set; }
        public bool IsInjured { get; set; }

        public SkillPlayer()
        {
            this.Name = "";
            this.Yards = 0;
            this.Touchdowns = 0;
            this.Fumbles = 0;
            this.IsInjured = false;
        }

        public SkillPlayer(string name, int yds, int td, int fum, bool inj)
        {
            this.Name = name;
            this.Yards = yds;
            this.Touchdowns = td;
            this.Fumbles = fum;
            this.IsInjured = inj;
        }
    }
}
