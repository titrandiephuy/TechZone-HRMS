using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechZone_HRMS.Domain;
using TechZone_HRMS.Domain.Response;
using TechZone_HRMS.Service.SalaryServices;
using TechZone_HRMS.Service.SalaryServices.SalaryModel;

namespace TechZone_HRMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryAPIController : ControllerBase
    {
        private readonly ISalaryService salaryService;

        public SalaryAPIController(ISalaryService salaryService)
        {
            this.salaryService = salaryService;
        }

        [HttpPost]
        public async Task<ActionResult<Result>> CreateSalary(CreateSalary createSalary)
        {
            return await salaryService.CreateSalary(createSalary);
        }

        [HttpGet]
        public async Task<IEnumerable<SalaryDetail>> GetSalary()
        {
            return await salaryService.GetSalary();
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<Salary>> GetSalaryById(int id)
        {
            return await salaryService.GetSalaryById(id);
        }

        [HttpPut]
        public async Task<ActionResult<Result>> EditSalary(EditSalary editSalary)
        {
            return await salaryService.EditSalary(editSalary);
        }
    }
}
