using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualBasic.FileIO;

namespace NFLPredictor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            #region Teams
            var Atlanta = new Team() { Name = "Atlanta", Abreviation = "atl" };
            var Buffalo = new Team() { Name = "Buffalo", Abreviation = "buf" };
            var Carolina = new Team() { Name = "Carlina", Abreviation = "car" };
            var Chicago = new Team() { Name = "Chicago", Abreviation = "chi" };
            var Cincinatti = new Team() { Name = "Cincinatti", Abreviation = "cin" };
            var Cleveland = new Team() { Name = "Cleveland", Abreviation = "cle" };
            var Indianapolis = new Team() { Name = "Indianapolis", Abreviation = "clt" };
            var Arizona = new Team() { Name = "Arizona", Abreviation = "crd" };
            var Dallas = new Team() { Name = "Dallas", Abreviation = "dal" };
            var Denver = new Team() { Name = "Denver", Abreviation = "den" };
            var Detroit = new Team() { Name = "Detroit", Abreviation = "det" };
            var GreenBay = new Team() { Name = "Green Bay", Abreviation = "gnb" };
            var Houston = new Team() { Name = "Houston", Abreviation = "htx" };
            var Jacksonville = new Team() { Name = "Jacksonville", Abreviation = "jax" };
            var KansasCity = new Team() { Name = "Kansas City", Abreviation = "kan" };
            var Miami = new Team() { Name = "Miami", Abreviation = "mia" };
            var Minnesota = new Team() { Name = "Minnesota", Abreviation = "min" };
            var NewOrleans = new Team() { Name = "New Orleans", Abreviation = "nor" };
            var NewEngland = new Team() { Name = "New England", Abreviation = "nwe" };
            var NewYorkGiants = new Team() { Name = "New York Giants", Abreviation = "nyg" };
            var NewYorkJets = new Team() { Name = "New York Jets", Abreviation = "nyj" };
            var Tennessee = new Team() { Name = "Tennessee", Abreviation = "oti" };
            var Philadelphia = new Team() { Name = "Philadelphia", Abreviation = "phi" };
            var Pittsburgh = new Team() { Name = "Pittsburgh", Abreviation = "pit" };
            var Oakland = new Team() { Name = "Oakland", Abreviation = "rai" };
            var StLouis = new Team() { Name = "StLouis", Abreviation = "ram" };
            var Baltimoore = new Team() { Name = "Baltimoore", Abreviation = "rav" };
            var SanDiego = new Team() { Name = "San Diego", Abreviation = "sdg" };
            var Seattle = new Team() { Name = "Seattle", Abreviation = "sea" };
            var SanFrancisco = new Team() { Name = "San Francisco", Abreviation = "sfo" };
            var TampaBay = new Team() { Name = "Tampa Bay", Abreviation = "tam" };
            var Washington = new Team() { Name = "Washington", Abreviation = "was" };
            #endregion

            const string rootFolder = "C:\\Users\\thoma_000\\Documents\\nfl-prediction\\csv\\";
            //Process.Start(rootFolder);

            #region Collection of Teams
            IList<Team> Teams = new List<Team>()
            {
                Atlanta, 
                Buffalo,
                Carolina,
                Chicago,
                Cincinatti,
                Cleveland,
                Indianapolis,
                Arizona,
                Dallas,
                Denver,
                Detroit,
                GreenBay,
                Houston,
                Jacksonville,
                KansasCity,
                Miami,
                Minnesota,
                NewOrleans,
                NewEngland,
                NewYorkGiants,
                NewYorkJets,
                Tennessee,
                Philadelphia,
                Pittsburgh,
                Oakland,
                StLouis,
                Baltimoore,
                SanDiego,
                Seattle,
                SanFrancisco,
                TampaBay,
                Washington,
            };
            #endregion

            IList<int> seasons = new List<int>() { 2011, 2012, 2013, 2014 };

            foreach (var team in Teams)
            {
                foreach (var season in seasons)
                {
                    using (var sr = new StreamReader(rootFolder + team.Abreviation + "\\teams_" + team.Abreviation + "_" + season + "_team_gamelogs.csv"))
                    {
                        string line;

                        while ((line = sr.ReadLine()) != null)
                        {
                            var csvProperties = line.Split(',');
                            int week;
                            while ((Int32.TryParse(csvProperties[0], out week)) == false)
                            {
                                line = sr.ReadLine();
                                if (line != null)
                                    csvProperties = line.Split(',');
                                else break;
                            }

                            if (line == null)
                                break;
                            Console.WriteLine(line);
   
                            bool byeWeek = csvProperties[8] == "Bye Week";

                            #region Game object initialization
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
                                                    TotalYards = !byeWeek ? Int32.Parse(csvProperties[12]) : 0,
                                                    PassYards = !byeWeek ? Int32.Parse(csvProperties[13]) : 0,
                                                    RushYards = !byeWeek ? Int32.Parse(csvProperties[14]) : 0,
                                                    FirstDowns = !byeWeek ? Int32.Parse(csvProperties[11]) : 0,
                                                    FirstDownsAllowed = !byeWeek ? Int32.Parse(csvProperties[16]) : 0,
                                                    TotalYardsAllowed = !byeWeek ? Int32.Parse(csvProperties[17]) : 0,
                                                    PassYardsAllowed = !byeWeek ? Int32.Parse(csvProperties[18]) : 0,
                                                    RushYardsAllowed = !byeWeek ? Int32.Parse(csvProperties[19]) : 0,
                                                };
                            try
                            {
                                game.PointsScored = Int32.Parse(csvProperties[9]);
                            }
                            catch (Exception)
                            {
                                game.PointsScored = 0;
                            }
                            try
                            {
                                game.PointsAllowed = Int32.Parse(csvProperties[10]);
                            }
                            catch (Exception)
                            {
                                game.PointsAllowed = 0;
                            }
                            try
                            {
                                game.TurnoversLost = Int32.Parse(csvProperties[15]);

                            }
                            catch (Exception)
                            {
                                game.TurnoversLost = 0;
                            }
                            try
                            {
                                game.TurnoversGained = Int32.Parse(csvProperties[20]);

                            }
                            catch (Exception)
                            {
                                game.TurnoversGained = 0;
                            } 
                            #endregion

                            Atlanta.Games.Add(game);
                        }
                        Console.WriteLine();

                    }
                } 
            }
        }
    }
}
