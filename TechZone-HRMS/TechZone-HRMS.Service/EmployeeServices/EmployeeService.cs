using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechZone_HRMS.Domain;
using TechZone_HRMS.Domain.Response;
using TechZone_HRMS.Service.EmployeeServices.EmployeeModel;

namespace TechZone_HRMS.Service.EmployeeServices
{
    public class EmployeeService : ControllerBase, IEmployeeService
    {
        private readonly EmployeesManagementContext context;

        public EmployeeService(EmployeesManagementContext context)
        {
            this.context = context;
        }

        public async Task<ActionResult<Result>> CreateEmployee(CreateEmployee createEmployee)
        {
            var result = new Result()
            {
                Success = false,
                Message = "Something went wrong please try again!"
            };
            try
            {
                var employee = new Employee()
                {
                    FirstName = createEmployee.FirstName,
                    LastName = createEmployee.LastName,
                    Gender = createEmployee.Gender,
                    EmployeePhoneNumber = createEmployee.EmployeePhoneNumber,
                    Email = createEmployee.Email,
                    EmployeeAddress = createEmployee.EmployeeAddress,
                    DateOfBirth = createEmployee.DateOfBirth,
                    PlaceOfOrigin = createEmployee.PlaceOfOrigin,
                    Ethnicity = createEmployee.Ethnicity,
                    JoinDate = createEmployee.JoinDate,
                    EmployeeAvatar = createEmployee.EmployeeAvatar,
                    DepartmentId = createEmployee.DepartmentId,
                    EducationLevelId = createEmployee.EducationLevelId,
                    EmployeeStatus = createEmployee.EmployeeStatus
                };

                context.Employees.Add(employee);
                if (await context.SaveChangesAsync() > 0)
                {
                    result.Success = true;
                    result.Message = "Employee created successfully";
                };
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            employee.EmployeeStatus = !employee.EmployeeStatus;
            context.Entry(employee).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        public async Task<ActionResult<Result>> EditEmployee(EditEmployee editEmployee)
        {
            var result = new Result()
            {
                Success = false,
                Message = "Something went wrong please try again!"
            };
            try
            {
                var employee = await context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == editEmployee.EmployeeId);
                employee.EmployeeId = editEmployee.EmployeeId;
                employee.FirstName = editEmployee.FirstName;
                employee.LastName = editEmployee.LastName;
                employee.Gender = editEmployee.Gender;
                employee.EmployeePhoneNumber = editEmployee.EmployeePhoneNumber;
                employee.Email = editEmployee.Email;
                employee.EmployeeAddress = editEmployee.EmployeeAddress;
                employee.DateOfBirth = editEmployee.DateOfBirth;
                employee.PlaceOfOrigin = editEmployee.PlaceOfOrigin;
                employee.Ethnicity = editEmployee.Ethnicity;
                employee.JoinDate = editEmployee.JoinDate;
                employee.EmployeeAvatar = editEmployee.EmployeeAvatar;
                employee.DepartmentId = editEmployee.DepartmentId;
                employee.EducationLevelId = editEmployee.EducationLevelId;
                employee.EmployeeStatus = editEmployee.EmployeeStatus;

                context.Entry(employee).State = EntityState.Modified;
                if (await context.SaveChangesAsync() > 0)
                {
                    result.Success = true;
                    result.Message = "Employee edited successfully";
                }
                return result;
            }
            catch (Exception ex)
            {

                return result;
            }
        }

        public async Task<ActionResult<EmployeeDetail>> GetEmployeeById(int id)
        {
            var employee = await context.Employees.Include(em => em.Department).Include(em => em.EducationLevel).FirstOrDefaultAsync(e => e.EmployeeId == id);

            if (employee == null)
            {
                return NotFound();
            }

            var employeeDetail = new EmployeeDetail();
            employeeDetail.EmployeeId = employee.EmployeeId;
            employeeDetail.FirstName = employee.FirstName;
            employeeDetail.LastName = employee.LastName;
            employeeDetail.Gender = employee.Gender;
            employeeDetail.EmployeePhoneNumber = employee.EmployeePhoneNumber;
            employeeDetail.Email = employee.Email;
            employeeDetail.EmployeeAddress = employee.EmployeeAddress;
            employeeDetail.DateOfBirth = employee.DateOfBirth;
            employeeDetail.PlaceOfOrigin = employee.PlaceOfOrigin;
            employeeDetail.Ethnicity = employee.Ethnicity;
            employeeDetail.JoinDate = employee.JoinDate;
            employeeDetail.EmployeeAvatar = employee.EmployeeAvatar;
            employeeDetail.DepartmentName = employee.Department.DepartmentName;
            employeeDetail.Degree = employee.EducationLevel.Degree;
            employeeDetail.Major = employee.EducationLevel.Major;
            employeeDetail.EmployeeStatus = employee.EmployeeStatus;

            return employeeDetail;
        }

        public async Task<IEnumerable<EmployeeDetail>> GetEmployees()
        {
            return (await context.Employees.Include(em => em.Department).Include(em => em.EducationLevel).ToListAsync()).Select(e => new EmployeeDetail()
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
        }
    }
}
