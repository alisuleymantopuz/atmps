using Microsoft.EntityFrameworkCore;
using vpos.seb.domain.infrastructure.Extensions;

namespace vpos.seb.domain.infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public virtual DbSet<BankAccount> BankAccounts { get; set; }

        public virtual DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BankAccount>(entity => {
                entity.HasIndex(e => e.Number).IsUnique();
            });

            modelBuilder.Entity<Customer>()
                  .HasMany(x => x.BankAccounts)
                  .WithOne(x => x.Customer);

            modelBuilder.Seed();
        }
    }
}
