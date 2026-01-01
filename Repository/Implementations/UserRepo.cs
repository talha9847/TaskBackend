using Microsoft.EntityFrameworkCore;
using TaskBackend.Data;
using TaskBackend.Interfaces;
using TaskBackend.Models;

namespace TaskBackend.Implementations;

public class UserRepo : IUserInterface
{
    private readonly ApplicationDbContext _context;
    public UserRepo(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Users?> Login(string email, string password)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
            return user;
        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine("Error: " + ex.Message);
            return null;
        }
    }
}