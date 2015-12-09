using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFLPredictor
{
    public class SeasonSnapshot
    {
        public string Name {get; set;}
        public string Abbreviation {get; set;}
        public string Year {get; set;}
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Ties { get; set; }
        public int PointsFor { get; set; }
        public int PointsAgainst { get; set; }
        public int TotalYards { get; set; }
        public int PassingYards { get; set; }
        public int RushingYards { get; set; }
        public int TotalYardsAllowed { get; set; }
        public int PassingYardsAllowed { get; set; }
        public int RushingYardsAllowed { get; set; }
        public int TurnoverMargin { get; set; }

        //Quarterback Stats
        //public List<Quarterback> StartingQuarterbacks { get; set; }
        public int PassAttempts { get; set; }
        public int PassCompletions { get; set; }
        public int QBPassingYards { get; set; }
        public int QBTouchdowns { get; set; }
        public int Interceptions { get; set; }
        public double CompletionPercentage
        {
            get
            {
                return (double)PassCompletions / PassAttempts;
            }
        }
        
        public List<SkillPlayer> SkillPlayers { get; set; }

        public double WinPercentage
        {
            get
            {
                return (this.Wins + 0.5 * this.Ties) / (this.Wins + this.Losses + this.Ties);
            }
        }

        //public Quarterback StartingQuarterback
        //{
        //    get
        //    {
        //        foreach (Quarterback q in StartingQuarterbacks)
        //        {
        //            if (q.IsStarting) return q;
        //        }
        //        return null;
        //    }
        //}

        public SeasonSnapshot()
        {
            this.Name = "";
            this.Abbreviation = "";
            this.Year = "";
            this.Wins = 0;
            this.Losses = 0;
            this.Ties = 0;
            this.PointsFor = 0;
            this.PointsAgainst = 0;
            this.TotalYards = 0;
            this.TotalYardsAllowed = 0;
            this.PassingYards = 0;
            this.PassingYardsAllowed = 0;
            this.RushingYards = 0;
            this.RushingYardsAllowed = 0;
            this.TurnoverMargin = 0;
            this.PassAttempts = 0;
            this.PassCompletions = 0;
            this.QBPassingYards = 0;
            this.QBTouchdowns = 0;
            this.Interceptions = 0;
            this.SkillPlayers = new List<SkillPlayer>();
        }

        public SeasonSnapshot(string name, string abr, string yr, int wins, int losses, int ties, int ptfr, int ptag,
            int totyrds, int passyrds, int rushyds, int totyrdsall, int passyrdsall, int rushyrdsall, int tnovr,
            int passAtt, int passComp, int qbPassYrds, int qbTds, int ints)
        {
            this.Name = name;
            this.Abbreviation = abr;
            this.Year = yr;
            this.Wins = wins;
            this.Losses = losses;
            this.Ties = ties;
            this.PointsFor = ptfr;
            this.PointsAgainst = ptag;
            this.TotalYards = totyrds;
            this.TotalYardsAllowed = totyrdsall;
            this.PassingYards = passyrds;
            this.PassingYardsAllowed = passyrdsall;
            this.RushingYards = rushyds;
            this.RushingYardsAllowed = rushyrdsall;
            this.TurnoverMargin = tnovr;
            this.PassAttempts = passAtt;
            this.PassCompletions = passComp;
            this.QBPassingYards = qbPassYrds;
            this.QBTouchdowns = qbTds;
            this.Interceptions = ints;
            this.SkillPlayers = new List<SkillPlayer>();
        }

        //public void AddQuarterback(Quarterback qb)
        //{
        //    this.StartingQuarterbacks.Add(qb);
        //}

        public void AddSkillPlayer(SkillPlayer sp)
        {
            this.SkillPlayers.Add(sp);
        }


    }
}
