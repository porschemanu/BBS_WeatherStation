using BBS_Wittlich.Weatherstation.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace BBS_Wittlich.Weatherstation.Data.Repositories
{
    public class SQLClientRepo : Interfaces.IRepositories
    {
        public string Source { get; set; }

        private MySqlConnection ConnectionBuilder()
        {
            MySqlConnection sqlConnection = new MySqlConnection($"server={Source};userid=WebServer;password=Wittlich;database=BBS_Wetterstation");
            return sqlConnection;
        }

        private WeatherEntry[] SQLReader(string statement)
        {
            MySqlConnection mySqlConnection = ConnectionBuilder();

            MySqlCommand cmd = new MySqlCommand(statement, mySqlConnection);

            List<WeatherEntry> weatherEntries = new();

            mySqlConnection.Open();
            using MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                WeatherEntry weatherEntry = new WeatherEntry();
                weatherEntry.topic = rdr.GetString(1);
                weatherEntry.value = rdr.GetDouble(2);
                weatherEntry.timestamp = rdr.GetDateTime(3);
                weatherEntries.Add(weatherEntry);
            }

            mySqlConnection.Close();

            return weatherEntries.ToArray();
        }

        public WeatherEntry[] Get()
        {   
            string statement = $"SELECT * FROM MQTT WHERE id mod 1000 = 0 ORDER BY id DESC";
            Console.WriteLine(statement);
            return SQLReader(statement);
          
        }

        public WeatherEntry[] Get(string topic)
        {
            string statement = $"SELECT * FROM MQTT WHERE id mod 100 = 0 AND Topic = '{topic}' ORDER BY id DESC";
            Console.WriteLine(statement);
            return SQLReader(statement);
        }

        public WeatherEntry[] Get(string topic, DateTime date)
        {
            Console.WriteLine(date);
            string statement = $"SELECT * FROM `MQTT` WHERE Topic = 'temp' AND Timestamp BETWEEN CAST('{date.AddDays(-1).ToString("yyyy-MM-dd")}' AS DATE) AND CAST('{date.Date.ToString("yyyy-MM-dd")}' AS DATE)";
            Console.WriteLine(statement);
            return SQLReader(statement);
        }
        
        public WeatherEntry[] Get(string topic, DateTime startDate, DateTime endDate)
        {
            string statement = $"SELECT * FROM `MQTT` WHERE Topic = 'temp' AND Timestamp between CAST('{startDate.ToString("yyyy-MM-dd")}' AS DATE) and CAST('{endDate.ToString("yyyy-MM-dd")}' AS DATE)";
            Console.WriteLine(statement);
            return SQLReader(statement);
        }
    }
}
