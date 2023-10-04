using Timesheet.Domain.Entities.Users;
using Timesheet.Domain.Interfaces;

namespace Timesheet.Domain.Entities.Timesheets
{
    public class TimesheetDTO : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public List<Occupation> OccupationList { get; set; }

        public TimesheetDTO()
        {
            OccupationList = new List<Occupation>();
            User = new User();
        }
    }
}