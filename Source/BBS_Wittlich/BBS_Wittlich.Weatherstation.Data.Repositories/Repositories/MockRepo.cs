using BBS_Wittlich.Weatherstation.Data.Interfaces;
using BBS_Wittlich.Weatherstation.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBS_Wittlich.Weatherstation.Data.Repositories
{
    public class MockRepo : IRepositories
    {
        string IRepositories.Source { get ; set ; }

        private string _source;

        public WeatherEntry[] Get()
        {
            WeatherEntry[] weatherEntries = null;
            return weatherEntries;
        }

        public WeatherEntry[] Get(string topic)
        {
            WeatherEntry[] weatherEntries = null;
            return weatherEntries;
        }

        public WeatherEntry[] Get(string topic, DateTime startDate, DateTime endDate)
        {
            WeatherEntry[] weatherEntries = null;
            return weatherEntries;
        }

        public WeatherEntry[] Get(string topic, DateTime date)
        {
            WeatherEntry[] weatherEntries = null;
            return weatherEntries;
        }

        public WeatherEntry Get(int id)
        {
            throw new NotImplementedException();
        }

        public WeatherEntry GetLast(string topic)
        {
            throw new NotImplementedException();
        }
    }
}
