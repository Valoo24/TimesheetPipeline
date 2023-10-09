using Timesheet.Domain.Entities;
using Timesheet.Domain.Entities.Timesheets;
using Timesheet.Domain.Entities.Users;

namespace Timesheet.Blazor.FakeDB
{
    public class MockDataBaseService
    {
        private static List<Holiday> _holidays = default!;
        private static List<User> _users = default!;
        private static List<TimesheetDTO> _timesheets = default!;

        public static List<Holiday> Holidays
        {
            get
            {
                _holidays ??= InitializeMockHolidays();
                return _holidays;
            }
        }
        public static List<User> Users
        {
            get
            {
                _users ??= InitializeMockUsers();
                return _users;
            }
        }
        public static List<TimesheetDTO> Timesheets
        {
            get 
            { 
                _timesheets ??= InitializeMockTimesheets();
                return _timesheets;
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
        private static List<User> InitializeMockUsers()
        {
            return new List<User>
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
        }
        private static List<TimesheetDTO> InitializeMockTimesheets()
        {
            return new List<TimesheetDTO>()
            {
                new TimesheetDTO
                {
                    Id = Guid.NewGuid(),
                    User = Users.FirstOrDefault(u => u.FirstName == "Brice"),
                    Year = 2024,
                    Month = 1,
                    OccupationList = new List<Occupation> 
                    {
                        new Occupation 
                        {
                            Title = Holidays.FirstOrDefault(h => h.Date.Month == 1).Name,
                            Date = Holidays.FirstOrDefault(h => h.Date.Month == 1).Date
                        },
                        new Occupation 
                        { 
                            Title = "Premier jour de travail",
                            Date = new DateTime(2024, 1, 2)
                        },
                        new Occupation 
                        {
                            Title = "Première Réunion",
                            Date = new DateTime(2024, 1, 3),
                        }
                    }
                },
                new TimesheetDTO 
                { 
                    Id = Guid.NewGuid(),
                    User = Users.FirstOrDefault(u => u.FirstName == "Brice"),
                    Year = 2024,
                    Month = 4,
                    OccupationList = new List<Occupation> 
                    {
                        new Occupation
                        {
                            Title = Holidays.FirstOrDefault(h => h.Date.Month == 4).Name,
                            Date = Holidays.FirstOrDefault(h => h.Date.Month == 4).Date
                        }
                    }
                },
                new TimesheetDTO 
                { 
                    Id = Guid.NewGuid(),
                    User = Users.FirstOrDefault(u => u.FirstName == "Brice"),
                    Year = 2024,
                    Month = 5,
                    OccupationList = new List<Occupation> 
                    { 
                        new Occupation
                        {
                            Title = Holidays.FirstOrDefault(h => h.Id == 3).Name,
                            Date = Holidays.FirstOrDefault(h => h.Id == 3).Date
                        },
                        new Occupation
                        {
                            Title = Holidays.FirstOrDefault(h => h.Id == 4).Name,
                            Date = Holidays.FirstOrDefault(h => h.Id == 4).Date
                        },
                        new Occupation
                        {
                            Title = Holidays.FirstOrDefault(h => h.Id == 5).Name,
                            Date = Holidays.FirstOrDefault(h => h.Id == 5).Date
                        }
                    }
                },
                new TimesheetDTO
                {
                    Id = Guid.NewGuid(),
                    User = Users.FirstOrDefault(u => u.FirstName == "Tom"),
                    Year = 2024,
                    Month = 1,
                    OccupationList = new List<Occupation>
                    {
                        new Occupation
                        {
                            Title = Holidays.FirstOrDefault(h => h.Date.Month == 1).Name,
                            Date = Holidays.FirstOrDefault(h => h.Date.Month == 1).Date
                        }
                    }
                },
                new TimesheetDTO
                {
                    Id = Guid.NewGuid(),
                    User = Users.FirstOrDefault(u => u.FirstName == "Tom"),
                    Year = 2024,
                    Month = 4,
                    OccupationList = new List<Occupation>
                    {
                        new Occupation
                        {
                            Title = Holidays.FirstOrDefault(h => h.Date.Month == 4).Name,
                            Date = Holidays.FirstOrDefault(h => h.Date.Month == 4).Date
                        }
                    }
                },
                new TimesheetDTO
                {
                    Id = Guid.NewGuid(),
                    User = Users.FirstOrDefault(u => u.FirstName == "Tom"),
                    Year = 2024,
                    Month = 5,
                    OccupationList = new List<Occupation>
                    {
                        new Occupation
                        {
                            Title = Holidays.FirstOrDefault(h => h.Id == 3).Name,
                            Date = Holidays.FirstOrDefault(h => h.Id == 3).Date
                        },
                        new Occupation
                        {
                            Title = Holidays.FirstOrDefault(h => h.Id == 4).Name,
                            Date = Holidays.FirstOrDefault(h => h.Id == 4).Date
                        },
                        new Occupation
                        {
                            Title = Holidays.FirstOrDefault(h => h.Id == 5).Name,
                            Date = Holidays.FirstOrDefault(h => h.Id == 5).Date
                        }
                    }
                },
                                new TimesheetDTO
                {
                    Id = Guid.NewGuid(),
                    User = Users.FirstOrDefault(u => u.FirstName == "Elon"),
                    Year = 2024,
                    Month = 1,
                    OccupationList = new List<Occupation>
                    {
                        new Occupation
                        {
                            Title = Holidays.FirstOrDefault(h => h.Date.Month == 1).Name,
                            Date = Holidays.FirstOrDefault(h => h.Date.Month == 1).Date
                        },
                        new Occupation
                        {
                            Title = "Tweeter des billevesées",
                            Date = new DateTime(2024, 1, 2)
                        },
                        new Occupation
                        {
                            Title = "Tweeter des pécadilles",
                            Date = new DateTime(2024, 1, 3),
                        },
                        new Occupation
                        {
                            Title = "Tweeter des sornettes",
                            Date = new DateTime(2024, 1, 4),
                        },
                        new Occupation
                        {
                            Title = "Tweeter des galéjades",
                            Date = new DateTime(2024, 1, 5),
                        },
                    }
                },
                new TimesheetDTO
                {
                    Id = Guid.NewGuid(),
                    User = Users.FirstOrDefault(u => u.FirstName == "Elon"),
                    Year = 2024,
                    Month = 4,
                    OccupationList = new List<Occupation>
                    {
                        new Occupation
                        {
                            Title = Holidays.FirstOrDefault(h => h.Date.Month == 4).Name,
                            Date = Holidays.FirstOrDefault(h => h.Date.Month == 4).Date
                        }
                    }
                },
                new TimesheetDTO
                {
                    Id = Guid.NewGuid(),
                    User = Users.FirstOrDefault(u => u.FirstName == "Elon"),
                    Year = 2024,
                    Month = 5,
                    OccupationList = new List<Occupation>
                    {
                        new Occupation
                        {
                            Title = Holidays.FirstOrDefault(h => h.Id == 3).Name,
                            Date = Holidays.FirstOrDefault(h => h.Id == 3).Date
                        },
                        new Occupation
                        {
                            Title = Holidays.FirstOrDefault(h => h.Id == 4).Name,
                            Date = Holidays.FirstOrDefault(h => h.Id == 4).Date
                        },
                        new Occupation
                        {
                            Title = Holidays.FirstOrDefault(h => h.Id == 5).Name,
                            Date = Holidays.FirstOrDefault(h => h.Id == 5).Date
                        }
                    }
                },
            };
        }
    }
}