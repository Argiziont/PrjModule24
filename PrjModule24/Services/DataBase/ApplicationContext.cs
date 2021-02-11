using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PrjModule24.Models;
using PrjModule24.Models.Authenticate_Models;
using PrjModule24.Services.Interfaces;

namespace PrjModule24.Services.DataBase
{
    public sealed class ApplicationContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserBankingAccount> BankingAccount { get; set; }
        public DbSet<UserProfile> Profile { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
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
