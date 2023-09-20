using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Domain.Entities.Users;

namespace Timesheet.Domain.Interfaces
{
    public interface IUserRepository : IReaderRepository<User, Guid>, IWriterRepository<User, Guid>
    {
    }
}