using System.ComponentModel.DataAnnotations;

namespace Note.API.View.Users
{
    public class CreateUserDto
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
