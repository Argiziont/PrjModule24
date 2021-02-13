using System.Collections.Generic;
using System.Threading.Tasks;
using LanguageExt;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services.DataBase
{
    public class EfFileFolderContext : IEfFileFolderContext
    {
        private readonly ApplicationContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public EfFileFolderContext(ApplicationContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<List<ApplicationUser>> GetUsers()
        {
            var users = new List<ApplicationUser>();
            foreach (var user in _userManager.Users)
            {
                var userBankAccount = await GetAccountAsync(user.Id);
                userBankAccount.Match(account => user.BankingAccount = account, () => null);

                var userProfile = await GetUserProfileAsync(user.Id);
                userProfile.Match(profile => user.Profile = profile, () => null);
                users.Add(user);
            }

            return users;
        }

        public async Task<Option<ApplicationUser>> GetUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return null;

            var userBankAccount = await GetAccountAsync(user.Id);
            var userProfile = await GetUserProfileAsync(user.Id);

            userBankAccount.Match(account => user.BankingAccount = account, () => null);

            userProfile.Match(profile => user.Profile = profile, () => null);

            return user;
        }

        public async Task<Option<UserBankingAccount>> AddAccountAsync(UserBankingAccount account)
        {
            var accountEntry = await _dbContext.BankingAccount.AddAsync(account);
            await _dbContext.SaveChangesAsync();
            return accountEntry.Entity;
        }

        public async Task<Option<UserBankingAccount>> GetAccountAsync(string id)
        {
            return await _dbContext.BankingAccount.FirstOrDefaultAsync(a => a.ApplicationUser.Id == id);
        }

        public async Task<Option<UserBankingAccount>> UpdateAccountMoneyAsync(string id, decimal amount)
        {
            var account = await GetAccountAsync(id);

            return account.Match(usr =>
            {
                usr.Money += amount;
                _dbContext.SaveChanges();

                return usr;
            }, () => null);
        }

        public async Task<Option<UserBankingAccount>> UpdateAccountStateAsync(string id, bool state)
        {
            var account = await GetAccountAsync(id);

            return account.Match(usr =>
            {
                usr.State = state;
                _dbContext.SaveChanges();

                return usr;
            }, () => null);
        }

        public async Task<Option<UserProfile>> AddUserProfileAsync(UserProfile profile)
        {
            var accountEntry = await _dbContext.Profile.AddAsync(profile);
            await _dbContext.SaveChangesAsync();
            return accountEntry.Entity;
        }

        public async Task<Option<UserProfile>> GetUserProfileAsync(string id)
        {
            return await _dbContext.Profile.FirstOrDefaultAsync(a => a.ApplicationUser.Id == id);
        }
    }
}