using Microsoft.EntityFrameworkCore;
using Timesheet.Domain.Entities;
using Timesheet.Domain.Interfaces;
using Timesheet.Infrastrucutre.DataAccess;

namespace Timesheet.Persistence.Repositories
{
    public class HolidayRepository : IHolidayRepository
    {
        private TimesheetContext _context;

        public HolidayRepository(TimesheetContext Context)
        {
            _context = Context;
        }

        public async Task<IEnumerable<Holiday>> GetAllAsync()
        {
            return await _context.Holidays.ToListAsync();
        }

        public async Task<Holiday> GetByIdAsync(int id)
        {
            CheckIdRange(id);

            Holiday? holiday = await _context.Holidays.FirstOrDefaultAsync(h => h.Id == id);

            if (holiday is null || holiday == default) throw new ArgumentNullException();

            return holiday;
        }

        public async Task InitializeDatabaseAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            IEnumerable<Holiday> HolidayList = new List<Holiday>
            {
                new Holiday
                {
                    //Id = 1,
                    Name = "Nouvel an",
                    Date = new DateTime(2024, 1, 1)
                },
                new Holiday
                {
                    //Id = 2,
                    Name = "Lundi de Pâques",
                },
                new Holiday
                {
                    //Id = 3,
                    Name = "Fête du travail",
                    Date = new DateTime(2023, 5, 1)
                },
                new Holiday
                {
                    //Id = 4,
                    Name = "Ascension"
                },
                new Holiday
                {
                    //Id = 5,
                    Name = "Lundi de Pentecôte"
                },
                new Holiday
                {
                    //Id = 6,
                    Name = "Fête nationale de Belgique",
                    Date = new DateTime(2023, 7, 21)
                },
                new Holiday
                {
                    //Id = 7,
                    Name = "Assomption",
                    Date = new DateTime(2023, 8, 15)
                },
                new Holiday
                {
                    //Id = 8,
                    Name = "Toussaint",
                    Date = new DateTime(2023, 11, 1)
                },
                new Holiday
                {
                    //Id = 9,
                    Name = "Armistice",
                    Date = new DateTime(2023, 11, 11)
                },
                new Holiday
                {
                    //Id = 10,
                    Name = "Noël",
                    Date = new DateTime(2023, 12, 25)
                }
            };

            foreach (var holiay in HolidayList)
            {
                await _context.AddAsync(holiay);
            }

            await _context.SaveChangesAsync();
        }

        private void CheckIdRange(int id)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException("id");

            if (id > 10) throw new ArgumentOutOfRangeException("id");
        }
    }
}