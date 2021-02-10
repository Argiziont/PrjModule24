using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrjModule24.Models
{
    public record UserBankingAccount
    {
        public int Id { get; set; }
        public decimal Money { get; set; }
        public bool State { get; set; }
        
        
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}