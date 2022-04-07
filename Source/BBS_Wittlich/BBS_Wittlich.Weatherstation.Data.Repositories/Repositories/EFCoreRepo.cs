using BBS_Wittlich.Weatherstation.Data.Interfaces;
using BBS_Wittlich.Weatherstation.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBS_Wittlich.Weatherstation.Data.Repositories
{
    public class EFCoreRepo : IRepositories
    {
        BBS_WetterstationContext _context = new BBS_WetterstationContext();
        public WeatherEntry[] GetAllWeatherEntries(string topic)
        {
            throw new NotImplementedException();
        }

        public WeatherEntry GetLastWeatherEntry(string topic)
        {
            throw new NotImplementedException();
        }

        public List<WeatherEntry> GetWeatherStats(string topic)
        {
            throw new NotImplementedException();
        }

        public List<WeatherEntry> GetWeatherTimespan(string topic)
        {
            throw new NotImplementedException();
        }
    }
}
