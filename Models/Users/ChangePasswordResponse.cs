using System.ComponentModel.DataAnnotations;

namespace DemoAPI.Models.Users
{
    public class ChangePasswordResponse
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string NewPassword { get; set; }
    }
}
