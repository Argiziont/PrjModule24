using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LanguageExt;
using PrjModule24.Models;

namespace PrjModule24.Services.Interfaces
{
    public interface IEfFileFolderContext
    {
        //Account Actions
        public Task<Option<UserBankingAccount>> GetAccountAsync(Guid id);
        public Task<Option<UserBankingAccount>> UpdateAccountMoneyAsync(Guid id, decimal amount);
        public Task<Option<UserBankingAccount>> UpdateAccountStateAsync(Guid id, bool state);

        //User Actions
        public Task<List<User>> GetUsersAsync();
        public Task<Option<User>> GetUserAsync(string password, string name);
        public Task<Option<User>> GetUserAsync(Guid id);
        public Task AddUserAsync(User user);
    }
}