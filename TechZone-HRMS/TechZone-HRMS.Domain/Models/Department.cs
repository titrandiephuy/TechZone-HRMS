using System;
using System.Collections.Generic;

#nullable disable

namespace TechZone_HRMS.Domain
{
    public partial class Department
    {
        public Department()
        {
            Employees = new HashSet<Employee>();
        }

        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentPhoneNumber { get; set; }
        public string DepartmentLocation { get; set; }
        public bool DepartmentStatus { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
