namespace WeatherstationVers._1.Models
{
    public class DataEntry
    {
        public int Id { get; set; }
        public Topic Measurement { get; set; }
        public double Value { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public enum Topic
    {
        temperature,
        humidity,
        airpressure
    }
}
