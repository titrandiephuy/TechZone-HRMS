using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechZone_HRMS.Service.EmployeeServices;
using TechZone_HRMS.Service.EmployeeServices.EmployeeModel;
using TechZone_HRMS.Service.SalaryServices;

namespace TechZone_HRMS.MVC.Controllers
{
    public class SalaryController : Controller
    {
        private readonly ISalaryService salaryService;
        private readonly IEmployeeService employeeService;
        private static EmployeeDetail employee = new EmployeeDetail();

        // GET: /<controller>/
        public SalaryController(ISalaryService salaryService, IEmployeeService employeeService)
        {
            this.salaryService = salaryService;
            this.employeeService = employeeService;
        }
        [HttpGet]
        [Route("/Salary")]
        
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("/Salary/SalaryDetail/{empId}")]
        public async Task<IActionResult> SalaryDetailAsync(int empId)
        {
            var data = await salaryService.GetSalaryById(empId);
            var employee =  employeeService.GetEmployeeById(empId).Result.Value;
            ViewBag.Employee = employee;
            return View(data);
        }
        [HttpGet]
        [Route("/Salary/Payslip/{empId}/{salId}")]
        public async Task<IActionResult> Payslip(int empId, int salId)
        {
            var data = await salaryService.GetSalaryDetailById(empId, salId);
            return View(data);
        }
    }
}
