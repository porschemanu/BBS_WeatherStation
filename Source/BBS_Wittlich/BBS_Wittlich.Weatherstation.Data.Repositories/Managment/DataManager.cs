using BBS_Wittlich.Weatherstation.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBS_Wittlich.Weatherstation.Data.Management
{
    public class DataManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public void RunCleaner()
        {
            DataCleaner();
        }

        private void DataCleaner()
        {
            Console.WriteLine("Cleaning Started");
            List<string> statements = new List<string>();
            Stopwatch stopwatch = new();
            stopwatch.Start();
            DCRepo dcRepo = new DCRepo();
            dcRepo.Source = "127.0.0.1";
            List<Models.WeatherEntry> allWeatherdata = dcRepo.Get().ToList();
            List<string> topics = new List<string>();
            
            foreach (Models.WeatherEntry entry in allWeatherdata)
            {
                if (!topics.Contains(entry.topic))
                {
                    topics.Add(entry.topic);
                }
            }

            foreach (string topic in topics)
            {
                List<Models.WeatherEntry> topicWeatherdata = dcRepo.Get(topic).OrderByDescending(e => e.timestamp).ToList();

                Console.WriteLine($"Now cleaning: {topic}");
                statements.Add($"CREATE TABLE IF NOT EXISTS {topic} (id int PRIMARY KEY NOT NULL AUTO_INCREMENT, Payload double, Timestamp datetime);");  

                while (topicWeatherdata.Count != 0) {
                    Models.WeatherEntry firstEntry = topicWeatherdata.Last();

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

                    List<Models.WeatherEntry> timespanEntries = topicWeatherdata.Where(e => e.timestamp < endDateTime && e.timestamp >= startDateTime).ToList();

                    Models.WeatherEntry finalWeatherEntry = new();
                    double sumValue = 0;

                    foreach (Models.WeatherEntry weatherEntry in timespanEntries)
                    {
                        sumValue += weatherEntry.value;
                    }
                    finalWeatherEntry.topic = topic;
                    finalWeatherEntry.value = sumValue / timespanEntries.Count;
                    finalWeatherEntry.timestamp = startDateTime;
                    statements.Add($"INSERT INTO `{ finalWeatherEntry.topic}` (`Payload`, `Timestamp`) VALUES({ finalWeatherEntry.value.ToString().Replace(',', '.')}, CAST('{finalWeatherEntry.timestamp.ToString("yyyy-MM-dd HH:mm")}' AS datetime));");
                    foreach (Models.WeatherEntry weatherEntry in timespanEntries)
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
                i2 ++;
            }

            dcRepo.SQLExecuter(SQL);
            stopwatch.Stop();
            Console.WriteLine($"DataCleaner Finished in {stopwatch.Elapsed.TotalSeconds}");
        }
    }
}
