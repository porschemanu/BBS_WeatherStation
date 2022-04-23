using BBS_Wittlich.Weatherstation.Data.Repositories;
using System;
using System.Collections.Generic;
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
            // Role Pattern?

            //Establish Database Connection - Connector

            //Fetch Local Copy of Database - Fetcher

            //Analyse existing Topics _ Analyser

            //Build SQL Statement for Topics Table - Builder

            //Execute SQL Statement - Executer

            //Analyse the Entries in the Database - Analyser

            //Averaging the 10 min entries into 1 - Merger

            //Execute SQL Statement - Executer

            TableBuilder();
        }


        private void Connector()
        {

        }

        private void Fetcher()
        {

        }

        private void Analyser()
        {

        }

        private void Builder()
        {

        }

        private void Executer()
        {

        }

        private void Merger()
        {

        }

        /// <summary>
        /// Scans the Database to create a Table for each commited Topics.
        /// Fills the Tables with the seperate Datasets.
        /// </summary>
        /// <returns></returns>
        private void TableBuilder()
        {
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
                statement = $"CREATE TABLE IF NOT EXISTS {topic} (id int PRIMARY KEY NOT NULL AUTO_INCREMENT, Payload double, Timestamp datetime);";
                sQLClientRepo.SQLExecuter(statement);
            }
            
            foreach (Models.WeatherEntry entry1 in weatherdata)
            {
                statement = $"INSERT INTO `{entry1.topic}` (`Payload`, `Timestamp`) VALUES ({entry1.value.ToString().Replace(',', '.')}, CAST('{entry1.timestamp.ToString("yyyy-MM-dd HH:mm")}' AS datetime));";
                sQLClientRepo.SQLExecuter(statement);
            }
           
            Console.WriteLine("SQL Finished");
        }

        private void DataMover()
        {

        }
    }
}
