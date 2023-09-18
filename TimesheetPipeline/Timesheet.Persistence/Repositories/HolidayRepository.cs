using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Domain.Entities;
using Timesheet.Domain.Interfaces;

namespace Timesheet.Persistence.Repositories
{
    public class HolidayRepository : IRepository<Holiday, int>
    {
        public IEnumerable<Holiday> GetAll()
        {
            return new List<Holiday>{
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
                },
                new Holiday
                {
                    Id = 3,
                    Name = "Fête du travail",
                    Date = new DateTime(2023, 5, 1)
                },
                new Holiday
                {
                    Id = 4,
                    Name = "Ascension"
                },
                new Holiday
                {
                    Id = 5,
                    Name = "Lundi de Pentecôte"
                },
                new Holiday
                {
                    Id = 6,
                    Name = "Fête nationale de Belgique",
                    Date = new DateTime(2023, 7, 21)
                },
                new Holiday
                {
                    Id = 7,
                    Name = "Assomption",
                    Date = new DateTime(2023, 8, 15)
                },
                new Holiday
                {
                    Id = 8,
                    Name = "Toussaint",
                    Date = new DateTime(2023, 11, 1)
                },
                new Holiday
                {
                    Id = 9,
                    Name = "Armistice",
                    Date = new DateTime(2023, 11, 11)
                },
                new Holiday
                {
                    Id = 10,
                    Name = "Noël",
                    Date = new DateTime(2023, 12, 25)
                }
            };
        }
    }
}
