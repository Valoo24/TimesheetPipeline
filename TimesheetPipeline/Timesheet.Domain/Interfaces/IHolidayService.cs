using Timesheet.Domain.Entities;

namespace Timesheet.Domain.Interfaces
{
    public interface IHolidayService : IReaderService<Holiday, int>
    {
        Task <IEnumerable<Holiday>> GetAllAsync(int year);
        Task <Holiday> GetByIdAsync(int year, int id);
        Task <IEnumerable<Holiday>> GetByMonthAsync(int year, int month);
    }
}