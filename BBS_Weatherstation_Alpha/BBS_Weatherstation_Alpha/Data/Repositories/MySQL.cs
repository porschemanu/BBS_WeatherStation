using System;
using System.Diagnostics;
using BBS_Weatherstation_SeriesA.Data.Models;
using MySql.Data.MySqlClient;
using static BBS_Weatherstation_SeriesA.Data.Global.Cache;

namespace BBS_Weatherstation_SeriesA.Data.Repositories
{
    public class MySQL
    {

        public static WeatherEntry[] SQLRead(string statement, WeatherEntry returnValue)
        {
            MySqlConnection _connection = new MySqlConnection($"server=localhost;userid=Hans;password=1234;database=BBS_Wetterstation");
            MySqlCommand cmd = new MySqlCommand(statement, _connection);

            List<WeatherEntry> weatherEntries = new();

            _connection.Open();
            using MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                WeatherEntry weatherEntry = new WeatherEntry();
                weatherEntry.Id = rdr.GetInt32(0);
                weatherEntry.topic = rdr.GetString(1);
                weatherEntry.value = rdr.GetDouble(2);
                weatherEntry.timestamp = rdr.GetDateTime(3);
                weatherEntries.Add(weatherEntry);
            }
            _connection.Close();
            return weatherEntries.ToArray();
        }

        private static double[] SQLRead(string statement)
        {
            MySqlConnection _connection = new MySqlConnection($"server=localhost;userid=Hans;password=1234;database=BBS_Wetterstation");
            MySqlCommand cmd = new MySqlCommand(statement, _connection);

            List<double> Data = new();

            _connection.Open();
            using MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Data.Add(rdr.GetDouble(1));
            }
            _connection.Close();
            return Data.ToArray();
        }

        public static void SQLWrite(string statement)
        {
            MySqlConnection _connection = new MySqlConnection($"server=localhost;userid=Hans;password=1234;database=BBS_Wetterstation");

            MySqlCommand cmd = new MySqlCommand(statement, _connection);

            _connection.Open();
            cmd.ExecuteNonQuery();
            _connection.Close();
        }

        public static WeatherEntry[] Get()
        {
            WeatherEntry dummy = new(); //<- Used to diferentiate between the two SQLRead methods
            string statement = $"SELECT * FROM mqtt ORDER BY id DESC";
            return SQLRead(statement, dummy);
        }

        public static WeatherEntry[] Get(string topic)
        {
            WeatherEntry dummy = new WeatherEntry();
            string statement = $"SELECT * FROM mqtt WHERE Topic = '{topic}' ORDER BY id DESC";
            return MySQL.SQLRead(statement, dummy);
        }

        public static List<string> GetMeasurements()
        {
            MySqlConnection _connection = new MySqlConnection($"server=localhost;userid=Hans;password=1234;database=BBS_Wetterstation");
            List<string> measurements = new List<string>();
            string statement = $"SELECT measurement FROM measurements";
            MySqlCommand cmd = new MySqlCommand(statement, _connection);

            _connection.Open();
            using MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                measurements.Add(rdr.GetString(0));
            }
            _connection.Close();

            return measurements;
        }

        public static List<DataObject> GetDataObjects(string topic)
        {
            MySqlConnection _connection = new MySqlConnection($"server=localhost;userid=Hans;password=1234;database=BBS_Wetterstation");
            List<DataObject> dataObjects = new();
            string statement = $"SELECT Payload, Timestamp FROM {topic} ORDER BY id DESC";
            MySqlCommand cmd = new MySqlCommand(statement, _connection);

            
            _connection.Open();
            using MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                DataObject dataObject = new DataObject();
                dataObject.value = rdr.GetDouble(0);
                dataObject.timestamp = rdr.GetDateTime(1);
                dataObjects.Add(dataObject);
            }
            _connection.Close();
            return dataObjects;
        }

    }
}

