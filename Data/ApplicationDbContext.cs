using Microsoft.EntityFrameworkCore;
using TaskBackend.Models;

namespace TaskBackend.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Employee> Employee => Set<Employee>();
    public DbSet<Users> Users => Set<Users>();
    public DbSet<Department> Department => Set<Department>();
}