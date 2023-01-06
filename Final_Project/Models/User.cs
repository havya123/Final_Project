 
namespace Final_Project.Models
{
    public partial class User
    {
        public int Id { get; set; }

        public string Username { get; set; } = null!;

        public string? Password { get; set; }
        public string? Email { get; set; }

    }
}