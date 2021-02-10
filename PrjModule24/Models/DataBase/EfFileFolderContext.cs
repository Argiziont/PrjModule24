using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PrjModule24.Services.Interfaces;

namespace PrjModule24.Models.DataBase
{
    public class EfFileFolderContext:IEfFileFolderContext
    {
        private readonly ApplicationContext _dbContext;

        public EfFileFolderContext(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Option<Account>> GetAccount(string id)
        {
            var user = await GetUser(id);

            return user.Match((usr) => usr.Account, () => null);
        }

        public Task<Option<Account>> UpdateAccountMoney(string id, decimal amount)
        {
            throw new System.NotImplementedException();
        }

        public Task<Option<Account>> UpdateAccountState(string id, bool state)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<User>> GetUsers()
        {
            throw new System.NotImplementedException();
        }

        public Task<Option<User>> GetUser(string password, string name)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Option<User>> GetUser(string id)
        {
            return await _dbContext.Users.FirstAsync(usr => usr.Id == id);
        }

        public async Task AddUser(User user)
        {
            await _dbContext.Users.AddAsync(user);
            _dbContext.SaveDatabaseChanges();
        }

        public DatabaseFacade GetDatabase()
        {
            throw new System.NotImplementedException();
        }
    }
}