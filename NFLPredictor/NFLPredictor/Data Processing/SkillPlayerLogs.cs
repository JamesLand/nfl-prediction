using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFLPredictor.Data_Processing
{
    public class SkillPlayerLogs
    {
        public String Name { get; set; }
        public String Year { get; set; }
        public int TotalTDs { get; set; }
        public List<SkillPlayerLog> Logs { get; set; }

        public SkillPlayerLogs()
        {
            Logs = new List<SkillPlayerLog>();
        }
    }
}
