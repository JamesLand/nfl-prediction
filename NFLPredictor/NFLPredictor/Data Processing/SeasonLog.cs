using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFLPredictor.Data_Processing
{
    public class SeasonLog
    {
        public String Name { get; set; }
        public String Year { get; set; }
        public TeamGamelogs GameLogs { get; set; }
        public List<QuarterbackLogs> Quarterbacks { get; set; }
        public List<SkillPlayerLogs> SkillPlayers { get; set; }

        public SeasonLog()
        {
            GameLogs = new TeamGamelogs();
            Quarterbacks = new List<QuarterbackLogs>();
            SkillPlayers = new List<SkillPlayerLogs>();
        }
    }
}
