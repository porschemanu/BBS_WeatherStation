using Microsoft.EntityFrameworkCore;

namespace Server.Data
{
    public class DataContext : DbContext
    {
        private DataContext()
        {
            
        }

        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
            
        }
        public virtual DbSet<DataEntry> DataEntries { get; set; }
    }
}
