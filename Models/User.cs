namespace auth0_demo.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string? Auth0ID { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public bool EmailVerified { get; set; }
        public string? Role { get; set; } // Role as per Auth0
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime LastLogin { get; set; }
    }
}
