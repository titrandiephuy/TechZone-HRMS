using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using TechZone_HRMS.Domain;
using TechZone_HRMS.Service.EmployeeServices;
using TechZone_HRMS.Service.EmployeeServices.EmployeeModel;
using TechZone_HRMS.Service.SalaryServices;
using TechZone_HRMS.Service.SalaryServices.SalaryModel;

namespace TechZone_HRMS.MVC.Controllers
{
    public class SalaryController : Controller
    {
        private readonly ISalaryService salaryService;
        private readonly IEmployeeService employeeService;
        private readonly EmployeesManagementContext context;
        private static EmployeeDetail employee = new EmployeeDetail();

        // GET: /<controller>/
        public SalaryController(ISalaryService salaryService, IEmployeeService employeeService, EmployeesManagementContext context )
        {
            this.salaryService = salaryService;
            this.employeeService = employeeService;
            this.context = context;
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
        [Route("/Salary/Payslip/{salId}")]
        public async Task<IActionResult> Payslip(int salId)
        {
            var data = await salaryService.GetSalaryDetailById(salId);
            var employee =  employeeService.GetEmployeeById(data.EmployeeId).Result.Value;
            ViewBag.Employee = employee;
            return View(data);
        }

        public async Task<IActionResult> ExportSalary()
        {
            var data = (await context.Salaries.ToListAsync()).Select(s => new SalaryDetail()
            {
                SalaryId = s.SalaryId,
                SalaryDate = s.SalaryDate,
                LabourContractSalary = s.LabourContractSalary,
                MonthsWorkday = s.MonthsWorkday,
                TotalWorkday = s.TotalWorkday,
                BasicSalary = s.BasicSalary,
                TotalBonus = s.TotalBonus,
                LunchAllowance = s.LunchAllowance,
                MobilePhoneAllowance = s.MobilePhoneAllowance,
                ConveyanceAllowance = s.ConveyanceAllowance,
                PerformanceBonus = s.PerformanceBonus,
                SocialInsurance = s.SocialInsurance,
                PersonalIncomeTax = s.PersonalIncomeTax,
                NetSalary = s.NetSalary,
                GrossSalary = s.GrossSalary,
                EmployeeId = s.EmployeeId
            });

            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                var sheet = package.Workbook.Worksheets.Add("Salary");
                sheet.Cells.LoadFromCollection(data, true);
                package.Save();
            }
            stream.Position = 0;
            var filename = $"Salary_{DateTime.Now.ToString("yyyyMMdd")}.xlsx";

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
        }


        [Route("ExportSalaryByEmployeeId/{empId}")]
        public async Task<IActionResult> ExportSalaryByEmployeeId(int empId)
        {
            var data = await context.Salaries.Where(s => s.EmployeeId == empId).ToListAsync();

            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                var sheet = package.Workbook.Worksheets.Add("EmployeeSalary");
                sheet.Cells.LoadFromCollection(data, true);
                package.Save();
            }
            stream.Position = 0;
            var filename = $"EmployeeSalary_{DateTime.Now.ToString("yyyyMMdd")}.xlsx";

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
        }

        [Route("ExportSalaryBySalaryId/{salId}")]
        public async Task<IActionResult> ExportSalaryBySalaryId(int salId)
        {
            var data = await context.Salaries.Where(s => s.SalaryId == salId).ToListAsync();

            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                var sheet = package.Workbook.Worksheets.Add("EmployeeSalary");
                sheet.Cells.LoadFromCollection(data, true);
                package.Save();
            }
            stream.Position = 0;
            var filename = $"EmployeeSalary_{DateTime.Now.ToString("yyyyMMdd")}.xlsx";

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
        }

        public async Task<IActionResult> ExportPatternSalary()
        {
            var data = new List<CreateSalary>();
            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                var sheet = package.Workbook.Worksheets.Add("PatternSalary");
                sheet.Cells.LoadFromCollection(data, true);
                package.Save();
            }
            stream.Position = 0;
            var filename = $"PatternSalary_{DateTime.Now.ToString("yyyyMMdd")}.xlsx";

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
        }

        [HttpPost]
        public async Task<ActionResult> ImportSalary(IFormFile file)
        {

            var list = new List<CreateSalary>();

            var stream = new MemoryStream();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            await file.CopyToAsync(stream);
            using (var package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                var rowcount = worksheet.Dimension.Rows;
                for (int row = 2; row <= rowcount; row++)
                {
                    list.Add(new CreateSalary
                    {
                        SalaryDate = (DateTime)(worksheet.Cells[row, 1].Value),
                        LabourContractSalary = Convert.ToInt32(worksheet.Cells[row, 2].Value),
                        MonthsWorkday = Convert.ToInt32(worksheet.Cells[row, 3].Value),
                        TotalWorkday = Convert.ToInt32(worksheet.Cells[row, 4].Value),
                        LunchAllowance = Convert.ToInt32(worksheet.Cells[row, 5].Value),
                        MobilePhoneAllowance = Convert.ToInt32(worksheet.Cells[row, 6].Value),
                        ConveyanceAllowance = Convert.ToInt32(worksheet.Cells[row, 7].Value),
                        PerformanceBonus = Convert.ToInt32(worksheet.Cells[row, 8].Value),
                        EmployeeId = Convert.ToInt32(worksheet.Cells[row, 9].Value),
                    });
                }
            }

            
            foreach (var create in list)
            {
               await salaryService.CreateSalary(create);
            }
            return RedirectToAction("Index");
        }
    }
}
