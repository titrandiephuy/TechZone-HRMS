using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechZone_HRMS.Service.DepartmentServices.DepartmentModel
{
    public class CreateDepartment
    {
        [Required]
        [StringLength(100)]
        public string DepartmentName { get; set; }
        [Required]
        [StringLength(50)]
        public string DepartmentPhoneNumber { get; set; }
        [Required]
        [StringLength(100)]
        public string DepartmentLocation { get; set; }
        [Required]
        public bool DepartmentStatus { get; set; }
    }
}
