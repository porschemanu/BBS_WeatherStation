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
        public string Source { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="topic"></param>
        /// <returns></returns>
        public Models.WeatherEntry[] Get();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="topic"></param>
        /// <returns></returns>
        public Models.WeatherEntry[] Get(string topic);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="topic"></param>
        /// <returns></returns>
        public Models.WeatherEntry[] Get(string topic, DateTime date);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="topic"></param>
        /// <returns></returns>
        public Models.WeatherEntry[] Get(string topic, DateTime startDate, DateTime endDate);

        public Models.WeatherEntry Get(int id);

        public Models.WeatherEntry GetLast(string topic);
    }
}
