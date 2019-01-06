using atmps.domain.infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace atmps.domain.infrastructure
{
    /// <summary>
    /// App db context.
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public virtual DbSet<AuditLog> AuditLogs { get; set; }

        public virtual DbSet<BankInfo> BankInfos { get; set; }

        public virtual DbSet<BankIdentificationNumber> BankIdentificationNumbers { get; set; }

        public virtual DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BankIdentificationNumber>(entity => {
                entity.HasIndex(e => e.BIN).IsUnique();
            });

            modelBuilder.Entity<BankInfo>()
                  .HasMany(x => x.BankIdentificationNumbers)
                  .WithOne(x => x.BankInfo);

            modelBuilder.Seed();
        }
    }
}
