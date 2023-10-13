using Timesheet.Domain.Entities.Timesheets;
using Timesheet.Domain.Entities.Users;

namespace Timesheet.Domain.Interfaces
{
    public interface ITimesheetRepository : IReaderRepository<TimesheetEntity, Guid>, IWriterRepository<TimesheetEntity, Guid>
    {
    }
}