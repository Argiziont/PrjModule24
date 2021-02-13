using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using WebAPI.Models;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services.DataBase
{
    public sealed class ApplicationContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<UserBankingAccount> BankingAccount { get; set; }
        public DbSet<UserProfile> Profile { get; set; }

        public DatabaseFacade GetDatabase()
        {
            return Database;
        }

        public int SaveDatabaseChanges()
        {
            return SaveChanges();
        }
    }
}