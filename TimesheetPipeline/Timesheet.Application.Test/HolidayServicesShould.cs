using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Xml;
using Timesheet.Application.Services;
using Timesheet.Domain.Entities;
using Timesheet.Domain.Interfaces;
using Timesheet.Infrastrucutre.DataAccess;
using Timesheet.Persistence.Repositories;

namespace Timesheet.Application.Test
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

        private static DbContextOptions<TimesheetContext> _options = new DbContextOptionsBuilder<TimesheetContext>()
            .UseInMemoryDatabase(databaseName: "TimesheetTestDB")
            .Options;

        private static TimesheetContext _context = new TimesheetContext(_options);

        private Mock<HolidayRepository> _mockHolidayRepository = new Mock<HolidayRepository>(_context);

        [Fact]
        public void GetAllHolidaysEntities()
        {
            //Arrange
            _context.Database.EnsureCreated();
            foreach (var holiday in _holidays)
            {
                _context.Holidays.Add(holiday);
            }
            _context.SaveChanges();

            var Service = new HolidayService(_mockHolidayRepository.Object);

            //Act
            var Result = Service.GetAll();

            //Assert
            Assert.Equal(_holidays, Result);
        }
    }
}