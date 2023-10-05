using Timesheet.Domain.Entities.Timesheets;
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
            if (entity is null) throw new ArgumentNullException();

            if (entity.UserId == Guid.Empty) throw new ArgumentNullException();

            foreach (var Occupation in entity.OccupationList)
            {
                _context.Occupations.Add(Occupation);
            }

            _context.Timesheets.Add(entity);
            _context.SaveChanges();

            return entity.Id;
        }

        public Guid Delete(Guid id)
        {
            TimesheetEntity entityToDelete = _context.Timesheets.FirstOrDefault(t => t.Id == id);

            if(entityToDelete is null || entityToDelete == default) throw new ArgumentNullException();

            _context.Remove(entityToDelete);
            _context.SaveChanges();

            return id;
        }

        public IEnumerable<TimesheetEntity> GetAll()
        {
            List<TimesheetEntity> TimesheetList = _context.Timesheets.ToList();
            foreach (var timesheet in TimesheetList)
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
            TimesheetEntity entityToUpdate = _context.Timesheets.FirstOrDefault(t => t.Id == entity.Id);

            entityToUpdate.UserId = entity.UserId;
            entityToUpdate.Year = entity.Year;
            entityToUpdate.Month = entity.Month;
            entityToUpdate.OccupationList = entity.OccupationList;

            return entityToUpdate.Id;
        }
    }
}