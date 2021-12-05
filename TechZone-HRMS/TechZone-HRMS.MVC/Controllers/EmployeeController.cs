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
using TechZone_HRMS.Service.EmployeeServices.EmployeeModel;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechZone_HRMS.MVC.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeesManagementContext context;

        public EmployeeController(EmployeesManagementContext context)
        {
            this.context = context;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ExportEmployee()
        {
            var data = (await context.Employees.Include(em => em.Department).Include(em => em.EducationLevel).ToListAsync()).Select(e => new EmployeeDetail()
            {
                EmployeeId = e.EmployeeId,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Gender = e.Gender,
                EmployeePhoneNumber = e.EmployeePhoneNumber,
                Email = e.Email,
                EmployeeAddress = e.EmployeeAddress,
                DateOfBirth = e.DateOfBirth,
                PlaceOfOrigin = e.PlaceOfOrigin,
                Ethnicity = e.Ethnicity,
                JoinDate = e.JoinDate,
                EmployeeAvatar = e.EmployeeAvatar,
                DepartmentName = e.Department.DepartmentName,
                Degree = e.EducationLevel.Degree,
                Major = e.EducationLevel.Major,
                EmployeeStatus = e.EmployeeStatus
            }
            );
            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                var sheet = package.Workbook.Worksheets.Add("Employee");
                sheet.Cells.LoadFromCollection(data, true);
                package.Save();
            }
            stream.Position = 0;
            var filename = $"Employee_{DateTime.Now.ToString("yyyyMMdd")}.xlsx";

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
        }
        [HttpPost]
        public async Task<ActionResult> ImportEmployee(IFormFile file)
        {
            
            var list = new List<CreateEmployee>();

            var stream = new MemoryStream();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            await file.CopyToAsync(stream);
            using (var package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                var rowcount = worksheet.Dimension.Rows;
                for (int row = 2; row <= rowcount; row++)
                {
                    list.Add(new CreateEmployee
                    {
                        FirstName = worksheet.Cells[row, 1].Value.ToString().Trim(),
                        LastName = worksheet.Cells[row, 2].Value.ToString().Trim(),
                        Gender = (bool)(worksheet.Cells[row, 3].Value),
                        EmployeePhoneNumber = worksheet.Cells[row, 4].Value.ToString().Trim(),
                        Email = worksheet.Cells[row, 5].Value.ToString().Trim(),
                        EmployeeAddress = worksheet.Cells[row, 6].Value.ToString().Trim(),
                        DateOfBirth = (DateTime)(worksheet.Cells[row, 7].Value),
                        PlaceOfOrigin = worksheet.Cells[row, 8].Value.ToString().Trim(),
                        Ethnicity = worksheet.Cells[row, 9].Value.ToString().Trim(),
                        JoinDate = (DateTime)(worksheet.Cells[row, 10].Value),
                        EmployeeAvatar = worksheet.Cells[row, 11].Value.ToString().Trim(),
                        DepartmentId = Convert.ToInt32(worksheet.Cells[row, 12].Value),
                        EducationLevelId = Convert.ToInt32(worksheet.Cells[row, 13].Value),
                        EmployeeStatus = (bool)(worksheet.Cells[row, 14].Value)
                    });
                }
            }
           
            var result = false;
            foreach (var create in list)
            {
                var employee = new Employee();
                employee.FirstName = create.FirstName;
                employee.LastName = create.LastName;
                employee.Gender = create.Gender;
                employee.EmployeePhoneNumber = create.EmployeePhoneNumber;
                employee.Email = create.Email;
                employee.EmployeeAddress = create.EmployeeAddress;
                employee.DateOfBirth = create.DateOfBirth;
                employee.PlaceOfOrigin = create.PlaceOfOrigin;
                employee.Ethnicity = create.Ethnicity;
                employee.JoinDate = create.JoinDate;
                employee.EmployeeAvatar = create.EmployeeAvatar;
                employee.DepartmentId = create.DepartmentId;
                employee.EducationLevelId = create.EducationLevelId;
                employee.EmployeeStatus = create.EmployeeStatus;
                context.Employees.Add(employee);
                context.SaveChanges();
                if (await context.SaveChangesAsync() > 0)
                {
                    result = true;

                };

            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> ExportPatternEmployee()
        {
            var data = new List<CreateEmployee>();
            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                var sheet = package.Workbook.Worksheets.Add("PatternEmployee");
                sheet.Cells.LoadFromCollection(data, true);
                package.Save();
            }
            stream.Position = 0;
            var filename = $"PatternEmployee_{DateTime.Now.ToString("yyyyMMdd")}.xlsx";

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
        }
    }
}
