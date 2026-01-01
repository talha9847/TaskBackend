using TaskBackend.Models;

namespace TaskBackend.Interfaces;

public interface IUserInterface
{
    Task<Users?> Login(string email, string password);
}