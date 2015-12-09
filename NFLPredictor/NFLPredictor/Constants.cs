using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFLPredictor
{
    public class Constants
    {
        public static string[] TEAM_NAMES = {
                                              "Atlanta Falcons",
                                              ""
                                          };

        public static string[] TEAM_ABBREVIATIONS = {
                                                     "atl",
                                                     "buf",
                                                     "car",
                                                     "chi",
                                                     "cin",
                                                     "cle",
                                                     "clt",
                                                     "crd",
                                                     "dal",
                                                     "den",
                                                     "det",
                                                     "gnb",
                                                     "htx",
                                                     "jax",
                                                     "kan",
                                                     "mia",
                                                     "min",
                                                     "nor",
                                                     "nwe",
                                                     "nyg",
                                                     "nyj",
                                                     "oti",
                                                     "phi",
                                                     "pit",
                                                     "rai",
                                                     "ram",
                                                     "rav",
                                                     "sdg",
                                                     "sea",
                                                     "sfo",
                                                     "tam",
                                                     "was"
                                                 };
        public static string[] YEARS = { "2011", "2012", "2013", "2014" };

        public static string ConvertNameToAbbreviation(string name)
        {
            switch (name)
            {
                case "Atlanta Falcons":
                    return "atl";

                case "Buffalo Bills":
                    return "buf";

                case "Carolina Panthers":
                    return "car";

                case "Chicago Bears":
                    return "chi";

                case "Cincinnati Bengals":
                    return "cin";

                case "Cleveland Browns":
                    return "cle";

                case "Indianapolis Colts":
                    return "clt";

                case "Arizona Cardinals":
                    return "crd";

                case "Dallas Cowboys":
                    return "dal";

                case "Denver Broncos":
                    return "den";

                case "Detroit Lions":
                    return "det";

                case "Green Bay Packers":
                    return "gnb";

                case "Houston Texans":
                    return "htx";

                case "Jacksonville Jaguars":
                    return "jax";

                case "Kansas City Chiefs":
                    return "kan";

                case "Miami Dolphins":
                    return "mia";

                case "Minnesota Vikings":
                    return "min";

                case "New Orleans Saints":
                    return "nor";

                case "New England Patriots":
                    return "nwe";

                case "New York Giants":
                    return "nyg";

                case "New York Jets":
                    return "nyj";

                case "Tennessee Titans":
                    return "oti";

                case "Philadelphia Eagles":
                    return "phi";

                case "Pittsburgh Steelers":
                    return "pit";

                case "Oakland Raiders":
                    return "rai";

                case "St. Louis Rams":
                    return "ram";

                case "Baltimore Ravens":
                    return "rav";

                case "San Diego Chargers":
                    return "sdg";

                case "Seattle Seahawks":
                    return "sea";

                case "San Francisco 49ers":
                    return "sfo";

                case "Tampa Bay Buccaneers":
                    return "tam";

                case "Washington Redskins":
                    return "was";
            }
            return "";
        }

        public static int TeamIndex(string abr)
        {
            return Array.IndexOf(TEAM_ABBREVIATIONS, abr);
        }

        public static int YearIndex(string year)
        {
            return Array.IndexOf(YEARS, year);
        }
    }
}
