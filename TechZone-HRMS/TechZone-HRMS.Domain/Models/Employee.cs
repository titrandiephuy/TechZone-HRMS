using System;
using System.Collections.Generic;

#nullable disable

namespace TechZone_HRMS.Domain
{
    public partial class Employee
    {
        public Employee()
        {
            Leaves = new HashSet<Leaves>();
            Salaries = new HashSet<Salary>();
        }

        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Gender { get; set; }
        public string EmployeePhoneNumber { get; set; }
        public string Email { get; set; }
        public string EmployeeAddress { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PlaceOfOrigin { get; set; }
        public string Ethnicity { get; set; }
        public DateTime JoinDate { get; set; }
        public string EmployeeAvatar { get; set; }
        public int DepartmentId { get; set; }
        public int EducationLevelId { get; set; }
        public bool EmployeeStatus { get; set; }

        public virtual Department Department { get; set; }
        public virtual EducationLevel EducationLevel { get; set; }
        public virtual ICollection<Leaves> Leaves { get; set; }
        public virtual ICollection<Salary> Salaries { get; set; }
    }
}
