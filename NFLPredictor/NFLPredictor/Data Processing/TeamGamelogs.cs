using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFLPredictor.Data_Processing
{
    public class TeamGamelogs
    {
        public String TeamName { get; set; }
        public String Year { get; set; }
        public List<Gamelog> Weeks { get; set; }

    }
}
