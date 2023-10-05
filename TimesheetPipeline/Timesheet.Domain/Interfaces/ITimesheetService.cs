using Timesheet.Domain.Entities.Timesheets;

namespace Timesheet.Domain.Interfaces
{
    public interface ITimesheetService : IReaderService<TimesheetEntity, Guid>, IWriterService<TimesheetEntity, Guid>
    {
        public IEnumerable<TimesheetDTO> GetAllDTO();
        public TimesheetDTO GetDTOById(Guid id);
    }
}
