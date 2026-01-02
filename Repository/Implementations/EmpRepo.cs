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
    public async Task<List<Employee>?> GetAllEmp(int deptId = 0, int page = 1, int pageSize = 10, string sortBy = "id",
    string sortOrder = "asc")
    {
        try
        {
            var query = from e in _context.Employee
                        join d in _context.Department
                            on e.DeptId equals d.Id
                        where e.isDeleted == false
                        select new Employee
                        {
                            Id = e.Id,
                            Name = e.Name,
                            DateOfJoin = e.DateOfJoin,
                            Salary = e.Salary,
                            YOE = e.YOE,
                            DepartmentName = d.Name,
                            DeptId = e.DeptId
                        };

            if (deptId > 0)
            {
                query = query.Where(e => e.DeptId == deptId);
            }


            query = sortBy.ToLower() switch
            {
                "name" => sortOrder == "desc"
                    ? query.OrderByDescending(e => e.Name)
                    : query.OrderBy(e => e.Name),

                "salary" => sortOrder == "desc"
                    ? query.OrderByDescending(e => e.Salary)
                    : query.OrderBy(e => e.Salary),

                "yoe" => sortOrder == "desc"
                    ? query.OrderByDescending(e => e.YOE)
                    : query.OrderBy(e => e.YOE),

                _ => sortOrder == "desc"
                    ? query.OrderByDescending(e => e.Id)
                    : query.OrderBy(e => e.Id)
            };

            var employees = await query
             .Skip((page - 1) * pageSize)
             .Take(pageSize)
             .ToListAsync();

            return employees;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
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
            var emp = await _context.Employee.Where(e => e.isDeleted == false && e.Id == empId).FirstOrDefaultAsync();
            return emp;
        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine("Error: " + ex.Message);
            return null;
        }
    }

    public async Task<int> UpdateEmp(Employee emp)
    {
        try
        {
            var existingEmp = await _context.Employee.FindAsync(emp.Id);
            if (existingEmp == null)
            {
                return -1;
            }
            existingEmp.Name = emp.Name;
            existingEmp.DeptId = emp.DeptId;
            existingEmp.DateOfJoin = emp.DateOfJoin;
            existingEmp.Salary = emp.Salary;
            existingEmp.YOE = emp.YOE;

            return await _context.SaveChangesAsync();

        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine("Error: " + ex.Message);
            return -1;
        }
    }

    public async Task<List<Employee>?> GetEmpByDeptById(int deptId)
    {
        try
        {
            var emp = await _context.Employee.Where(e => e.DeptId == deptId && e.isDeleted == false).ToListAsync();
            return emp;
        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine("Error: " + ex.Message);
            return null;
        }
    }

    public async Task<int> DeleteEmp(int empId)
    {
        try
        {
            var emp = await _context.Employee.Where(e => e.isDeleted == false && e.Id == empId).FirstOrDefaultAsync();
            if (emp == null)
            {
                return 0;
            }
            emp.isDeleted = true;
            return await _context.SaveChangesAsync();
        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine("Error: " + ex.Message);
            return -1;
        }
    }


    public async Task<List<Department>?> GetAllDepartments()
    {
        try
        {
            var depts = await _context.Department.ToListAsync();
            return depts;
        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine("Erorr: " + ex.Message);
            return null;
        }
    }

    public async Task<int> GetEmployeeCount(int deptId = 0)
    {
        var query = _context.Employee.Where(e => !e.isDeleted);
        if (deptId > 0)
            query = query.Where(e => e.DeptId == deptId);

        return await query.CountAsync();
    }

}