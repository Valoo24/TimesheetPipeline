using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Domain.Entities;
using Timesheet.Domain.Entities.Timesheets;
using Timesheet.Domain.Entities.Users;

namespace Timesheet.Application.Mappers
{
    public static class TimesheetMapper
    {
        public static TimesheetEntity ToEntity(this TimesheetCreateForm form)
        {
            return new TimesheetEntity
            {
                Id = Guid.NewGuid(),
                User = new User
                {
                    Id = form.UserId
                },
                Year = form.Year,
                Month = form.Month,
                OccupationList = new List<Occupation>()
            };
        }
    }
}