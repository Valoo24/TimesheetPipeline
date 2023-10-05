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

            foreach (var timesheet in _testTimesheets)
            {
                _context.Timesheets.Add(timesheet);
            }

            _context.SaveChanges();

            _repository = new TimesheetRepository(_context);
        }
        #endregion

        #region TestAddMethods
        [Fact]
        public void AddANewTimesheetForElonMusk()
        {
            //Arrange
            TimesheetEntity timesheetToAdd = new TimesheetEntity()
            {
                Id = Guid.NewGuid(),
                UserId = _testUsers.FirstOrDefault(tu => tu.FirstName == "Elon").Id,
                Year = 2026,
                Month = 12,
                OccupationList = new List<Occupation>() 
                { 
                    new Occupation
                    {
                        Date = new DateTime(2026, 12, 1),
                        Title = "Thinking about buying the world"
                    },
                    new Occupation
                    {
                        Date = new DateTime(2026, 12, 2),
                        Title = "Buying Jeff Bezos"
                    }
                }
            };

            //Act
            var result = _repository.Add(timesheetToAdd);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(timesheetToAdd.Id, result);
        }

        [Fact]
        public void AddANewTimesheetForTomCruiseWithEmptyOccupationList()
        {
            //Arrange
            TimesheetEntity timesheetToAdd = new TimesheetEntity()
            {
                Id = Guid.NewGuid(),
                UserId = _testUsers.FirstOrDefault(tu => tu.FirstName == "Tom").Id,
                Year = 2026,
                Month = 12,
                OccupationList = new List<Occupation>()
            };

            //Act
            var result = _repository.Add(timesheetToAdd);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(timesheetToAdd.Id, result);
        }

        [Fact]
        public void AddANewTimesheetForBriceDeNiceWithNoOccupationList()
        {
            //Arrange
            TimesheetEntity timesheetToAdd = new TimesheetEntity()
            {
                Id = Guid.NewGuid(),
                UserId = _testUsers.FirstOrDefault(tu => tu.FirstName == "Brice").Id,
                Year = 2026,
                Month = 6
            };

            //Act
            var result = _repository.Add(timesheetToAdd);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(timesheetToAdd.Id, result);
            Assert.False(timesheetToAdd.OccupationList is null);
        }

        [Fact]
        public void ThrowArgumentNullExceptionIfAddingANullTimesheet()
        {
            try
            {
                var result = _repository.Add(null);
            }
            catch(ArgumentNullException ex)
            {
                Assert.NotNull(ex);
                Assert.IsType<ArgumentNullException>(ex);
            }
        }

        [Fact]
        public void ThrowArgumentNullExceptionIfTimesheetEntityHasAnEmptyUserId()
        {
            //Arrange
            TimesheetEntity timesheetToAdd = new TimesheetEntity()
            {
                Id = Guid.NewGuid(),
                UserId = Guid.Empty,
                Year = 2026,
                Month = 6
            };

            //Act
            try
            {
                var result = _repository.Add(timesheetToAdd);
            }
            catch(ArgumentNullException ex)
            {
                Assert.NotNull(ex);
                Assert.IsType<ArgumentNullException>(ex);
            }
        }

        [Fact]
        public void GetAllTimesheetsEntity()
        {
            var result = _repository.GetAll();

            Assert.NotNull(result);
            Assert.Equal(4, result.Count());
            foreach(var timesheet in result)
            {
                Assert.NotNull(timesheet.OccupationList);
                Assert.Equal(2, timesheet.OccupationList.Count);
            }
        }

        [Fact]
        public void GetTomCruiseTimesheetEntityById()
        {
            User tomCruise = _testUsers.FirstOrDefault(u => u.FirstName == "Tom");
            Guid timesheetIdToFind = _testTimesheets.FirstOrDefault(t => 
            t.UserId == tomCruise.Id).Id;

            var result = _repository.GetById(timesheetIdToFind);

            Assert.NotNull(result);
            Assert.Equal(tomCruise.Id, result.UserId);
            Assert.Equal(1, result.Month);
            Assert.Equal(2, result.OccupationList.Count);
            Assert.Equal("Taking Pictures with people", result.OccupationList.FirstOrDefault(o => o.Date == new DateTime(2025, 1, 6)).Title);
        }

        [Fact]
        public void UpdateJanuaryBriceDeNiceTimesheetForATomCruiseTimesheet()
        {
            User briceDeNice = _testUsers.FirstOrDefault(u => u.FirstName == "Brice");
            User tomCruise = _testUsers.FirstOrDefault(u => u.FirstName == "Tom");

            TimesheetEntity timesheetToUpdate = _repository.GetById(
                _testTimesheets.FirstOrDefault(
                    t => t.UserId == briceDeNice.Id 
                    && t.Month == 1).Id);

            Occupation occupationToRemove = timesheetToUpdate.OccupationList.FirstOrDefault(o => o.Title == "Big Meeting");

            timesheetToUpdate.UserId = tomCruise.Id;
            timesheetToUpdate.OccupationList.Remove(occupationToRemove);
            timesheetToUpdate.OccupationList.Add(new Occupation
            {
                Date = new DateTime(2025, 1, 7),
                Title = "First Update"
            });
            timesheetToUpdate.OccupationList.Add(new Occupation
            {
                Date = new DateTime(2025, 1, 8),
                Title = "Second Update"
            });

            var result = _repository.Update(timesheetToUpdate);

            Assert.NotNull(result);
            Assert.Equal(timesheetToUpdate.Id, result);
            Assert.Equal(timesheetToUpdate, _repository.GetById(timesheetToUpdate.Id));
        }

        [Fact]
        public void DeleteElonMuskTimesheetEntity()
        {
            User elonMusk = _testUsers.FirstOrDefault(u => u.FirstName == "Elon");
            Guid timesheetIdToDelete = _testTimesheets.FirstOrDefault(t => t.UserId == elonMusk.Id).Id;

            var result = _repository.Delete(timesheetIdToDelete);
            var resultAllTimesheet = _repository.GetAll();


            Assert.NotNull(result);
            Assert.Equal(3, resultAllTimesheet.Count());
            Assert.Equal(default, resultAllTimesheet.FirstOrDefault(t => t.UserId == elonMusk.Id));
        }
        #endregion
    }
}