using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBS_Wittlich.Weatherstation.Data
{
    public class Query
    {
        public string Source { get; set; } = "127.0.0.1";
        public string UnitOfWork { get; set; } = "sqlclient";
        public string Topic { get; set; } = "temp";
        public string Timespan { get; set; } = "all";
    }

    public class Respone
    {
        public string Format { get; set; } 
        public Models.WeatherEntry[] Result { get; set; }
    }

    public class QueryManager {

        public Respone QueryAdministration(Query query)
        {
            Data.Interfaces.IRepositories DataRepo = new Repositories.MockRepo();

            switch (query.UnitOfWork)
            {
                case "sqlclient":
                    DataRepo = new Repositories.SQLClientRepo();
                    break;

                case "efcore":
                    DataRepo = new Repositories.EFCoreRepo();
                    break;

                case "localstorage":
                    DataRepo = new Repositories.LocalRepo();
                    break;
            }

            DataRepo.Source = query.Source;

            Respone respone = new Respone();
            switch (query.Timespan)
            {
                case "today":
                    respone.Result = DataRepo.Get(query.Topic, DateTime.Today);
                    break;

                case "yesterday":
                    respone.Result = DataRepo.Get(query.Topic, DateTime.Today.AddDays(-1));
                    break;

                case "week":
                    respone.Result = DataRepo.Get(query.Topic,DateTime.Today.AddDays(-7), DateTime.Today);
                    break;

                case "month":
                    respone.Result = DataRepo.Get(query.Topic, DateTime.Today.AddDays(-30), DateTime.Today);
                    break;

                case "year":
                    respone.Result = DataRepo.Get(query.Topic, DateTime.Today.AddDays(-365), DateTime.Today);
                    break;

                case "all":
                    respone.Result = DataRepo.Get(query.Topic);
                    break;

                default:
                    break;
            }

            respone.Format = "";
            return respone;
        }
    }
}