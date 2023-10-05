using Timesheet.Domain.Entities.Timesheets;

namespace Timesheet.Domain.Interfaces
{
    public interface ITimesheetRepository : IReaderRepository<TimesheetEntity, Guid>, IWriterRepository<TimesheetEntity, Guid>
    {
    }
}