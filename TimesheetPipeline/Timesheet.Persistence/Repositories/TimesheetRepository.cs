using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Domain.Entities;
using Timesheet.Domain.Entities.Timesheets;
using Timesheet.Domain.Entities.Users;
using Timesheet.Domain.Interfaces;

namespace Timesheet.Persistence.Repositories
{
    public class TimesheetRepository : IReaderRepository<TimesheetEntity, Guid>
    {
        public string _csvFilePath
        {
            get
            {
                string AppPath = AppDomain.CurrentDomain.BaseDirectory;
                return Path.Combine(AppPath, "Timesheet.csv");
            }
        }

        public IEnumerable<TimesheetEntity> GetAll()
        {
            IList<TimesheetEntity> TimesheetList = new List<TimesheetEntity>();

            using (StreamReader reader = new StreamReader(_csvFilePath))
            {
                reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    string timesheetLine = reader.ReadLine();
                    //Index 0 = ID de la timesheet, 1 = ID du user, 2 = Année, 3 = Mois, 4 = Date de l'occupation, 5 = Titre de l'occupation.
                    string[] timesheetValues = timesheetLine.Split(',');

                    TimesheetEntity timesheet;

                    Guid idResult;
                    Guid.TryParse(timesheetValues[0], out idResult);

                    DateTime dateResult;
                    DateTime.TryParse(timesheetValues[4], out dateResult);

                    if (TimesheetList.Any(t => t.Id == idResult))
                    {
                        timesheet = TimesheetList.FirstOrDefault(t => t.Id == idResult);
                        int timesheetIndex = TimesheetList.IndexOf(timesheet);

                        timesheet.OccupationList.Add(new Occupation
                        {
                            Date = dateResult,
                            Title = timesheetValues[5]
                        });

                        TimesheetList.Insert(timesheetIndex, timesheet);
                        TimesheetList.RemoveAt(timesheetIndex + 1);
                    }
                    else
                    {
                        Guid userIdResult;
                        Guid.TryParse(timesheetValues[1], out userIdResult);

                        int yearResult;
                        int.TryParse(timesheetValues[2], out yearResult);

                        int monthResult;
                        int.TryParse(timesheetValues[3], out monthResult);

                        timesheet = new TimesheetEntity
                        {
                            Id = idResult,
                            User = new User
                            {
                                Id = userIdResult,
                            },
                            Year = yearResult,
                            Month = monthResult,
                            OccupationList = new List<Occupation>
                            {
                                new Occupation
                                {
                                    Date = dateResult,
                                    Title = timesheetValues[5]
                                }
                            }
                        };

                        TimesheetList.Add(timesheet);
                    }
                }
            }
            return TimesheetList;
        }

        public TimesheetEntity GetById(Guid id)
        {
            return GetAll().FirstOrDefault(t => t.Id == id);
        }

        public void InitializeCSV()
        {
            using (StreamWriter writer = new StreamWriter(_csvFilePath, false, Encoding.UTF8))
            {
                writer.WriteLine("Id,User Id,Year,Month,Date of the occupation,Occupation Title");
            }
        }
    }
}