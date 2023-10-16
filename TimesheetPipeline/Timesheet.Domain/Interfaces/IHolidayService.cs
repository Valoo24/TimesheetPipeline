using Timesheet.Domain.Entities;

namespace Timesheet.Domain.Interfaces
{
    public interface IHolidayService : IReaderService<Holiday, int>
    {
        Task <IEnumerable<Holiday>> GetByMonthAsync(int year, int month);
        void ChangeDate(Holiday holiday, int year);
    }
}