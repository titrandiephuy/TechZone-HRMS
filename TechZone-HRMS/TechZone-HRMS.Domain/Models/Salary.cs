using System;
using System.Collections.Generic;

#nullable disable

namespace TechZone_HRMS.Domain
{
    public partial class Salary
    {
        public int SalaryId { get; set; }
        public int LabourContractSalary { get; set; }
        public int MonthsWorkday { get; set; }
        public int TotalWorkday { get; set; }
        public double? BasicSalary { get; set; }
        public double? TotalBonus { get; set; }
        public int LunchAllowance { get; set; }
        public int MobilePhoneAllowance { get; set; }
        public int ConveyanceAllowance { get; set; }
        public int PerformanceBonus { get; set; }
        public double? SocialInsurance { get; set; }
        public double? PersonalIncomeTax { get; set; }
        public double? NetSalary { get; set; }
        public double? GrossSalary { get; set; }
        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
