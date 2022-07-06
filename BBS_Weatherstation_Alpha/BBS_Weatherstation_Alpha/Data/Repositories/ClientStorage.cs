using BBS_Weatherstation_SeriesA.Data.Global;
using BBS_Weatherstation_SeriesA.Data.Models;
using static BBS_Weatherstation_SeriesA.Data.Global.Storage;

namespace BBS_Weatherstation_SeriesA.Data.Repositories
{
	public class ClientStorage
	{
        public static DataQuery Get(DataQuery query, List<Storage.DataList> serverStorage)
        {
            if (query.measurement == "all")
            {
                foreach (DataList dataList in serverStorage)
                {
                    DataList queryObjects = new();
                    queryObjects.topic = dataList.topic;
                    
                    switch (query.timespan)
                    {
                        case timespan.today:
                            foreach (DataObject dataObject in dataList.dataObjects)
                            {
                                if(dataObject.timestamp.Date == DateTime.UtcNow.Date)
                                {
                                    queryObjects.dataObjects.Add(dataObject);
                                }
                            }

                            break;
                        case timespan.yesterday:
                            foreach (DataObject dataObject in dataList.dataObjects)
                            {
                                if (dataObject.timestamp.Date > DateTime.UtcNow.Date.AddDays(-1))
                                {
                                    queryObjects.dataObjects.Add(dataObject);
                                }
                            }
                            break;
                        case timespan.lastweek:
                            break;
                        case timespan.lastmonth:
                            foreach (DataObject dataObject in dataList.dataObjects)
                            {
                                if (dataObject.timestamp.Date > DateTime.UtcNow.Date.AddDays(-30))
                                {
                                    queryObjects.dataObjects.Add(dataObject);
                                }
                            }
                            break;
                        case timespan.lastyear:
                            break;
                        default:
                            break;
                    }
                }
            }
            return query;
        }
    }
}
