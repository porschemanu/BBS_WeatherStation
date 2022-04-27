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
            Stopwatch stopwatch = new();
            Console.WriteLine("Cleaning Started");
            stopwatch.Start();
            SQLClientRepo sQLClientRepo = new SQLClientRepo();
            sQLClientRepo.Source = "192.168.0.10";

            Models.WeatherEntry[] weatherdata = sQLClientRepo.Get();
            
            List<string> topics = new List<string>();
            foreach (Models.WeatherEntry entry in weatherdata)
            {
                if (!topics.Contains(entry.topic))
                {
                    topics.Add(entry.topic);
                }
            }

            string statement = "";
            foreach (string topic in topics)
            {
                Console.WriteLine($"Now cleaning: {topic}");
                
                statement = $"CREATE TABLE IF NOT EXISTS {topic} (id int PRIMARY KEY NOT NULL AUTO_INCREMENT, Payload double, Timestamp datetime);";
                sQLClientRepo.SQLExecuter(statement);

                Console.WriteLine($"Table created");

                bool unsortedEntries = true;
                Models.WeatherEntry firstEntry = sQLClientRepo.GetLast(topic);
                int i = 0;
                while (unsortedEntries) {
                    Console.WriteLine($"Operation: {i}");

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

                    Models.WeatherEntry[] weatherEntries = sQLClientRepo.Get(topic, startDateTime, endDateTime);

                    Models.WeatherEntry finalWeatherEntry = new();
                    double sumValue = 0;

                    foreach (Models.WeatherEntry weatherEntry in weatherEntries)
                    {
                        sumValue += weatherEntry.value;
                    }

                    finalWeatherEntry.topic = topic;
                    finalWeatherEntry.value = sumValue / weatherEntries.Length;
                    finalWeatherEntry.timestamp = startDateTime;


                    Console.WriteLine("Inserting...");
                    statement = $"INSERT INTO `{ finalWeatherEntry.topic}` (`Payload`, `Timestamp`) VALUES({ finalWeatherEntry.value.ToString().Replace(',', '.')}, CAST('{finalWeatherEntry.timestamp.ToString("yyyy-MM-dd HH:mm")}' AS datetime));";
                    sQLClientRepo.SQLExecuter(statement);

                    Console.WriteLine("Deleting...");
                    foreach (Models.WeatherEntry weatherEntry in weatherEntries)
                    {
                        statement = $"DELETE FROM `MQTT` WHERE id = {weatherEntry.Id}";
                        sQLClientRepo.SQLExecuter(statement);
                    }
                    firstEntry = sQLClientRepo.GetLast(topic);
                    if(firstEntry == null) { unsortedEntries = false; }
                    i++;
                }
            }
            stopwatch.Stop();
            Console.WriteLine($"DataCleaner Finished in {stopwatch.Elapsed.TotalSeconds}");
        }
    }
}
