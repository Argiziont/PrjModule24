namespace PrjModule24.Models
{
    public record Account
    {
        public int Id { get; set; }
        public decimal Money { get; set; }
        public bool State { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}