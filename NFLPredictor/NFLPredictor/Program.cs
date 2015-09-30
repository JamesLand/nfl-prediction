using System;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualBasic.FileIO;

namespace NFLPredictor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            const string rootFolder = "C:\\Users\\thoma_000\\Documents\\NFLData";

            //Process.Start(rootFolder + "\\teams_atl_2011_team_gamelogs.csv");
            int season = 2011;
            var Atlanta = new Team {Name = "Atlanta"};

            using (var sr = new StreamReader(rootFolder + "\\teams_atl_2011_team_gamelogs.csv"))
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
                        if(line != null)
                            csvProperties = line.Split(',');
                    }
                    Console.WriteLine();
                    Game game = new Game
                    {
                        Season = season,
                        Week = Int32.Parse(csvProperties[0]),
                        Day = csvProperties[1],
                        Date = csvProperties[2],
                        Result = csvProperties[4],
                        Overtime = csvProperties[5] == "Overtime",
                        Home = csvProperties[7] == "@",
                        Opponent = csvProperties[8],
                        //if one of these is 0, the cell is blank.. need to account for this
                        PointsScored = Int32.Parse(csvProperties[9]),
                        PointsAllowed = Int32.Parse(csvProperties[10]),
                        FirstDowns = Int32.Parse(csvProperties[11]),
                        TotalYards = Int32.Parse(csvProperties[12]),
                        PassYards = Int32.Parse(csvProperties[13]),
                        RushYards = Int32.Parse(csvProperties[14]),
                        TurnoversLost = Int32.Parse(csvProperties[15]),
                        FirstDownsAllowed = Int32.Parse(csvProperties[16]),
                        TotalYardsAllowed = Int32.Parse(csvProperties[17]),
                        PassYardsAllowed = Int32.Parse(csvProperties[18]),
                        RushYardsAllowed = Int32.Parse(csvProperties[19]),
                        TurnoversGained = Int32.Parse(csvProperties[20]),
                    };
                    Atlanta.Games.Add(game);
                }
            }
            Console.WriteLine(Atlanta);
        }
    }
}
