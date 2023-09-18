using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timesheet.Domain.Entities.Users
{
    public class UserAddForm
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MailAdress { get; set; }
    }
}