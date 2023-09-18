using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Domain.Interfaces;

namespace Timesheet.Domain.Entities.Users
{
    public class User : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MailAdress { get; set; }
    }
}