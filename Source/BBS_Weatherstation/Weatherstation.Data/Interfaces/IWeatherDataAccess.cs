using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weatherstation.Data.Model;

namespace Weatherstation.Data
{
    public interface IWeatherDataAccess
    {
        /// <summary>
        /// Gets the last DataEntry to a specific topic.
        /// </summary>
        public WeatherDataEntry GetLastEntry(string topic);

        /// <summary>
        /// Gets all DataEntries of a specific topic in a specific timespan.
        /// </summary>
        public List<WeatherDataEntry> GetEntries(string topic, int days);

        /// <summary>
        /// Gets the Minimum, Maximum and the Average of a specific topic in a specific timespan.
        /// </summary>
        public List<double> GetStatistics(string topic, int days);


    }
}
