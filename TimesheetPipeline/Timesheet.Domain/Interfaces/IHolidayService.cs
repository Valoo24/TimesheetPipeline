using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Domain.Entities;

namespace Timesheet.Domain.Interfaces
{
    public interface IHolidayService : IReaderService<Holiday, int>
    {
        public IEnumerable<Holiday> GetAll(int year);
        public Holiday GetById(int year, int id);
        public IEnumerable<Holiday> GetByMonth(int year, int month);
    }
}