using Microsoft.AspNetCore.Identity;

namespace WebAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public UserProfile Profile { get; set; }
        public UserBankingAccount BankingAccount { get; set; }
    }
}