using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Domain.Entities.Timesheets;

namespace Timesheet.Domain.Interfaces
{
    public interface ITimesheetRepository : IReaderRepository<TimesheetEntity, Guid>, IWriterRepository<TimesheetEntity, Guid>
    {
    }
}