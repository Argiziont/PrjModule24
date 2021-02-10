using System;
using System.Collections.Generic;
using PrjModule24.Models;
using PrjModule24.Models.Shared;

namespace PrjModule24.Controllers
{
    public static class UserStab
    {
        public static readonly IList<User> UsersDb = new List<User>
        {
            new()
            {
                Account = new Account {Money = 1000},
                Id = Guid.NewGuid().ToString(),
                Name = "Nick",
                Surname = "Surname 0",
                Password = "123456789",
                Role = Role.User
            },
            new()
            {
                Account = new Account {Money = 20},
                Id = Guid.NewGuid().ToString(),
                Name = "Mike",
                Surname = "Surname 1",
                Password = "123456789",
                Role = Role.Admin
            },
            new()
            {
                Account = new Account {Money = 300},
                Id = Guid.NewGuid().ToString(),
                Name = "Tim",
                Surname = "Surname 2",
                Password = "123456789",
                Role = Role.User
            },
            new()
            {
                Account = new Account {Money = 40},
                Id = Guid.NewGuid().ToString(),
                Name = "Zak",
                Surname = "Surname 3",
                Password = "123456789",
                Role = Role.Moderator
            }
        };
    }
}