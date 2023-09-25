using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Domain.Entities;
using Timesheet.Domain.Entities.Timesheets;
using Timesheet.Domain.Entities.Users;
using Timesheet.Domain.Interfaces;

namespace Timesheet.Application.Mappers
{
    public static class TimesheetMapper
    {
        public static TimesheetEntity ToEntity(this TimesheetCreateForm form)
        {
            return new TimesheetEntity
            {
                UserId = form.UserId,
                Year = form.Year,
                Month = form.Month,
            };
        }

        public static TimesheetDTO ToDTO(this TimesheetEntity entity) 
        {
            return new TimesheetDTO
            {
                Id = entity.Id,
                User = new User 
                { 
                    Id = entity.UserId
                },
                Year = entity.Year,
                Month = entity.Month,
                OccupationList = entity.OccupationList
            };
        }
    }
}