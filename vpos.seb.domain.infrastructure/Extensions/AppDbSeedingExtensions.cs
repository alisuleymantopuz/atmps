using System;
using Microsoft.EntityFrameworkCore;

namespace vpos.seb.domain.infrastructure.Extensions
{
    /// <summary>
    /// App db seeding extension with fake data.
    /// </summary>
    public static class AppDbSeedingExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, Name = "Jhon", Surname = "Doe" },
                new Customer { Id = 2, Name = "Jonathan", Surname = "Starski" },
                new Customer { Id = 3, Name = "Alfonso", Surname = "Arrivederci" }
                );

            modelBuilder.Entity<BankAccount>().HasData(
                new BankAccount { Id = 1, Balance = 470.30m, CustomerId = 1, Name = "Default", Number = "582567296" },
                new BankAccount { Id = 2, Balance = 1100.25m, CustomerId = 2, Name = "Default", Number = "117434127" },
                new BankAccount { Id = 3, Balance = 45000, CustomerId = 3, Name = "Default", Number = "63539843" }
               );
        }
    }
}
