using Microsoft.EntityFrameworkCore;
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

        public async Task<Guid> AddAsync(TimesheetEntity entity)
        {
            if (entity is null) throw new ArgumentNullException();

            if (entity.UserId == Guid.Empty) throw new ArgumentNullException();

            foreach (var Occupation in entity.OccupationList)
            {
                await _context.Occupations.AddAsync(Occupation);
            }

            await _context.Timesheets.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task<Guid> DeleteAsync(Guid id)
        {
            TimesheetEntity? entityToDelete = await _context.Timesheets.FirstOrDefaultAsync(t => t.Id == id);

            if(entityToDelete is null || entityToDelete == default) throw new ArgumentNullException();

            _context.Remove(entityToDelete);
            await _context.SaveChangesAsync();

            return id;
        }

        public async Task<IEnumerable<TimesheetEntity>> GetAllAsync()
        {
            IList<TimesheetEntity> TimesheetList = await _context.Timesheets.ToListAsync();

            foreach (var timesheet in TimesheetList)
            {
                timesheet.OccupationList = await _context.Occupations.Where(o => o.TimesheetId == timesheet.Id).ToListAsync();
            }

            return TimesheetList;
        }

        public async Task<TimesheetEntity> GetByIdAsync(Guid id)
        {
            TimesheetEntity? timesheet = await _context.Timesheets.FirstOrDefaultAsync(t => t.Id == id);

            if(timesheet is null || timesheet == default) throw new ArgumentNullException();

            return timesheet;
        }

        public async Task<Guid> UpdateAsync(TimesheetEntity entity)
        {
            TimesheetEntity? entityToUpdate = await _context.Timesheets.FirstOrDefaultAsync(t => t.Id == entity.Id);

            if(entityToUpdate is null || entityToUpdate == default) throw new ArgumentNullException();

            entityToUpdate.UserId = entity.UserId;
            entityToUpdate.Year = entity.Year;
            entityToUpdate.Month = entity.Month;
            entityToUpdate.OccupationList = entity.OccupationList;

            return entityToUpdate.Id;
        }
    }
}