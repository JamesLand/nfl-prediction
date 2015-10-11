using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFLPredictor
{
    public class Team
    {
        public string Name { get; set; }
        public string Abreviation { get; set; }
        public IList<Game> Games { get; set; }

        public Team()
        {
            Games = new List<Game>();
        }
    }
}
