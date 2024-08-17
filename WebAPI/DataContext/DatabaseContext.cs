using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;

namespace WebAPI.DataContext
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<StorageLocation> StorageLocations { get; set; }
        public DbSet<BPKB> BPKB { get; set; }
    }
}
