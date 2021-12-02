using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechZone_HRMS.Domain.Response;
using TechZone_HRMS.Service.DepartmentServices;
using TechZone_HRMS.Service.DepartmentServices.DepartmentModel;

namespace TechZone_HRMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentAPIController : ControllerBase
    {
        private readonly IDepartmentService departmentService;

        public DepartmentAPIController(IDepartmentService departmentService)
        {
            this.departmentService = departmentService;
        }

        [HttpGet]
        public async Task<IEnumerable<DepartmentDetail>> GetDepartments()
        {
            return await departmentService.GetDepartments();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentDetail>> GetDepartmentById(int id)
        {
            return await departmentService.GetDepartmentById(id);
        }

        [HttpPost]
        public async Task<ActionResult<Result>> CreateDepartment(CreateDepartment department)
        {
            return await departmentService.CreateDepartment(department);
        }

        [HttpPut]
        public async Task<ActionResult<Result>> EditDepartment(DepartmentDetail editdepartment)
        {
            return await departmentService.EditDepartment(editdepartment);
        }
        [HttpPatch]
        public async Task<IActionResult> ChangeStatusDepartment(int id)
        {
            return await departmentService.ChangeStatusDepartment(id);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Result>> DeleteDepartment (int id)
        {
            return await departmentService.DeleteDepartment(id);
        }

    }
}
