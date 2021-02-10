using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PrjModule24.Services.Interfaces;

namespace PrjModule24.Models.DataBase
{
    public sealed class ApplicationContext : DbContext, IApplicationDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(a => a.Account)
                .WithOne(a => a.User)
                .HasForeignKey<Account>(c => c.UserId);
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
