using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Domain.Entities.Users;

namespace Timesheet.Application.Mappers
{
    public static class UserMapper
    {
        public static User ToEntity(this UserAddForm form)
        {
            return new User
            {
                Id = Guid.NewGuid(),
                FirstName = form.FirstName,
                LastName = form.LastName,
                MailAdress = form.MailAdress,
            };
        }

        public static User ToEntity(this UserAddForm form, Guid id)
        {
            return new User
            {
                Id = id,
                FirstName = form.FirstName,
                LastName = form.LastName,
                MailAdress = form.MailAdress,
            };
        }
    }
}