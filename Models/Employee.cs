using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskBackend.Models;

[Table("employee")]
public class Employee
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    [Column("deptid")]
    public int DeptId { get; set; }
    [Column("name")]
    public string? Name { get; set; }
    [Column("dateofjoin")]
    public DateOnly? DateOfJoin { get; set; }
    [Column("salary")]
    public double Salary { get; set; }
    [Column("yoe")]
    public int YOE { get; set; }

    [NotMapped]
    public string? DepartmentName { get; set; }
}