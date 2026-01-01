using Microsoft.EntityFrameworkCore;
using TaskBackend.Data;
using TaskBackend.Interfaces;
using TaskBackend.Models;

namespace TaskBackend.Implementations;

public class EmpRepo : IEmpInterface
{
    private readonly ApplicationDbContext _context;
    public EmpRepo(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<List<Employee>?> GetAllEmp()
    {
        try
        {
            var employees = await (
                        from e in _context.Employee
                        join d in _context.Department
                            on e.DeptId equals d.Id
                        select new Employee
                        {
                            Id = e.Id,
                            Name = e.Name,
                            DateOfJoin = e.DateOfJoin,
                            Salary = e.Salary,
                            YOE = e.YOE,
                            DepartmentName = d.Name,
                            DeptId = e.DeptId
                        }).ToListAsync();

            return employees;
        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine("Error: " + ex.Message);
            return null;
        }
    }

    public async Task<int> AddEmp(Employee employee)
    {
        try
        {
            await _context.Employee.AddAsync(employee);
            await _context.SaveChangesAsync();
            return 1;
        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine("Error: " + ex.Message);
            return -1;
        }
    }
    public async Task<Employee?> GetEmpById(int empId)
    {
        try
        {
            var emp = await _context.Employee.FindAsync(empId);
            return emp;
        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine("Error: " + ex.Message);
            return null;
        }
    }
}