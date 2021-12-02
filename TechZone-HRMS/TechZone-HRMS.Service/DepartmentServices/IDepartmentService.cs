using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechZone_HRMS.Domain.Response;
using TechZone_HRMS.Service.DepartmentServices.DepartmentModel;

namespace TechZone_HRMS.Service.DepartmentServices
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDetail>> GetDepartments();
        Task<ActionResult<DepartmentDetail>> GetDepartmentById(int id);
        Task<ActionResult<Result>> EditDepartment(DepartmentDetail editdepartment);
        Task<ActionResult<Result>> CreateDepartment(CreateDepartment create);
        Task<IActionResult> ChangeStatusDepartment(int id);
        Task<ActionResult<Result>> DeleteDepartment(int id);
     }
}
