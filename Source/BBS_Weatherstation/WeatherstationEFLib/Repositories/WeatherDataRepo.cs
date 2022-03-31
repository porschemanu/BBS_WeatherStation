using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weatherstation.Data;
using Weatherstation.Data.Model;

namespace WeatherstationEFLib.Repositories
{
    public class WeatherDataRepo : IWeatherDataAccess
    {
        public List<WeatherDataEntry> GetEntries(string topic, int days)
        {
            throw new NotImplementedException();
        }

        public WeatherDataEntry GetLastEntry(string topic)
        {
            throw new NotImplementedException();
        }

        public List<double> GetStatistics(string topic, int days)
        {
            throw new NotImplementedException();
        }
    }
}
