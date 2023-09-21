using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerRelationsManagement.Data
{
    public class Shift : BaseEntity
    {
        public int EmployeeId { get; set; }
        public int TimeOfShift { get; set; }
    }
}
