using Microsoft.EntityFrameworkCore;

namespace WeatherstationVers._1.Models
{
    public class DataContext : DbContext
    {
        public DataContext() : base()
        {
        }

        public DbSet<DataEntry> DataEntries { get; set; }
    }
}
