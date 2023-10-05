using Timesheet.Domain.Entities;

namespace Timesheet.Domain.Interfaces
{
    public interface IHolidayRepository : IReaderRepository<Holiday, int>
    {
        public void InitializeDatabase();
    }
}