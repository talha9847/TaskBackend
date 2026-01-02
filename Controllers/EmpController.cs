using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskBackend.Interfaces;
using TaskBackend.Models;

namespace TaskBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmpController : ControllerBase
{

    private readonly IEmpInterface _empRepo;
    public EmpController(IEmpInterface empRepo)
    {
        _empRepo = empRepo;
    }

    [Authorize(Roles = "Admin,User")]
    [HttpGet("fetchemployees/{deptId}/{page}/{pageSize}")]
    public async Task<IActionResult> GetAllEmployees(int deptId = 0, int page = 1, int pageSize = 10, string sortBy = "id", string orderBy = "asc")
    {
        System.Console.WriteLine(orderBy);
        var employees = await _empRepo.GetAllEmp(deptId, page, pageSize, sortBy, orderBy);

        if (employees == null || !employees.Any())
        {
            return NotFound(new { message = "No employees found", success = false });
        }

        var totalCount = await _empRepo.GetEmployeeCount(deptId);

        return Ok(new
        {
            message = "Employees loaded successfully",
            success = true,
            employees,
            page,
            pageSize,
            totalCount,
            totalPages = (int)Math.Ceiling((double)totalCount / pageSize)
        });
    }
    [Authorize(Roles = "Admin")]
    [HttpPost("AddEmployee")]
    public async Task<IActionResult> AddEmp(Employee employee)
    {
        var result = await _empRepo.AddEmp(employee);
        if (result == 1)
        {
            return Ok(new { message = "Employee Added successfully", success = true });
        }
        return BadRequest(new { message = "Error in Adding Employee", success = false });

    }

    [Authorize(Roles = "Admin")]
    [HttpGet("GetEmpById/{empId}")]
    public async Task<IActionResult> GetEmpById(int empId)
    {
        var emp = await _empRepo.GetEmpById(empId);
        if (emp == null)
        {
            return BadRequest(new { message = "No employee found", success = false });
        }
        return Ok(new { message = "Employee getting successfull", emp, success = true });
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("UpdateEmp")]
    public async Task<IActionResult> UpdateEmp(Employee emp)
    {
        emp.DeptId = Convert.ToInt32(emp.DeptId);

        var result = await _empRepo.UpdateEmp(emp);
        if (result >= 0)
        {
            return Ok(new { message = "Employee Updated successfully", success = true });
        }
        return BadRequest(new { message = "Errorr in updating Employee", success = false });
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("GetEmpByDeptById/{deptId}")]
    public async Task<IActionResult> GetEmpByDeptId(int deptId)
    {
        var emp = await _empRepo.GetEmpByDeptById(deptId);
        if (emp == null)
        {
            return BadRequest(new { message = "Errorr in getting Employees", success = false });
        }
        return Ok(new { message = "Employees getting successfully", success = true, emp });

    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("DeleteEmp/{empId}")]
    public async Task<IActionResult> DeleteEmp(int empId)
    {
        var result = await _empRepo.DeleteEmp(empId);
        if (result == 1)
        {
            return Ok(new { message = "Employees deleted successfully", success = true, });
        }
        return BadRequest(new { message = "Errorr in getting Employees", success = false });
    }
    [HttpGet("GetAllDept")]
    public async Task<IActionResult> GetAllDepartments()
    {
        var depts = await _empRepo.GetAllDepartments();
        if (depts == null)
        {
            return BadRequest(new { message = "Errorr in getting depts", success = false });

        }
        return Ok(new { message = "Depts loaded successfully", success = true, depts });

    }

}
