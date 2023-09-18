using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Domain.Entities;
using Timesheet.Domain.Interfaces;

namespace Timesheet.Persistence.Repositories
{
    public class HolidayRepository : IReaderRepository<Holiday, int>
    {
        private string _csvFilePath
        {
            get
            {
                string AppPath = AppDomain.CurrentDomain.BaseDirectory;
                return Path.Combine(AppPath, "HolidayDataBase.csv");
            }
        }

        public IEnumerable<Holiday> GetAll()
        {
            IList<Holiday> holidayList = new List<Holiday>();

            using (StreamReader reader = new StreamReader(_csvFilePath))
            {
                reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] values = line.Split(';');

                    string idString = values[0].Trim();
                    string name = values[1].Trim();
                    string dateTimeString = values[2].Trim();

                    int HolidayId;
                    int.TryParse(idString, out HolidayId);

                    string HolidayName = name;

                    DateTime HolidayDate;
                    DateTime.TryParse(dateTimeString, out HolidayDate);

                    Holiday holiday = new Holiday
                    {
                        Id = HolidayId,
                        Name = HolidayName,
                        Date = HolidayDate
                    };

                    holidayList.Add(holiday);
                }
            }
            return holidayList;
        }

        public Holiday GetById(int id)
        {
            return GetAll().FirstOrDefault(h => h.Id == id);
        }

        public void InitializeCSV()
        {
            IEnumerable<Holiday> HolidayList = new List<Holiday>
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

            using (StreamWriter writer = new StreamWriter(_csvFilePath, false, Encoding.UTF8))
            {
                writer.WriteLine("Id,Name,Date");
                foreach (Holiday holiday in HolidayList)
                {
                    writer.WriteLine($"{holiday.Id},{holiday.Name},{holiday.Date}");
                }
            }
        }
    }
}