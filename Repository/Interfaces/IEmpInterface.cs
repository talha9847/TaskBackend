using TaskBackend.Models;

namespace TaskBackend.Interfaces;

public interface IEmpInterface
{
    Task<List<Employee>?> GetAllEmp();
    Task<int> AddEmp(Employee emp);
    Task<Employee?> GetEmpById(int empId);
    Task<int> UpdateEmp(Employee emp);
    Task<List<Employee>?> GetEmpByDeptById(int deptId);
    Task<int> DeleteEmp(int empId);
}