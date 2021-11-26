using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechZone_HRMS.Service.SalaryServices.SalaryModel
{
    public class EditSalary
    {
        public int SalaryId { get; set; }
        public DateTime SalaryDate { get; set; }
        public int LabourContractSalary { get; set; }
        public int MonthsWorkday { get; set; }
        public int TotalWorkday { get; set; }
        public int LunchAllowance { get; set; }
        public int MobilePhoneAllowance { get; set; }
        public int ConveyanceAllowance { get; set; }
        public int PerformanceBonus { get; set; }
       
    }
}
