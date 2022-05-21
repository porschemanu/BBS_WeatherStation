using System;
using System.Diagnostics;
using BBS_Weatherstation_Alpha.Data.MySQLRepo;
using MySql.Data.MySqlClient;

namespace BBS_Weatherstation_Alpha.Data.DataCleanerRepo
{
	public class DataCleanerRepo
	{

        private static MySqlConnection ConnectionBuilder()
        {
            MySqlConnection sqlConnection = new MySqlConnection($"server=localhost;userid=Hans;password=1234;database=BBS_Wetterstation");
            return sqlConnection;
        }

        private static WeatherEntry[] SQLReader(string statement)
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
                weatherEntry.topic = rdr.GetString(1);
                weatherEntry.value = rdr.GetDouble(2);
                weatherEntry.timestamp = rdr.GetDateTime(3);
                weatherEntries.Add(weatherEntry);
            }

            mySqlConnection.Close();

            return weatherEntries.ToArray();
        }

        public static void SQLExecuter(string statement)
        {
            MySqlConnection mySqlConnection = ConnectionBuilder();

            MySqlCommand cmd = new MySqlCommand(statement, mySqlConnection);

            List<WeatherEntry> weatherEntries = new();

            mySqlConnection.Open();
            cmd.ExecuteNonQuery();
            mySqlConnection.Close();
        }

        public static WeatherEntry[] Get()
        {
            string statement = $"SELECT * FROM MQTT ORDER BY id DESC";
            return SQLReader(statement);
        }

        public static WeatherEntry[] Get(string topic)
        {
            string statement = $"SELECT * FROM MQTT WHERE Topic = '{topic}' ORDER BY id DESC";
            return SQLReader(statement);
        }

        public static WeatherEntry[] Get(string topic, DateTime date)
        {
            string statement = $"SELECT * FROM MQTT WHERE Timestamp BETWEEN '{date.AddDays(-1).ToString("yyyy-MM-dd HH:mm:ss")}' AND '{date.Date.ToString("yyyy-MM-dd HH:mm:ss")}' ORDER BY id DESC";
            return SQLReader(statement);
        }

        public static WeatherEntry[] Get(string topic, DateTime startDate, DateTime endDate)
        {
            string statement = $"SELECT * FROM MQTT WHERE Topic = '{topic}' AND Timestamp BETWEEN '{startDate.ToString("yyyy-MM-dd HH:mm:ss")}'AND '{endDate.ToString("yyyy-MM-dd HH:mm:ss")}' ORDER BY id DESC";
            return SQLReader(statement);
        }

        public static WeatherEntry Get(int id)
        {
            throw new NotImplementedException();
        }

        public static WeatherEntry GetLast(string topic)
        {
            string statement = $"SELECT * FROM MQTT WHERE Topic = '{topic}' LIMIT 1";
            return SQLReader(statement).FirstOrDefault();
        }

        public static async Task DataCleaner()
        {
            Console.WriteLine("Cleaning Started");
            List<string> statements = new List<string>();
            Stopwatch stopwatch = new();
            stopwatch.Start();
            List<WeatherEntry> allWeatherdata = Get().ToList();
            List<string> topics = new List<string>();

            foreach (WeatherEntry entry in allWeatherdata)
            {
                if (!topics.Contains(entry.topic))
                {
                    topics.Add(entry.topic);
                }
            }

            foreach (string topic in topics)
            {
                List<WeatherEntry> topicWeatherdata = Get(topic).OrderByDescending(e => e.timestamp).ToList();

                Console.WriteLine($"Now cleaning: {topic}");
                statements.Add($"CREATE TABLE IF NOT EXISTS {topic} (id int PRIMARY KEY NOT NULL AUTO_INCREMENT, Payload double, Timestamp datetime);");

                while (topicWeatherdata.Count != 0)
                {
                    WeatherEntry firstEntry = topicWeatherdata.Last();

                    DateTime startDateTime = firstEntry.timestamp.AddSeconds(-1);
                    DateTime endDateTime;

                    if (startDateTime.Minute % 10 == 0)
                    {
                        endDateTime = startDateTime.AddMinutes(10);
                    }
                    else
                    {
                        endDateTime = startDateTime;
                        while (endDateTime.Minute % 10 != 0)
                        {
                            endDateTime = endDateTime.AddMinutes(1);
                        }
                    }

                    List<WeatherEntry> timespanEntries = topicWeatherdata.Where(e => e.timestamp < endDateTime && e.timestamp >= startDateTime).ToList();

                    WeatherEntry finalWeatherEntry = new();
                    double sumValue = 0;

                    foreach (WeatherEntry weatherEntry in timespanEntries)
                    {
                        sumValue += weatherEntry.value;
                    }
                    finalWeatherEntry.topic = topic;
                    finalWeatherEntry.value = sumValue / timespanEntries.Count;
                    finalWeatherEntry.timestamp = startDateTime;
                    statements.Add($"INSERT INTO `{ finalWeatherEntry.topic}` (`Payload`, `Timestamp`) VALUES({ finalWeatherEntry.value.ToString().Replace(',', '.')}, CAST('{finalWeatherEntry.timestamp.ToString("yyyy-MM-dd HH:mm")}' AS datetime));");
                    foreach (WeatherEntry weatherEntry in timespanEntries)
                    {
                        topicWeatherdata.Remove(weatherEntry);
                    }
                }

            }
            statements.Add("TRUNCATE TABLE MQTT;");
            string SQL = "";
            int i2 = 1;
            foreach (string statement in statements)
            {
                SQL += statement;
                Console.WriteLine($"{i2}/{statements.Count}");
                //dcRepo.SQLExecuter(statement);
                i2++;
            }

            SQLExecuter(SQL);
            stopwatch.Stop();
            Console.WriteLine($"DataCleaner Finished in {stopwatch.Elapsed.TotalSeconds}");
            Task.Delay(6000).Wait();
            DataCleaner();
        }
    }
}

