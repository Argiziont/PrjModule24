using System;

namespace PrjModule24.Models
{
    public class UserBankingAccount
    {
        public int Id { get; set; }
        public decimal Money { get; set; }
        public bool State { get; set; }
        
        
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}