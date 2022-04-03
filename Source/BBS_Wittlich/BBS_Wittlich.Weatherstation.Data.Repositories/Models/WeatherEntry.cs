using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBS_Wittlich.Weatherstation.Data.Models
{
    public class WeatherEntry
    {
        public DateTime timestamp { get; set; }
        public string topic { get; set; }
        public double value { get; set; }

    }
}
