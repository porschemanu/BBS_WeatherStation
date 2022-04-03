using BBS_Wittlich.Weatherstation.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace BBS_Wittlich.Weatherstation.Data.Repositories
{
    public class SQLClientRepo : Data.Interfaces.IRepositories
    {
        public WeatherEntry[] GetAllWeatherEntries(string topic)
        {
            string cs = @"server=192.168.0.10;userid=WebServer;password=Wittlich;database=BBS_Wetterstation";
            using var con = new MySqlConnection(cs);
            string sql = $"SELECT * FROM MQTT WHERE Topic = '{topic}' ORDER BY id DESC LIMIT 100";
            MySqlCommand cmd = new MySqlCommand(sql, con);
            List<WeatherEntry> weatherEntries = new();

            con.Open();
            using MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                WeatherEntry weatherEntry = new WeatherEntry();
                weatherEntry.topic = rdr.GetString(1);
                weatherEntry.value = rdr.GetDouble(2);
                weatherEntry.timestamp = rdr.GetDateTime(3);
                weatherEntries.Add(weatherEntry);
            }
            con.Close();

            return weatherEntries.ToArray();
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
