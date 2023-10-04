using Microsoft.EntityFrameworkCore;
using Timesheet.Domain.Entities;
using Timesheet.Domain.Entities.Timesheets;
using Timesheet.Domain.Entities.Users;
using Timesheet.Domain.Interfaces;
using Timesheet.Infrastrucutre.DataAccess;
using Timesheet.Persistence.Repositories;

namespace Timesheet.Persistence.Test
{
    public class TimesheetRepositoryShould
    {
        #region Properties
        private IList<User> _testUsers;

        private IList<TimesheetEntity> _testTimesheets;

        private DbContextOptions<TimesheetContext> _options;

        private TimesheetContext _context;

        private ITimesheetRepository _repository;
        #endregion

        #region Constructors
        public TimesheetRepositoryShould()
        {
            //1 = Brice DeNice, 2 = Elon Musk, 3 = Tom Cruise
            _testUsers = new List<User>()
        {
            new User
            {
                Id = Guid.NewGuid(),
                FirstName = "Brice",
                LastName = "DeNice",
                MailAdress = "BriceDeNice@mail.com",
            },
            new User
            {
                Id = Guid.NewGuid(),
                FirstName = "Elon",
                LastName = "Musk",
                MailAdress = "ElonMusk@mail.com",
            },
            new User
            {
                Id = Guid.NewGuid(),
                FirstName = "Tom",
                LastName = "Cruise",
                MailAdress = "TomCruise@mail.com",
            }
        };

            //1 = Brice Jan/2025, 2 = Brice Feb/2025, 3 = Tom Jan/2025, 4 = Elon Mar/2026
            _testTimesheets = new List<TimesheetEntity>()
            {
                new TimesheetEntity
                {
                    Id = Guid.NewGuid(),
                    UserId = _testUsers.FirstOrDefault(tu => tu.FirstName == "Brice").Id,
                    Year = 2025,
                    Month = 1,
                    OccupationList = new List<Occupation>()
                    {
                        new Occupation()
                        {
                            Date = new DateTime(2025,1, 2),
                            Title = "First Day Of Work"
                        },
                        new Occupation()
                        {
                            Date = new DateTime(2025, 1, 3),
                            Title = "Big Meeting"
                        }
                    }
                },
                new TimesheetEntity
                {
                    Id = Guid.NewGuid(),
                    UserId = _testUsers.FirstOrDefault(tu => tu.FirstName == "Brice").Id,
                    Year = 2025,
                    Month = 2,
                    OccupationList = new List<Occupation>()
                    {
                        new Occupation()
                        {
                            Date = new DateTime(2025,2, 2),
                            Title = "After Work Party"
                        },
                        new Occupation()
                        {
                            Date = new DateTime(2025, 2, 7),
                            Title = "Code Review"
                        }
                    }
                },
                new TimesheetEntity
                {
                    Id = Guid.NewGuid(),
                    UserId = _testUsers.FirstOrDefault(tu => tu.FirstName == "Tom").Id,
                    Year = 2025,
                    Month = 1,
                    OccupationList = new List<Occupation>()
                    {
                        new Occupation()
                        {
                            Date = new DateTime(2025,1, 2),
                            Title = "First Day Of Work"
                        },
                        new Occupation()
                        {
                            Date = new DateTime(2025, 1, 6),
                            Title = "Taking Pictures with people"
                        }
                    }
                },
                new TimesheetEntity 
                { 
                    Id = Guid.NewGuid(),
                    UserId = _testUsers.FirstOrDefault(tu => tu.FirstName == "Elon").Id,
                    Year = 2026, 
                    Month = 3,
                    OccupationList = new List<Occupation>()
                    {
                        new Occupation()
                        {
                            Date = new DateTime(2026,3, 2),
                            Title = "Buying Microsoft"
                        },
                        new Occupation()
                        {
                            Date = new DateTime(2026, 3, 6),
                            Title = "Tell people that buying Microsoft is a bad idea"
                        }
                    }
                }
            };

            _options = new DbContextOptionsBuilder<TimesheetContext>()
            .UseInMemoryDatabase(databaseName: "TimesheetTestDBForTimesheetRepository")
            .Options;

            _context = new TimesheetContext(_options);

            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            _context.SaveChanges();

            _repository = new TimesheetRepository(_context);
        }
        #endregion


    }
}