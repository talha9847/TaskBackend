using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskBackend.Models;

[Table("users")]
public class Users
{
    [Column("id")]         
    public int Id { get; set; }
    [Required]
    [Column("email")]         
    public string? Email { get; set; }
    [Required]
    [Column("password")]         
    public string? Password { get; set; }
    [Column("role")]         
    public string? Role { get; set; }
}