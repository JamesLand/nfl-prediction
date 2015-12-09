using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.VisualBasic.FileIO;
using NFLPredictor.Data_Processing;
using System.Collections.Generic;
using NFLPredictor.Neural_Nets;

namespace NFLPredictor
{
    public class Program
    {
        public static List<SeasonSnapshot>[,] AllData = new List<SeasonSnapshot>[32, 4];
        public static List<Game>[,] Games = new List<Game>[32, 4];
        public static List<double> Results = new List<double>();

        public static void Main(string[] args)
        {
            DataReader dr = new DataReader();

            for (int i = 0; i < 32; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    AllData[i, j] = dr.ReadSeason(Constants.TEAM_ABBREVIATIONS[i], Constants.YEARS[j], out Games[i, j]);
                }
            }

            Network network = new Network();
            network.InitializeNetwork(21, 31);//bias + # of nodes in that row

            StreamWriter outFile = new StreamWriter("D:\\nflPred\\nfl-prediction\\output\\outfileh30_a01_500.txt");
            
             //RunXorTest(network, outFile);
            for (int i = 0; i < 500; i++)
            {
                List<Game>[,] games = ScrambleGames();
                double result = RunAllGamesAndUpdate(network, games);
                Console.WriteLine(i + " " + result);
                outFile.WriteLine(result);
            }
            outFile.Close();
            WriteOutResults();
            StreamWriter sw = new StreamWriter("D:\\nflPred\\nfl-prediction\\output\\" + "Results_on_test_data_h30_a01_500passes " + DateTime.Now.ToLongDateString() + ".txt");
            RunSeason2014(network, sw);
            sw.Close();

            //HeuristicsRun();
        }

        public static SeasonSnapshot GetSnapshotOfTeamAtWeek(string teamAbbrev, string season, int week)
        {
            return Program.AllData[Constants.TeamIndex(teamAbbrev), Constants.YearIndex(season)][week - 1];
        }

        public static void RunXorTest(Network network, StreamWriter outFile)
        {
            Random rand = new Random();
            int first = 0;
            int second = 0;
            bool actualResult = false;
            int intActualResult = 0;
            bool successful = false;
            double error = 1;
            while (error > 0.1)
            {
                first = rand.Next(0, 2);
                second = rand.Next(0, 2);
                actualResult = (first == 1) ^ (second == 1);
                intActualResult = actualResult ? 1 : 0;
                double result = network.RunXOR(first, second);
                successful = (Math.Round(result) == 1) == actualResult;
                error = Math.Abs(result - intActualResult);
                network.UpdateWeights(actualResult);
                if (error < 0.2)
                    outFile.WriteLine(first + " ^ " + second + " = " + intActualResult + " -> " + result.ToString("F4") + " " + successful);

            }
            outFile.Flush();
            outFile.Close();
        }

        public static double RunAllGamesAndUpdate(Network network, List<Game>[,] games)
        {
            int gameCount = 0;
            int correctGameCount = 0;
            for (int i = 0; i < 32; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    foreach (Game game in games[i, j])
                    {
                        if (game.Home)
                        {
                            gameCount++;
                            string opposingTeam = Constants.ConvertNameToAbbreviation(game.Opponent);
                            int week = game.Week;
                            string season = Constants.YEARS[j]; //I know this is redundant as of now.
                            bool didHomeTeamWin = game.DidTeamWin;
                            double result = network.Run(Program.AllData[i, j][week], GetSnapshotOfTeamAtWeek(opposingTeam, season, week));
                            double error = didHomeTeamWin ? (1 - result) : result;
                            if (error < .5) correctGameCount++;
                            Results.Add(error);
                            network.UpdateWeights(didHomeTeamWin);
                        }
                    }
                }
            }
            return (double)correctGameCount / gameCount;
        }

        public static List<Game>[,] ScrambleGames()
        {
            Random rand = new Random();
            List<Game>[,] scrambledGames = new List<Game>[32, 4];
            for (int i = 0; i < 32; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    scrambledGames[i, j] = Program.Games[i, j].OrderBy(x => rand.Next()).ToList();
                }

            }
            return scrambledGames;
        }

        public static void RunSeason2014(Network network, StreamWriter sw)
        {
            int correctlyPredicted = 0;
            int totalGames = 0;

            for (int i = 0; i < 32; i++)
            {
                for (int j = 3; j < 4; j++)
                {
                    foreach (Game game in Program.Games[i, j])
                    {
                        if (game.Home)
                        {
                            totalGames++;
                            string homeTeam = Program.AllData[i, j][game.Week].Abbreviation;
                            string opposingTeam = Constants.ConvertNameToAbbreviation(game.Opponent);
                            int week = game.Week;
                            string season = Constants.YEARS[3]; //I know this is redundant as of now.
                            bool didHomeTeamWin = game.DidTeamWin;
                            double result = network.Run(Program.AllData[i, j][game.Week], GetSnapshotOfTeamAtWeek(opposingTeam, season, week));
                            double error = didHomeTeamWin ? (1 - result) : result;
                            if (error < 0.5)
                            {
                                correctlyPredicted++;
                            }
                            sw.WriteLine(opposingTeam + " @ " + homeTeam + ": Prediction is " + (error < 0.5));
                        }
                    }
                }

            }
            sw.WriteLine();
            sw.WriteLine("Results: " + correctlyPredicted + " out of " + totalGames + " games predicted correctly.");
            sw.WriteLine((double)correctlyPredicted / totalGames * 100 + "% correct");
        }

        public static void WriteOutResults()
        {
            ResultsWriter rw = new ResultsWriter();
            rw.WriteResults(Results);
        }

        public static void HeuristicsRun()
        {
            int gamesHomeTeamWon = 0 ;
            for (int i = 0; i < 32; i++)
            {
                for (int j = 3; j < 4; j++)
                {
                    foreach (Game game in Program.Games[i, j])
                    {
                        if (game.Home && game.DidTeamWin)
                        {
                            gamesHomeTeamWon++;
                        }
                    }
                }

            }
            Console.WriteLine(gamesHomeTeamWon);
        }

    }
}
