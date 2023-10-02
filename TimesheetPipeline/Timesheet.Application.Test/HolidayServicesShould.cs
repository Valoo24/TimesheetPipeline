using Microsoft.EntityFrameworkCore;
using Moq;
using Timesheet.Domain.Entities;
using Timesheet.Domain.Exceptions;
using Timesheet.Infrastrucutre.DataAccess;
using Timesheet.Persistence.Repositories;

namespace Timesheet.Application.Services
{
    public class HolidayServiceShould
    {
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

        private Mock<HolidayRepository> _mockHolidayRepository;

        private HolidayService _service;

        public HolidayServiceShould()
        {
            _options = new DbContextOptionsBuilder<TimesheetContext>()
            .UseInMemoryDatabase(databaseName: "TimesheetTestDB")
            .Options;

            _context = new TimesheetContext(_options);

            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            foreach (var holiday in _holidays)
            {
                _context.Holidays.Add(holiday);
            }
            _context.SaveChanges();

            _mockHolidayRepository = new Mock<HolidayRepository>(_context);

            _service = new HolidayService(_mockHolidayRepository.Object);
        }

        [Fact]
        public void GetAllHolidaysEntities()
        {
            //Arrange

            //Act
            var Result = _service.GetAll();

            //Assert
            Assert.Equal(_holidays, Result);
        }

        [Fact]
        public void GetEasterHoliday()
        {
            //Arrange & Act
            var Result = _service.GetById(2);

            //Assert
            Assert.Equal("Lundi de Pâques", Result.Name);
            Assert.Equal(DayOfWeek.Monday, Result.Date.DayOfWeek);
        }

        [Fact]
        public void GetEasterHolidayFrom2024()
        {
            //Arrange & Act
            var Result = _service.GetById(2024, 2);

            //Assert
            Assert.Equal("Lundi de Pâques", Result.Name);
            Assert.Equal(4, Result.Date.Month);
            Assert.Equal(2024, Result.Date.Year);
        }

        [Fact]
        public void GetEasterHolidayFromAnyYear()
        {
            //Arrange
            Random rnd = new Random();
            int Year = rnd.Next(1, 10_000);

            //Act
            var Result = _service.GetById(Year, 2);

            //Assert
            Assert.Equal("Lundi de Pâques", Result.Name);
            Assert.Equal(Year, Result.Date.Year);
        }

        [Fact]
        public void GetThreeHolidaysFromMayIn2024()
        {
            //Arrange & Act
            var Result = _service.GetByMonth(2024, 5);

            //Assert
            Assert.Equal(3, Result.Count());
            foreach (var holiday in Result)
            {
                Assert.Equal(5, holiday.Date.Month);
            }
        }

        [Fact]
        public void ShouldGetAllHolidaysFrom2027()
        {
            //Arrange & Act
            var Result = _service.GetAll(2027);

            //Assert
            Assert.Equal(10, Result.Count());
            foreach (var holiday in Result)
            {
                Assert.Equal(2027, holiday.Date.Year);
            }
        }

        [Fact]
        public void ShouldThrowANotExisitingMonthExceptionFor0OrLower()
        {
            //Arrange
            Random rnd = new Random();
            int Month = rnd.Next(int.MinValue, 1);
            int Year = rnd.Next(1, 10_000);

            //Act
            Exception Exception = Record.Exception(() =>
            {
                _service.GetByMonth(Year, Month);
            });

            //Act & Assert
            Assert.NotNull(Exception);
            Assert.IsType<NonExistingMonthException>(Exception);
        }
    }
}