using LanguageExt;
using Microsoft.EntityFrameworkCore;
using PrjModule24.Models;
using PrjModule24.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrjModule24.Services.DataBase
{
    public class EfFileFolderContext : IEfFileFolderContext
    {
        private readonly ApplicationContext _dbContext;

        public EfFileFolderContext(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Option<UserBankingAccount>> GetAccountAsync(Guid id)
        {

            return await _dbContext.BankingAccount.FirstOrDefaultAsync(a => a.User.Id == id);

        }

        public async Task<Option<UserBankingAccount>> UpdateAccountMoneyAsync(Guid id, decimal amount)
        {
            var account = await GetAccountAsync(id);

            return account.Match((usr) =>
            {

                usr.Money += amount;
                _dbContext.SaveChanges();

                return usr;
            }, () => null);

        }

        public async Task<Option<UserBankingAccount>> UpdateAccountStateAsync(Guid id, bool state)
        {
            var account = await GetAccountAsync(id);

            return  account.Match((usr) =>
            {

                usr.State = state;
                 _dbContext.SaveChanges();

                return usr;
            }, () => null);

        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _dbContext.Users
                .Include(u => u.Profile)
                .Include(u => u.BankingAccount)
                .ToListAsync();

        }

        public Task<Option<User>> GetUserAsync(string password, string name)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Option<User>> GetUserAsync(Guid id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(usr => usr.Id == id);
        }

        public async Task AddUserAsync(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            #region StartValue

            //await using (_dbContext)
            //{
            //   await _dbContext.Database.EnsureDeletedAsync();
            //   await _dbContext.Database.EnsureCreatedAsync();
            //    var user1 = new User { Login = "Kate", Password = "123456789", Id = Guid.NewGuid(), Role = Role.Admin };
            //    var user2 = new User { Login = "Drake", Password = "123456789", Id = Guid.NewGuid(), Role = Role.User };
            //    var user3 = new User { Login = "Jake", Password = "123456789", Id = Guid.NewGuid(), Role = Role.Moderator };

            //    await _dbContext.Users.AddRangeAsync(user1, user2, user3);

            //    var account1 = new UserBankingAccount { User = user1, Money = 1000 };
            //    var account2 = new UserBankingAccount { User = user2, Money = 1000 };
            //    var account3 = new UserBankingAccount { User = user3, Money = 1000 };

            //    await _dbContext.BankingAccount.AddRangeAsync(account1, account2, account3);

            //    var profile1 = new UserProfile() { User = user1, Name = "Kate", Age = 24 };
            //    var profile2 = new UserProfile() { User = user2, Name = "Drake", Age = 31 };
            //    var profile3 = new UserProfile() { User = user3, Name = "Jake", Age = 50 };
            //    await _dbContext.Profile.AddRangeAsync(profile1, profile2, profile3);

            //    await _dbContext.SaveChangesAsync();
            //}

            #endregion


            var account = user.BankingAccount;
            var profile = user.Profile;

            user.BankingAccount = null;
            user.Profile = null;

            await _dbContext.Users.AddAsync(user);
            await _dbContext.BankingAccount.AddAsync(account);
            await _dbContext.Profile.AddAsync(profile);

            await _dbContext.SaveChangesAsync();

        }
    }
}