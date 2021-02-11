using Microsoft.AspNetCore.Identity;

namespace PrjModule24.Models
{
    public class ApplicationUser : IdentityUser
    {
        public UserProfile Profile { get; set; }
        public UserBankingAccount BankingAccount { get; set; }
    }
}