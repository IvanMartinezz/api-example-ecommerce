namespace DemoAPI.Models.Users;

using DemoAPI.Models.Data;
using System.ComponentModel.DataAnnotations;
public class AuthenticateRequest
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}