namespace BBS_Weatherstation_SeriesA.Data.Models
{
    public class WeatherEntry
    {
        public int Id { get; set; }
        public DateTime timestamp { get; set; }
        public string topic { get; set; }
        public double value { get; set; }
    }
}
