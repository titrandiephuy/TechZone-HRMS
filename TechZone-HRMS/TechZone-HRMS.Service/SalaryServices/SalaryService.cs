using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class SalaryService : ControllerBase, ISalaryService
    {
        private readonly EmployeesManagementContext context;

        public SalaryService(EmployeesManagementContext context)
        {
            this.context = context;
        }
        public async Task<ActionResult<Result>> CreateSalary(CreateSalary createSalary)
        {
            var result = new Result()
            {
                Success = false,
                Message = "Something went wrong please try again!"
            };
            try
            {
                double basic = 0;
                if (createSalary.MonthsWorkday == createSalary.TotalWorkday)
                {
                    basic = createSalary.LabourContractSalary;
                }
                else if (createSalary.MonthsWorkday != createSalary.TotalWorkday)
                {
                    basic = ((createSalary.LabourContractSalary / createSalary.MonthsWorkday) * createSalary.TotalWorkday);
                }

                var bonus = (createSalary.LunchAllowance + createSalary.MobilePhoneAllowance + createSalary.ConveyanceAllowance + createSalary.PerformanceBonus);

                var social = ((createSalary.LabourContractSalary * 10.5) / 100);

                var gross = (((createSalary.LabourContractSalary / createSalary.MonthsWorkday) * createSalary.TotalWorkday) + (createSalary.LunchAllowance + createSalary.MobilePhoneAllowance + createSalary.ConveyanceAllowance + createSalary.PerformanceBonus));

                var salarytotal = gross - createSalary.LunchAllowance - social;

                double tax = 0;

                if (salarytotal > 80000000)
                {
                    tax = (salarytotal * 35) / 100;
                }
                else if (52000000 < salarytotal && salarytotal <= 80000000)
                {
                    tax = (salarytotal * 30) / 100;
                }
                else if (32000000 < salarytotal && salarytotal <= 52000000)
                {
                    tax = (salarytotal * 25) / 100;
                }
                else if (18000000 < salarytotal && salarytotal <= 32000000)
                {
                    tax = (salarytotal * 20) / 100;
                }
                else if (10000000 < salarytotal && salarytotal <= 18000000)
                {
                    tax = (salarytotal * 15) / 100;
                }
                else if (5000000 < salarytotal && salarytotal <= 10000000)
                {
                    tax = (salarytotal * 10) / 100;
                }
                else if (salarytotal <= 5000000)
                {
                    tax = (salarytotal * 5) / 100;
                }

                var salary = new Salary()
                {
                    SalaryDate = createSalary.SalaryDate,
                    LabourContractSalary = createSalary.LabourContractSalary,
                    MonthsWorkday = createSalary.MonthsWorkday,
                    TotalWorkday = createSalary.TotalWorkday,
                    BasicSalary = basic,
                    LunchAllowance = createSalary.LunchAllowance,
                    MobilePhoneAllowance = createSalary.MobilePhoneAllowance,
                    ConveyanceAllowance = createSalary.ConveyanceAllowance,
                    PerformanceBonus = createSalary.PerformanceBonus,
                    TotalBonus = bonus,
                    SocialInsurance = social,
                    NetSalary = gross - tax - social,
                    PersonalIncomeTax = tax,
                    GrossSalary = gross,
                    EmployeeId = createSalary.EmployeeId
                };

                context.Salaries.Add(salary);
                if (await context.SaveChangesAsync() > 0)
                {
                    result.Success = true;
                    result.Message = "Salary created successfully";
                };
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public async Task<IEnumerable<SalaryDetail>> GetSalary()
        {
            return (await context.Salaries.ToListAsync()).Select(s => new SalaryDetail()
            {
                SalaryId = s.SalaryId,
                SalaryDate = s.SalaryDate,
                LabourContractSalary = s.LabourContractSalary,
                MonthsWorkday = s.MonthsWorkday,
                TotalWorkday = s.TotalWorkday,
                BasicSalary = s.BasicSalary,
                TotalBonus = s.TotalBonus,
                LunchAllowance = s.LunchAllowance,
                MobilePhoneAllowance = s.MobilePhoneAllowance,
                ConveyanceAllowance = s.ConveyanceAllowance,
                PerformanceBonus = s.PerformanceBonus,
                SocialInsurance = s.SocialInsurance,
                PersonalIncomeTax = s.PersonalIncomeTax,
                NetSalary = s.NetSalary,
                GrossSalary = s.GrossSalary,
                EmployeeId = s.EmployeeId
            });
        }

        public async Task<IEnumerable<Salary>> GetSalaryById(int id)
        {
            return await context.Salaries.Where(s => s.EmployeeId == id).ToListAsync();
        }
    }
}
