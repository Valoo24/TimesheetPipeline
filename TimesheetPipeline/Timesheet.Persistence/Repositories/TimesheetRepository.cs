using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Domain.Entities;
using Timesheet.Domain.Entities.Timesheets;
using Timesheet.Domain.Entities.Users;
using Timesheet.Domain.Interfaces;
using Timesheet.Infrastrucutre.DataAccess;

namespace Timesheet.Persistence.Repositories
{
    public class TimesheetRepository : ITimesheetRepository
    {
        private TimesheetContext _context;

        public TimesheetRepository(TimesheetContext Context)
        {
            _context = Context;
        }


        public Guid Add(TimesheetEntity entity)
        {
            foreach(var Occupation in entity.OccupationList) 
            { 
                _context.Occupations.Add(Occupation);
            }

            _context.Timesheets.Add(entity);
            _context.SaveChanges();

            return entity.Id;
        }

        public Guid Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TimesheetEntity> GetAll()
        {
            List<TimesheetEntity> TimesheetList = _context.Timesheets.ToList();
            foreach(var timesheet in TimesheetList)
            {
                timesheet.OccupationList = _context.Occupations.Where(o => o.TimesheetId == timesheet.Id).ToList();
            }
            return TimesheetList;
        }

        public TimesheetEntity GetById(Guid id)
        {
            return GetAll().FirstOrDefault(t => t.Id == id);
        }

        public Guid Update(TimesheetEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}