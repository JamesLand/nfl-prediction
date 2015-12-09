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
            SeasonSnapshot snapshot =  new SeasonSnapshot
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
                TurnoverMargin = log.GameLogs.PropertyUpToWeek("TurnoverMargin", week),
                PassAttempts = log.GetQBStatUpToWeek(week, "PassingAttempts"),
                PassCompletions = log.GetQBStatUpToWeek(week, "PassingCompletions"),
                QBPassingYards = log.GetQBStatUpToWeek(week, "PassingYards"),
                QBTouchdowns = log.GetQBStatUpToWeek(week, "Touchdowns"),
                Interceptions = log.GetQBStatUpToWeek(week, "Interceptions"),
                SkillPlayers = new List<SkillPlayer>()
            };
            foreach (SkillPlayerLogs skillPlayer in log.SkillPlayers)
            {
                if (week >= log.GameLogs.ByeWeek)
                {
                    snapshot.SkillPlayers.Add(new SkillPlayer
                    {
                        Yards = skillPlayer.GetSPStatUpToGame(week - 1, "Yards"),
                        Touchdowns = skillPlayer.GetSPStatUpToGame(week - 1, "Touchdowns")
                    });
                }
                else
                {
                    snapshot.SkillPlayers.Add(new SkillPlayer
                    {
                        Yards = skillPlayer.GetSPStatUpToGame(week, "Yards"),
                        Touchdowns = skillPlayer.GetSPStatUpToGame(week, "Touchdowns")
                    });
                }
            }

            return snapshot;
        }

        
    }
}
