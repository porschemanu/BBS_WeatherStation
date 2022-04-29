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
                weatherEntry.Id = rdr.GetInt32(0);
                weatherEntry.topic = "FUCK";
                weatherEntry.value = rdr.GetDouble(1);
                weatherEntry.timestamp = rdr.GetDateTime(2);
                weatherEntries.Add(weatherEntry);
            }

            mySqlConnection.Close();

            return weatherEntries.ToArray();
        }

        public void SQLExecuter(string statement)
        {
            MySqlConnection mySqlConnection = ConnectionBuilder();

            MySqlCommand cmd = new MySqlCommand(statement, mySqlConnection);

            List<WeatherEntry> weatherEntries = new();

            mySqlConnection.Open();
            cmd.ExecuteNonQuery();
            mySqlConnection.Close();
        }

        public WeatherEntry[] Get()
        {   
            string statement = $"SELECT * FROM MQTT ORDER BY id DESC";
            return SQLReader(statement);
        }

        public WeatherEntry[] Get(string topic)
        {
            string statement = $"SELECT * FROM `{topic}` ORDER BY id DESC";
            return SQLReader(statement);
        }

        public WeatherEntry[] Get(string topic, DateTime date)
        {
            string statement = $"SELECT * FROM `{topic}` WHERE Timestamp BETWEEN '{date.ToString("yyyy-MM-dd 00:00:00")}' AND '{date.Date.ToString("yyyy-MM-dd 23:59:59")}' ORDER BY id DESC";
            Console.WriteLine(statement);
            return SQLReader(statement);
        }
        
        public WeatherEntry[] Get(string topic, DateTime startDate, DateTime endDate)
        {
            string statement = $"SELECT * FROM `{topic}` WHERE Timestamp BETWEEN '{startDate.ToString("yyyy-MM-dd HH:mm:ss")}' AND '{endDate.ToString("yyyy-MM-dd HH:mm:ss")}' ORDER BY id DESC";
            Console.WriteLine(statement);
            return SQLReader(statement);
        }

        public WeatherEntry Get(int id)
        {
            throw new NotImplementedException();
        }

        public WeatherEntry GetLast(string topic)
        {
            string statement = $"SELECT * FROM `{topic}` LIMIT 1";
            return SQLReader(statement).FirstOrDefault();
        }
    }
}
