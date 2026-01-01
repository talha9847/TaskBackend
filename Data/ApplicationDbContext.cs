using Microsoft.EntityFrameworkCore;
using TaskBackend.Models;

namespace TaskBackend.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Employee> Users => Set<Employee>();
}