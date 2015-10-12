using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFLPredictor.Data_Processing
{
    public class SeasonLog
    {
        public TeamGamelogs GameLogs { get; set; }
        public QuarterbackLogs QuarterbackLogs { get; set; }
        public SkillPlayerLogs SkillPlayerLogs { get; set; }

        public SeasonLog()
        {
            GameLogs = new TeamGamelogs();
            QuarterbackLogs = new QuarterbackLogs();
            SkillPlayerLogs = new SkillPlayerLogs();
        }
    }
}
