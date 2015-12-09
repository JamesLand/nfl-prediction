using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NFLPredictor.Data_Processing
{
    public class SkillPlayerLogs
    {
        public String Name { get; set; }
        public String Year { get; set; }
        public int TotalTDs { get; set; }
        //public List<int> GamesPlayedIn { get; set; }
        public Dictionary<int, SkillPlayerLog> Logs { get; set; }

        public SkillPlayerLogs()
        {
            Logs = new Dictionary<int, SkillPlayerLog>();
            //GamesPlayedIn = new List<int>();
        }

        public int GetSPStatUpToGame(int game, string stat)
        {
            int returnValue = 0;
            Type gameLogType = typeof(SkillPlayerLog);
            PropertyInfo myPropInfo = gameLogType.GetProperty(stat);
            for (int i = 1; i < game; i++)
            {
                if (Logs.ContainsKey(i))
                    returnValue += (int)myPropInfo.GetValue(Logs[i]);
            }
            return returnValue;
        }
    }
}
