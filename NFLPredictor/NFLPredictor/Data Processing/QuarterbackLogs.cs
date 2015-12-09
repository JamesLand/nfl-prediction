using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NFLPredictor.Data_Processing
{
    public class QuarterbackLogs
    {
        public String Name { get; set; }
        public String Year { get; set; }
        public List<int> GamesStarted { get; set; }
        public Dictionary<int, QuarterbackLog> Logs { get; set; }

        public QuarterbackLogs()
        {
            Logs = new Dictionary<int, QuarterbackLog>();
            GamesStarted = new List<int>();
        }

        public int GetQBStatUpToGame(int game, string stat)
        {
            int returnValue = 0;
            Type gameLogType = typeof(QuarterbackLog);
            PropertyInfo myPropInfo = gameLogType.GetProperty(stat);
            for (int i = 1; i < game; i++)
            {
                if (GamesStarted.Contains(i))
                    returnValue += (int)myPropInfo.GetValue(Logs[i]);
            }
            return returnValue;
        }
    }
}
