using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechZone_HRMS.Domain.Models
{
    public class AppIdentityUser : IdentityUser
    {
        [ForeignKey("Employees")]
        public int EmployeeId { get; set; }
    }
}
