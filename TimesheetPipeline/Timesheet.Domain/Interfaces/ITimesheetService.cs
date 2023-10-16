using Timesheet.Domain.Entities.Timesheets;

namespace Timesheet.Domain.Interfaces
{
    public interface ITimesheetService : IReaderService<TimesheetEntity, Guid>, IWriterService<TimesheetEntity, Guid>
    {
        public IEnumerable<TimesheetEntity> GetAllDTO();
        public TimesheetEntity GetDTOById(Guid id);
        Task InitializeDatabaseAsync();
    }
}