using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFLPredictor.Data_Processing
{
    public class QuarterbackLogs
    {
        public String Name { get; set; }
        public String Year { get; set; }
        public List<QuarterbackLog> Logs { get; set; }
    }
}
