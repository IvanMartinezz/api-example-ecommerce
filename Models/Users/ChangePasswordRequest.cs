using System.ComponentModel.DataAnnotations;

namespace DemoAPI.Models.Users
{
    public class ChangePasswordRequest
    {
        [Required]
        public string Password { get; set; }
        [Required]
        public string NewPassword { get; set; }
    }
}
