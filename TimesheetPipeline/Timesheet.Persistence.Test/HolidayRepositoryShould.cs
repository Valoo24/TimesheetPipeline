using Microsoft.EntityFrameworkCore;
using Timesheet.Domain.Entities;
using Timesheet.Domain.Interfaces;
using Timesheet.Infrastrucutre.DataAccess;
using Timesheet.Persistence.Repositories;

namespace Timesheet.Persistence.Test
{
    public class HolidayRepositoryShould
    {
        #region Properties
        private IEnumerable<Holiday> _holidays = new List<Holiday>()
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

        private DbContextOptions<TimesheetContext> _options;

        private TimesheetContext _context;

        private IHolidayRepository _repository;
        #endregion

        #region Constructors
        public HolidayRepositoryShould()
        {
            _options = new DbContextOptionsBuilder<TimesheetContext>()
            .UseInMemoryDatabase(databaseName: "TimesheetTestDBForHolidayRepository")
            .Options;

            _context = new TimesheetContext(_options);

            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            foreach (var holiday in _holidays)
            {
                _context.Holidays.Add(holiday);
            }
            _context.SaveChanges();

            _repository = new HolidayRepository(_context);
        }
        #endregion

        #region TestGetMethods
        [Fact]
        public void GetChristmasHolidayEntity()
        {
            //Arrange & Act
            var result = _repository.GetById(10);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(10, result.Id);
            Assert.Equal("Noël", result.Name);
        }

        [Fact]
        public void GetAllChristmasEntities()
        {
            //Arrange & Act
            var result = _repository.GetAll();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(10, result.Count());
            foreach(var holiday in result)
            {
                Assert.Equal(_holidays.FirstOrDefault(h => h.Id == holiday.Id).Id, holiday.Id);
            }
        }

        [Fact]
        public void ThrowAnArgumentOutOfRangeException()
        {
            //Arrange
            Random rnd = new Random();
            int CheckType = rnd.Next(0, 2);
            int idToCheck = 0;

            if (CheckType == 0) idToCheck = rnd.Next(int.MinValue, 1);
            if (CheckType == 1) idToCheck = rnd.Next(11, int.MaxValue);


            //Act & Assert
            try
            {
                var result = _repository.GetById(idToCheck);
            }
            catch (ArgumentOutOfRangeException ex) 
            {
                Assert.NotNull(ex);
                Assert.IsType<ArgumentOutOfRangeException>(ex);
            }
        }
        #endregion
    }
}