using Timesheet.Domain.Entities;
using Timesheet.Domain.Entities.Timesheets;
using Timesheet.Domain.Entities.Users;

namespace Timesheet.Blazor.FakeDB
{
    public class MockDataBaseService
    {
        private static List<Holiday> _holidays = default!;
        private static List<User> _users = default!;
        private static List<TimesheetEntity> _timesheets = default!;

        public static List<Holiday> Holidays 
        { 
            get
            {
                _holidays ??= InitializeMockHolidays();
                return _holidays;
            }
        }

        private static List<Holiday> InitializeMockHolidays() 
        {
            return new List<Holiday>()
            {
            new Holiday
                {
                    Id = 1,
                    Name = "Nouvel an",
                    Date = new DateTime(2024, 1, 1)
                },
                new Holiday
                {
                    Id = 2,
                    Name = "Lundi de Pâques",
                    Date = new DateTime(2024, 4, 1)
                },
                new Holiday
                {
                    Id = 3,
                    Name = "Fête du travail",
                    Date = new DateTime(2024, 5, 1)
                },
                new Holiday
                {
                    Id = 4,
                    Name = "Ascension",
                    Date = new DateTime(2024, 5, 9)
                },
                new Holiday
                {
                    Id = 5,
                    Name = "Lundi de Pentecôte",
                    Date = new DateTime(2024, 5, 20)
                },
                new Holiday
                {
                    Id = 6,
                    Name = "Fête nationale de Belgique",
                    Date = new DateTime(2024, 7, 21)
                },
                new Holiday
                {
                    Id = 7,
                    Name = "Assomption",
                    Date = new DateTime(2024, 8, 15)
                },
                new Holiday
                {
                    Id = 8,
                    Name = "Toussaint",
                    Date = new DateTime(2024, 11, 1)
                },
                new Holiday
                {
                    Id = 9,
                    Name = "Armistice",
                    Date = new DateTime(2024, 11, 11)
                },
                new Holiday
                {
                    Id = 10,
                    Name = "Noël",
                    Date = new DateTime(2024, 12, 25)
                }
            };
        }
    }
}