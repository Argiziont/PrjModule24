using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PrjModule24.Models;

namespace PrjModule24.Services.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserBankingAccount> BankingAccount { get; set; }
        public DbSet<UserProfile> Profile { get; set; }
        public DatabaseFacade GetDatabase();
        public int SaveDatabaseChanges();
    }
}