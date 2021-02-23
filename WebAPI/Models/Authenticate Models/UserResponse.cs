using System;

namespace WebAPI.Models.Authenticate_Models
{
    public class UserResponse
    {
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}