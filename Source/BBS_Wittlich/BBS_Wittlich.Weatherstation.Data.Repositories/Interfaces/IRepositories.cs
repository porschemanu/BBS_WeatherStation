using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBS_Wittlich.Weatherstation.Data.Interfaces
{
    public interface IRepositories
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="topic"></param>
        /// <returns></returns>
        public Models.WeatherEntry GetLastWeatherEntry(string topic);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="topic"></param>
        /// <returns></returns>
        public Models.WeatherEntry[] GetAllWeatherEntries(string topic);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="topic"></param>
        /// <returns></returns>
        public List<Models.WeatherEntry> GetWeatherStats(string topic);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="topic"></param>
        /// <returns></returns>
        public List<Models.WeatherEntry> GetWeatherTimespan(string topic);
    }
}
