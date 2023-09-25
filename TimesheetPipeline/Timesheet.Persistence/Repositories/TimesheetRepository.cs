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
            IList<TimesheetEntity> timesheetList = GetAll().ToList();

            timesheetList.Remove(timesheetList.FirstOrDefault(t => t.Id == id));

            foreach(var timesheet in timesheetList) 
            { 
                Add(timesheet);
            }

            return id;
        }

        public IEnumerable<TimesheetEntity> GetAll()
        {
            return _context.Timesheets.Include(t => t.OccupationList).ToList();
        }

        public TimesheetEntity GetById(Guid id)
        {
            return _context.Timesheets.FirstOrDefault(t => t.Id == id);
        }

        public Guid Update(TimesheetEntity entity)
        {
            IList<TimesheetEntity> TimesheetList = GetAll().ToList();

            int index = TimesheetList.IndexOf(TimesheetList.FirstOrDefault(t => t.Id == entity.Id));

            TimesheetList.Insert(index, entity);

            TimesheetList.RemoveAt(index + 1);

            foreach (var timesheet in TimesheetList)
            {
                Add(timesheet);
            }

            return entity.Id;
        }
    }
}