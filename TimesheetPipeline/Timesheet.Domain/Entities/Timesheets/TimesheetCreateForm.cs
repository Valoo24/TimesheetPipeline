using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Domain.Entities.Users;

namespace Timesheet.Domain.Entities.Timesheets
{
    public class TimesheetCreateForm
    {
        public Guid UserId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
    }
}