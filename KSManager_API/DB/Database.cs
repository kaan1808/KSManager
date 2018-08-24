using Microsoft.EntityFrameworkCore;

namespace KSManager_API.DB
{
    public class Database : DbContext
    {
        public Database(DbContextOptions<Database> options)
            :base(options)
        {
        }

        public DbSet<User> User { get; set; }

        public DbSet<PasswordStorageData> PasswordStorageDatas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\KSManager;Database=KSManager;Trusted_Connection=True;");
        }
    }
}
