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
    }
}