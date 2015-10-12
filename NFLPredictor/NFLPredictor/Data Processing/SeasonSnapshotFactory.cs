using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFLPredictor.Data_Processing
{
    public class SeasonSnapshotFactory
    {
        public static SeasonSnapshot CreateSeasonSnapshot(SeasonLog log, int week)
        {
            return new SeasonSnapshot
            {
                Name = log.Name,
                Year = log.Year,
                Wins = log.GameLogs.WinsUpToWeek(week),
                Losses = log.GameLogs.LossesUpToWeek(week),
                Ties = 0,
                PointsFor = log.GameLogs.PropertyUpToWeek("TeamScore", week),
                PointsAgainst = log.GameLogs.PropertyUpToWeek("OpposingTeamScore", week),
                TotalYards = log.GameLogs.PropertyUpToWeek("OffenseTotalYards", week),
                TotalYardsAllowed = log.GameLogs.PropertyUpToWeek("DefenseTotalYards", week),
                PassingYards= log.GameLogs.PropertyUpToWeek("OffensePassYards", week),
                PassingYardsAllowed= log.GameLogs.PropertyUpToWeek("DefensePassYards", week),
                RushingYards= log.GameLogs.PropertyUpToWeek("OffenseRushYards", week),
                RushingYardsAllowed= log.GameLogs.PropertyUpToWeek("DefenseRushYards", week),
                TurnoverMargin = log.GameLogs.PropertyUpToWeek("TurnoverMargin", week)
            };
        }

        
    }
}
