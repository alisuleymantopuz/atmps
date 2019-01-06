using Microsoft.EntityFrameworkCore;

namespace atmps.domain.infrastructure.Extensions
{
    /// <summary>
    /// App db seeding extension with fake data.
    /// </summary>
    public static class AppDbSeedingExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BankInfo>().HasData(new BankInfo { Id = 1, Name = "SEB", VposUrl = "https://localhost:4000/api/Payment/", IsOperated=true });

            modelBuilder.Entity<BankIdentificationNumber>().HasData(
                new BankIdentificationNumber { Id = 1, BankInfoId =1, BIN = "556975" },
                new BankIdentificationNumber { Id = 2, BankInfoId = 1, BIN = "453242" },
                new BankIdentificationNumber { Id = 3, BankInfoId = 1, BIN = "371449" });
        }
    }
}
