using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PrjModule24.Models.Shared;

namespace PrjModule24.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public string Login { get; set; }
        
        [Required]
        public Role Role { get; set; }
        
        [MinLength(6)]
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        public UserProfile Profile { get; set; }
        public UserBankingAccount BankingAccount { get; set; }
    }
}