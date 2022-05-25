namespace BBS_Weatherstation_SeriesA.Data.Global
{
    public class Cache
    {

        public static List<DataList>? Data { get; set; }
        public static List<string>? Measurements { get; set; }

        public class DataList
        {
            public string topic { get; set; }
            public List<DataObject> dataObjects { get; set; }
        }

        public class DataObject
        {
            public DateTime timestamp { get; set; }
            public double value { get; set; }
        }
    }
}
