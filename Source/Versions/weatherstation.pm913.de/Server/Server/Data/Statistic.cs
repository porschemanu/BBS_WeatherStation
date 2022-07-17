namespace Server.Data
{
    public class Statistic
    {
        public string IPAdress { get; set; }
        public string Version { get; set; }
        public int EntryCount { get; set; }
        public int EntryCountLast24H { get; set; }
        public List<string> Topics { get; set; }
    }
}
