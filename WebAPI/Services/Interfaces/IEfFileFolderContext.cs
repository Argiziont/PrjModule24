using System.Collections.Generic;
using System.Threading.Tasks;
using LanguageExt;
using WebAPI.Models;

namespace WebAPI.Services.Interfaces
{
    public interface IEfFileFolderContext
    {
        //ApplicationUser Actions
        public Task<List<ApplicationUser>> GetUsers();
        public Task<Option<ApplicationUser>> GetUser(string id);

        //UserBankingAccount Actions
        public Task<Option<UserBankingAccount>> AddAccountAsync(UserBankingAccount account);
        public Task<Option<UserBankingAccount>> GetAccountAsync(string id);
        public Task<Option<UserBankingAccount>> UpdateAccountMoneyAsync(string id, decimal amount);
        public Task<Option<UserBankingAccount>> UpdateAccountStateAsync(string id, bool state);

        //UserProfile Actions
        public Task<Option<UserProfile>> AddUserProfileAsync(UserProfile profile);
        public Task<Option<UserProfile>> GetUserProfileAsync(string id);
    }
}