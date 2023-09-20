using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Application.Mappers;
using Timesheet.Domain.Entities;
using Timesheet.Domain.Entities.Timesheets;
using Timesheet.Domain.Entities.Users;
using Timesheet.Domain.Interfaces;
using Timesheet.Persistence.Repositories;

namespace Timesheet.Application.Services
{
    public class TimesheetService : IReaderService<TimesheetEntity, Guid>, IWriterService<TimesheetEntity, Guid>
    {
        private TimesheetRepository _timesheetRepository;

        private UserRepository _userRepository;

        private HolidayRepository _holidayRepository;

        private IHolidayService _holidayService;

        public TimesheetService(TimesheetRepository TimesheetRepository, UserRepository UserRepository, HolidayRepository HolidayRepository, IHolidayService HolidayService)
        {
            _timesheetRepository = TimesheetRepository;
            _userRepository = UserRepository;
            _holidayRepository = HolidayRepository;
            _holidayService = HolidayService;

        }

        public Guid Add(TimesheetEntity entity)
        {
            IList<Holiday> holidayList = _holidayService.GetByMonth(entity.Year, entity.Month).ToList();

            foreach (var holiday in holidayList)
            {
                entity.OccupationList.Add(
                    new Occupation
                    {
                        Date = holiday.Date,
                        Title = holiday.Name
                    });
            }

            return _timesheetRepository.Add(entity);
        }

        public Guid Delete(Guid id)
        {
            return _timesheetRepository.Delete(id);
        }

        public IEnumerable<TimesheetEntity> GetAll()
        {
            IList<TimesheetEntity> TimesheetList = _timesheetRepository.GetAll().ToList();

            foreach(var Timesheet in TimesheetList)
            {
                Timesheet.User = _userRepository.GetById(Timesheet.User.Id);
                OrderOccupationList(Timesheet);
            }

            return TimesheetList;
        }

        public TimesheetEntity GetById(Guid id)
        {
            TimesheetEntity Timesheet = _timesheetRepository.GetById(id);

            Timesheet.User = _userRepository.GetById(Timesheet.User.Id);

            OrderOccupationList(Timesheet);

            return Timesheet;
        }

        public Guid Update(TimesheetEntity entity)
        {
            TimesheetEntity TimesheetToUpdate = GetById(entity.Id);

            foreach(var occupation in entity.OccupationList)
            {
                TimesheetToUpdate.OccupationList.Add(occupation);
            }

            return _timesheetRepository.Update(TimesheetToUpdate);
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