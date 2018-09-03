using Microsoft.EntityFrameworkCore;

namespace KSManager_API.DB
{
    public class KsDatabase : DbContext
    {
        public KsDatabase(DbContextOptions<KsDatabase> options)
            :base(options)
        {
        }

        public DbSet<User> User { get; set; }

        public DbSet<PasswordStorageData> PasswordStorageDatas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\KSManager;Database=KSManager;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<PasswordStorageData>()
                .Property(psd => psd.LastChanges)
                .HasDefaultValueSql("GETUTCDATE()")
                .ValueGeneratedOnAddOrUpdate();

            modelBuilder.Entity<PasswordStorageData>()
                .Property(psd => psd.IsDeleted)
                .HasDefaultValue(false);

            modelBuilder.Entity<PasswordStorageData>().Property(psd => psd.Icon).HasDefaultValueSql("1");
        }
    }
}
