using Server.Data;


namespace Server.Repositories
{


    public class DataRepo
    {
        private readonly DataContext _context;

        public DataRepo(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public List<DataEntry> Get(DateTime start, DateTime end, string topic)
        {
            return _context.DataEntries.Where(x => x.Timestamp >= start && x.Timestamp <= end && x.Topic == topic).ToList();
        }

        public List<string> GetTopics()
        {
            List<string> topics = new List<string>();
            foreach (DataEntry entry in _context.DataEntries.Where(x => !topics.Contains(x.Topic)))
            {
                topics.Add(entry.Topic);
            }
            return topics.Distinct().ToList(); ;
        }
        
    }
}
