using System.Collections.Generic;
using System.Threading.Tasks;
using LanguageExt;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PrjModule24.Models;

namespace PrjModule24.Services.Interfaces
{
    public interface IEfFileFolderContext
    {
        //Account Actions
        public Task<Option<Account>> GetAccount(string id);
        public Task<Option<Account>> UpdateAccountMoney(string id, decimal amount);
        public Task<Option<Account>> UpdateAccountState(string id, bool state);

        //User Actions
        public Task<List<User>> GetUsers();
        public Task<Option<User>> GetUser(string password, string name);
        public Task<Option<User>> GetUser(string id);
        public Task AddUser(User user);

        //Database Actions
        public DatabaseFacade GetDatabase();
    }
}