using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weatherstation.Data.Model
{
    public class WeatherDataEntry
    {
        public string topic { get; set; }
        public double value { get; set; }
        public DateTime timestamp { get; set; }

    }
}
