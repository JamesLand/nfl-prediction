using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFLPredictor.Data_Processing
{
    public class DataReader
    {
        const string rootFolder = "D:\\nflPred\\nfl-prediction\\csv\\";

        public List<SeasonSnapshot> ReadSeason(String teamAbrev, String year)
        {
            SeasonLog seasonLog = new SeasonLog
            {
                Name = teamAbrev,
                Year = year
            };
            string[] files = Directory.GetFiles(rootFolder + teamAbrev);
            foreach (string fileName in files)
            {
                if (fileName.Contains(year))
                {
                   // string filePath = rootFolder + year + "\\" + fileName;
                    ReadFile(fileName, ref seasonLog);
                }
            }
            List<SeasonSnapshot> snapshots = new List<SeasonSnapshot>();
            for (int i = 1; i <= 17; i++)
            {
                SeasonSnapshot snapshot = SeasonSnapshotFactory.CreateSeasonSnapshot(seasonLog, i);
                snapshots.Add(snapshot);
            }
            return snapshots;
        }

        public void ReadFile(string filePath, ref SeasonLog seasonLog)
        {
            if (filePath.Contains("teams"))
            {
                seasonLog.GameLogs = ReadTeamGamelogs(filePath);
            }
            else
            {
                // ReadPlayerGameLogs(filePath, ref seasonLog);
            }
        }

        public TeamGamelogs ReadTeamGamelogs(string filePath)
        {
            TeamGamelogs gamelogs = new TeamGamelogs();
            using (var sr = new StreamReader(filePath))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                    var csvProperties = line.Split(',');
                    int week;
                    

                    while ((Int32.TryParse(csvProperties[0], out week)) == false)
                    {
                        line = sr.ReadLine();
                        if (line != null)
                            csvProperties = line.Split(',');
                    }
                    Console.WriteLine();
                    

                    Gamelog game = new Gamelog
                    {
                        Week = Int32.Parse(csvProperties[0]),
                        DidTeamWin = csvProperties[4] == "W",
                        WasAtHome = csvProperties[7] == "@",
                        Opponent = csvProperties[8],
                        TeamScore = csvProperties[9].Equals("") ? 0 : Int32.Parse(csvProperties[9]),
                        OpposingTeamScore = csvProperties[10].Equals("") ? 0 : Int32.Parse(csvProperties[10]),
                        OffenseFirstDowns = csvProperties[11].Equals("") ? 0 : Int32.Parse(csvProperties[11]),
                        OffenseTotalYards = csvProperties[12].Equals("") ? 0 : Int32.Parse(csvProperties[12]),
                        OffensePassYards = csvProperties[13].Equals("") ? 0 : Int32.Parse(csvProperties[13]),
                        OffenseRushYards = csvProperties[14].Equals("") ? 0 : Int32.Parse(csvProperties[14]),
                        OffenseTurnovers = csvProperties[15].Equals("") ? 0 : Int32.Parse(csvProperties[15]),
                        DefenseFirstDowns = csvProperties[16].Equals("") ? 0 : Int32.Parse(csvProperties[16]),
                        DefenseTotalYards = csvProperties[17].Equals("") ? 0 : Int32.Parse(csvProperties[17]),
                        DefensePassYards = csvProperties[18].Equals("") ? 0 : Int32.Parse(csvProperties[18]),
                        DefenseRushYards = csvProperties[19].Equals("") ? 0 : Int32.Parse(csvProperties[19]),
                        DefenseTurnovers = csvProperties[20].Equals("") ? 0 : Int32.Parse(csvProperties[20]),
                    };

                    game.WasByeWeek = game.Opponent.Equals("Bye Week");
                    gamelogs.Weeks.Add(game.Week, game);
                    if (gamelogs.Weeks.Count >= 17)
                    {
                        return gamelogs;
                    }
                }
            }
            return gamelogs;
        }

        public void ReadPlayerGameLogs(string filePath, ref SeasonLog seasonLog)
        {
            using (var sr = new StreamReader(filePath))
            {
                string line;
                QuarterbackLogs qbLogs;
                SkillPlayerLogs spLogs;
                bool isQB = true;
                while ((line = sr.ReadLine()) != null)
                {
                }
                Console.WriteLine(line);
                var csvProperties = line.Split(',');
                int week;
                while ((csvProperties[9].Equals("")))
                {
                    line = sr.ReadLine();
                    if (line != null)
                        csvProperties = line.Split(',');
                }
                if (csvProperties[9].Equals("Passing")) //Is a quarterback
                {
                    qbLogs = new QuarterbackLogs();

                    while ((line = sr.ReadLine()) != null)
                    {
                        while ((Int32.TryParse(csvProperties[0], out week)) == false)
                        {
                            line = sr.ReadLine();
                            if (line != null)
                                csvProperties = line.Split(',');
                        }
                        Console.WriteLine();

                        QuarterbackLog qbLog = new QuarterbackLog
                        {
                            Week = Int32.Parse(csvProperties[0]),
                            Opponent = csvProperties[8],
                            PassingAttempts = csvProperties[10].Equals("") ? 0 : Int32.Parse(csvProperties[10]),
                            PassingCompletions = csvProperties[9].Equals("") ? 0 : Int32.Parse(csvProperties[9]),
                            PassingYards = csvProperties[12].Equals("") ? 0 : Int32.Parse(csvProperties[12]),
                            Touchdowns = csvProperties[13].Equals("") ? 0 : Int32.Parse(csvProperties[13]),
                            Interceptions = csvProperties[14].Equals("") ? 0 : Int32.Parse(csvProperties[14]),
                        };
                        qbLogs.Logs.Add(qbLog);

                    }
                    seasonLog.Quarterbacks.Add(qbLogs);
                }
                else //Is a skill player (hopefully)
                {
                    spLogs = new SkillPlayerLogs();
                    int touchdownsColumn = 0;
                    for (int i = 0; i < csvProperties.Length; i++)
                    {
                        if (csvProperties[i].Equals("Scoring"))
                        {
                            touchdownsColumn = i;
                            break;
                        }
                    }
                    line = sr.ReadLine();
                    csvProperties = line.Split(',');
                    int[] yardsColumns = { -1, -1, -1, -1, -1 };
                    int counter = 0;
                    for (int i = 0; i < csvProperties.Length; i++)
                    {
                        if (csvProperties[i].Equals("Yds"))
                        {
                            yardsColumns[counter] = i;
                            counter++;
                        }
                    }
                    while ((line = sr.ReadLine()) != null)
                    {

                        while ((Int32.TryParse(csvProperties[0], out week)) == false)
                        {
                            line = sr.ReadLine();
                            if (line != null)
                                csvProperties = line.Split(',');
                        }
                        Console.WriteLine();

                        SkillPlayerLog spLog = new SkillPlayerLog
                        {
                            Week = Int32.Parse(csvProperties[0]),
                            Opponent = csvProperties[8],
                            Touchdowns = csvProperties[touchdownsColumn].Equals("") ? 0 : Int32.Parse(csvProperties[touchdownsColumn]),
                            Yards = 0
                        };
                        for (int i = 0; i < counter; i++)
                        {
                            spLog.Yards += csvProperties[yardsColumns[i]].Equals("") ? 0 : Int32.Parse(csvProperties[yardsColumns[i]]);
                        }
                        spLogs.Logs.Add(spLog);
                    }

                    seasonLog.SkillPlayers.Add(spLogs);
                }


            }
        }
    }
}
