using System;
using System.Collections.Generic;

#nullable disable

namespace TechZone_HRMS.Domain
{
    public partial class InCharge
    {
        public DateTime? EmployeedDate { get; set; }
        public int? EmployeeId { get; set; }
        public int? PositionId { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Position Position { get; set; }
    }
}
