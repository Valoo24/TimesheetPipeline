using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Domain.Entities.Timesheets;
using Timesheet.Domain.Interfaces;
using Timesheet.Persistence.Repositories;

namespace Timesheet.Application.Services
{
    public class TimesheetService : IReaderService<TimesheetEntity, Guid>
    {
        private TimesheetRepository _timesheetRepository;

        private UserRepository _userRepository;

        public TimesheetService(TimesheetRepository TimesheetRepository, UserRepository userRepository)
        {
            _timesheetRepository = TimesheetRepository;
            _userRepository = userRepository;

        }

        public IEnumerable<TimesheetEntity> GetAll()
        {
            IList<TimesheetEntity> TimesheetList = _timesheetRepository.GetAll().ToList();

            foreach(var Timesheet in TimesheetList)
            {
                Timesheet.User = _userRepository.GetById(Timesheet.User.Id);
            }

            return TimesheetList;
        }

        public TimesheetEntity GetById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}