using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NFLPredictor.Data_Processing
{
    public class TeamGamelogs
    {
        public String TeamName { get; set; }
        public String Year { get; set; }
        public Dictionary<int, Gamelog> Weeks { get; set; }

        public TeamGamelogs()
        {
            Weeks = new Dictionary<int, Gamelog>();
        }

        public int WinsUpToWeek(int week)
        {
            int wins = 0;
            for (int i = 1; i < week; i++)
            {
                if (!Weeks[i].WasByeWeek && Weeks[i].DidTeamWin) wins++;
            }
            return wins;
        }

        public int LossesUpToWeek(int week)
        {
            int losses = 0;
            for (int i = 1; i < week; i++)
            {
                if (!Weeks[i].WasByeWeek && !Weeks[i].DidTeamWin) losses++;
            }
            return losses;
        }

        public int PropertyUpToWeek(string propertyName, int week)
        {

            int returnValue = 0;
            Type gameLogType = typeof(Gamelog);
            PropertyInfo myPropInfo = gameLogType.GetProperty(propertyName);
            for (int i = 1; i < week; i++)
            {
                returnValue += (int)myPropInfo.GetValue(Weeks[i]);
            }
            return returnValue;

        }
    }
}
