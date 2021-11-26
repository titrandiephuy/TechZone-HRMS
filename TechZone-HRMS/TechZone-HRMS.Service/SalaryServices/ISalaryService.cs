using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechZone_HRMS.Domain;
using TechZone_HRMS.Domain.Response;
using TechZone_HRMS.Service.SalaryServices.SalaryModel;

namespace TechZone_HRMS.Service.SalaryServices
{
    public interface ISalaryService
    {
        Task<ActionResult<Result>> CreateSalary(CreateSalary createSalary);
        Task<IEnumerable<SalaryDetail>> GetSalary();
        Task<IEnumerable<Salary>> GetSalaryById(int id);
        Task<ActionResult<Result>> EditSalary(EditSalary editSalary);
    }
}
