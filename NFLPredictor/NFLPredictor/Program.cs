using System;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using NFLPredictor.Data_Processing;
using System.Collections.Generic;

namespace NFLPredictor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DataReader dr = new DataReader();

            List<SeasonSnapshot>[,] AllData = new List<SeasonSnapshot>[32,4];
            for (int i = 0; i < 32; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    AllData[i, j] = dr.ReadSeason(Constants.TEAM_ABBREVIATIONS[i], Constants.YEARS[j]);
                }
            }
        }
    }
}
