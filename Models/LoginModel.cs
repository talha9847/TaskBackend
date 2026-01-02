using System.ComponentModel.DataAnnotations;

namespace TaskBackend.Models;

public class LoginModel
{
    [Required]
    public string? Email { get; set; }
    [Required]
    public string? Password { get; set; }
}