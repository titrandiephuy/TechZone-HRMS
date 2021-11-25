using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechZone_HRMS.Domain.Response;
using TechZone_HRMS.Service.EmployeeServices;
using TechZone_HRMS.Service.EmployeeServices.EmployeeModel;

namespace TechZone_HRMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeAPIController : ControllerBase
    {
        private readonly IEmployeeService employeeService;

        public EmployeeAPIController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IEnumerable<EmployeeDetail>> GetEmployees()
        {
            return await employeeService.GetEmployees();
        }

        [HttpGet("id")]
        public async Task<ActionResult<EmployeeDetail>> GetEmployeeById(int id)
        {
            return await employeeService.GetEmployeeById(id);
        }

        [HttpPatch]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            return await employeeService.DeleteEmployee(id);
        }

        [HttpPost]
        public async Task<ActionResult<Result>> CreateEmployee(CreateEmployee createEmployee)
        {
            return await employeeService.CreateEmployee(createEmployee);
        }

        [HttpPut]
        public async Task<ActionResult<Result>> EditEmployee(EditEmployee editEmployee)
        {
            return await employeeService.EditEmployee(editEmployee);
        }
    }
}
