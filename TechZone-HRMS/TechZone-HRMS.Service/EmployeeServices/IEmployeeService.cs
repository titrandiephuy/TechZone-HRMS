using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechZone_HRMS.Domain.Response;
using TechZone_HRMS.Service.EmployeeServices.EmployeeModel;

namespace TechZone_HRMS.Service.EmployeeServices
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDetail>> GetEmployees();
        Task<ActionResult<EmployeeDetail>> GetEmployeeById(int id);
        Task<EmployeeDetail> GetEmployeeByIdd(int id);
        Task<ActionResult<Result>> EditEmployee(EditEmployee editEmployee);
        Task<ActionResult<Result>> CreateEmployee(CreateEmployee createEmployee);
        Task<IActionResult> DeleteEmployee(int id);
        Task<ActionResult<EmployeeShow>> GetEmployeeByIdShow(int id);
    }
}
