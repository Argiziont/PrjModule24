using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using WebAPI.Models;

namespace WebAPI.Services.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<UserBankingAccount> BankingAccount { get; set; }
        public DbSet<UserProfile> Profile { get; set; }
        public DatabaseFacade GetDatabase();
        public int SaveDatabaseChanges();
    }
}