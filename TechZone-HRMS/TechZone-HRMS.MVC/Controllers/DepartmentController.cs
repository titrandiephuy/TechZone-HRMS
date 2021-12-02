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
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var departments = await service.GetDepartments();
            return View();
        }
    }
}
