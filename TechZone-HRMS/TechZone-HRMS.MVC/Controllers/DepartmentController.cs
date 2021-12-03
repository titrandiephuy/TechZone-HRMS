using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechZone_HRMS.Service.DepartmentServices;

namespace TechZone_HRMS.MVC.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService service;

        public DepartmentController(IDepartmentService service)
        {
            this.service = service;
        }
        [HttpGet]
        [Route("/Department/Get")]
        public async Task<IActionResult> Get()
        {
            var data = await service.GetDepartments();
            return Ok(data);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
