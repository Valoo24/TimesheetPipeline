using Timesheet.Application.Mappers;
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

        public Guid Add(TimesheetEntity entity)
        {
            IList<Holiday> holidayList = _holidayService.GetByMonthAsync(entity.Year, entity.Month).ToList();

            foreach (var holiday in holidayList)
            {
                entity.OccupationList.Add(
                    new Occupation
                    {
                        Date = holiday.Date,
                        Title = holiday.Name
                    });
            }

            return _timesheetRepository.AddAsync(entity);
        }

        public Guid Delete(Guid id)
        {
            return _timesheetRepository.DeleteAsync(id);
        }

        public IEnumerable<TimesheetEntity> GetAll()
        {
            IList<TimesheetEntity> TimesheetList = _timesheetRepository.GetAllAsync().ToList();

            foreach (var Timesheet in TimesheetList)
            {
                OrderOccupationList(Timesheet);
            }

            return TimesheetList;
        }

        public IEnumerable<TimesheetEntity> GetAllDTO()
        {
            IList<TimesheetEntity> TimesheetList = new List<TimesheetEntity>();

            foreach(var timesheet in GetAll())
            {
                TimesheetEntity TimesheetDTO = timesheet;
                TimesheetDTO.User = _userRepository.GetByIdAsync(TimesheetDTO.User.Id);
                TimesheetList.Add(TimesheetDTO);
            }

            return TimesheetList;
        }

        public TimesheetEntity GetById(Guid id)
        {
            TimesheetEntity Timesheet = _timesheetRepository.GetByIdAsync(id);

            OrderOccupationList(Timesheet);

            return Timesheet;
        }

        public TimesheetEntity GetDTOById(Guid id)
        {
            return GetAllDTO().FirstOrDefault(t => t.Id == id);
        }

        public Guid Update(TimesheetEntity entity)
        {
            TimesheetEntity TimesheetToUpdate = GetById(entity.Id);

            foreach(var occupation in entity.OccupationList)
            {
                TimesheetToUpdate.OccupationList.Add(occupation);
            }

            return _timesheetRepository.UpdateAsync(TimesheetToUpdate);
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