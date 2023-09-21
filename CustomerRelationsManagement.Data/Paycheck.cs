using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerRelationsManagement.Data
{
    public class Paycheck : BaseEntity
    {
        public int EmployeeId { get; set; }
        public int Hours { get; set; }
        public int Salary { get; set; }
    }
}
