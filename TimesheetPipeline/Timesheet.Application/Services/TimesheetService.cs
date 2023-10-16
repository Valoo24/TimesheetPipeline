using Timesheet.Domain.Entities;
using Timesheet.Domain.Entities.Timesheets;
using Timesheet.Domain.Interfaces;

namespace Timesheet.Application.Services
{
    public class TimesheetService : ITimesheetService
    {
        private ITimesheetRepository _timesheetRepository;

        private IUserRepository _userRepository;

        private IHolidayRepository _holidayRepository;

        private IHolidayService _holidayService;

        public TimesheetService(ITimesheetRepository TimesheetRepository, IUserRepository UserRepository, IHolidayRepository HolidayRepository, IHolidayService HolidayService)
        {
            _timesheetRepository = TimesheetRepository;
            _userRepository = UserRepository;
            _holidayRepository = HolidayRepository;
            _holidayService = HolidayService;

        }

        public async Task<Guid> AddAsync(TimesheetEntity entity)
        {
            IEnumerable<Holiday> holidayList = await _holidayService.GetByMonthAsync(entity.Year, entity.Month);

            foreach (var holiday in holidayList)
            {
                entity.OccupationList.Add(
                    new Occupation
                    {
                        Date = holiday.Date,
                        Title = holiday.Name
                    });
            }

            return await _timesheetRepository.AddAsync(entity);
        }

        public async Task<Guid> DeleteAsync(Guid id)
        {
            return await _timesheetRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<TimesheetEntity>> GetAllAsync()
        {
            IEnumerable<TimesheetEntity> TimesheetList = await _timesheetRepository.GetAllAsync();

            foreach (var Timesheet in TimesheetList)
            {
                OrderOccupationList(Timesheet);
            }

            return TimesheetList;
        }

        public async Task<TimesheetEntity> GetByIdAsync(Guid id)
        {
            TimesheetEntity Timesheet = await _timesheetRepository.GetByIdAsync(id);

            OrderOccupationList(Timesheet);

            return Timesheet;
        }

        public async Task<Guid> UpdateAsync(TimesheetEntity entity)
        {
            TimesheetEntity TimesheetToUpdate = await GetByIdAsync(entity.Id);

            foreach(var occupation in entity.OccupationList)
            {
                TimesheetToUpdate.OccupationList.Add(occupation);
            }

            return await _timesheetRepository.UpdateAsync(TimesheetToUpdate);
        }

        public async Task InitializeDatabaseAsync()
        {
            await _holidayRepository.InitializeDatabaseAsync();
            await _userRepository.InitializeDatabaseAsync();
        }

        private void OrderOccupationList(TimesheetEntity timesheet)
        {
            IEnumerable<Occupation> OrderedOccupations = timesheet.OccupationList.OrderBy(o => o.Date);
            timesheet.OccupationList = new List<Occupation>();

            foreach (var occupation in OrderedOccupations)
            {
                timesheet.OccupationList.Add(occupation);
            }
        }
    }
}