using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Domain.Entities;
using Timesheet.Domain.Interfaces;
using Timesheet.Persistence.Repositories;

namespace Timesheet.Application.Services
{
    public class HolidayService : IService<Holiday, int>
    {
        public HolidayRepository _repository { get; set; }

        public HolidayService(HolidayRepository Repository)
        {
            _repository = Repository;
        }

        public IEnumerable<Holiday> GetAll()
        {
            return _repository.GetAll();
        }
    }
}