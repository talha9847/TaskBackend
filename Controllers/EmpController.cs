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

    // [Authorize(Roles = "Admin,User")]
    [HttpGet("fetchemployees")]
    public async Task<IActionResult> GetAllEmployees()
    {
        var employees = await _empRepo.GetAllEmp();
        if (employees == null)
        {
            return BadRequest(new { message = "No emp found", success = false });
        }
        return Ok(new { message = "Employee getting successfully", success = true, employees });
    }
    // [Authorize(Roles = "Admin")]
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

    // [Authorize(Roles = "Admin")]
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

    // [Authorize(Roles = "Admin")]
    [HttpPut("UpdateEmp")]
    public async Task<IActionResult> UpdateEmp(Employee emp)
    {
        var result = await _empRepo.UpdateEmp(emp);
        if (result == 1)
        {
            return Ok(new { message = "Employee Updated successfully", success = true });
        }
        return BadRequest(new { message = "Errorr in updating Employee", success = false });
    }

    // [Authorize(Roles = "Admin")]
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

    // [Authorize(Roles = "Admin")]
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

}
