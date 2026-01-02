using TaskBackend.Models;

namespace TaskBackend.Interfaces;

public interface IEmpInterface
{
    Task<List<Employee>?> GetAllEmp(int deptId = 0, int page = 1, int pageSize = 10, string sortBy = "id",
    string sortOrder = "asc");
    Task<int> AddEmp(Employee emp);
    Task<Employee?> GetEmpById(int empId);
    Task<int> UpdateEmp(Employee emp);
    Task<List<Employee>?> GetEmpByDeptById(int deptId);
    Task<int> DeleteEmp(int empId);
    Task<List<Department>?> GetAllDepartments();
    Task<int> GetEmployeeCount(int deptId);
}