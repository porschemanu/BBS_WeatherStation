using BBS_Weatherstation_SeriesA.Data.Global;
using static BBS_Weatherstation_SeriesA.Data.Global.Storage;

namespace BBS_Weatherstation_SeriesA.Data.Models
{
    public class DataQuery
    {
        public string measurement { get; set; } = "all";
        
        public timespan timespan { get; set; }
        
        public List<Storage.DataList> dataObjects { get; set; }
        
        public string format { get; set; }

    }

    public enum timespan
    {
        today,
        yesterday,
        lastweek,
        lastmonth,
        lastyear
    }

}
