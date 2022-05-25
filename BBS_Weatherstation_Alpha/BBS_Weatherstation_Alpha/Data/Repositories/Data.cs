using BBS_Weatherstation_SeriesA.Data.Global;
using BBS_Weatherstation_SeriesA.Data.Models;
using MySql.Data.MySqlClient;
using System.Diagnostics;


namespace BBS_Weatherstation_SeriesA.Data.Repositories
{
    public class DataRepo
    {
        public static async Task CleanData()
        {
            Console.WriteLine("Cleaning Started");
            List<string> statements = new List<string>();
            Stopwatch stopwatch = new();
            stopwatch.Start();
            List<WeatherEntry> allWeatherdata = MySQL.Get().ToList();
            List<string> topics = new List<string>();
            statements.Add($"USE bbs_wetterstation;");
            statements.Add($"CREATE TABLE IF NOT EXISTS measurements(measurement varchar(255) PRIMARY KEY NOT NULL);");

            foreach (WeatherEntry entry in allWeatherdata)
            {
                if (!topics.Contains(entry.topic))
                {
                    topics.Add(entry.topic);
                    statements.Add($"INSERT INTO measurements(measurement) VALUES('{entry.topic}');");
                }
            }

            foreach (string topic in topics)
            {
                List<WeatherEntry> topicWeatherdata = MySQL.Get(topic).OrderByDescending(e => e.timestamp).ToList();

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
                    statements.Add($"INSERT INTO `{finalWeatherEntry.topic}` (`Payload`, `Timestamp`) VALUES({finalWeatherEntry.value.ToString().Replace(',', '.')}, CAST('{finalWeatherEntry.timestamp.ToString("yyyy-MM-dd HH:mm")}' AS datetime));");
                    foreach (WeatherEntry weatherEntry in timespanEntries)
                    {
                        topicWeatherdata.Remove(weatherEntry);
                    }
                }

            }
            statements.Add("TRUNCATE TABLE MQTT;");
            string SQL = "";
            int i = 1;
            foreach (string statement in statements)
            {
                SQL += statement;
                Console.WriteLine($"{i}/{statements.Count}");
                //SQLExecuter(statement);
                i++;
            }

            try
            {
                MySQL.SQLWrite(SQL);
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e);
            }

            stopwatch.Stop();
            Console.WriteLine($"DataCleaner Finished in {stopwatch.Elapsed.TotalSeconds}");
        }

        public static async Task FillCache()
        {
            Cache.Measurements =  MySQL.GetMeasurements();

            if (Cache.Data == null) 
            {
                Cache.Data = new();
            }
            else
            {
                Cache.Data.Clear();
            }
            foreach (string measurement in Cache.Measurements)
            {
                Cache.DataList dataList = new();
                dataList.topic = measurement;
                dataList.dataObjects = MySQL.GetDataObjects(measurement);
                Cache.Data.Add(dataList);
            }
        }

    }
}
