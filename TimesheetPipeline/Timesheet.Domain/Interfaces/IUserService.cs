using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Domain.Entities.Users;

namespace Timesheet.Domain.Interfaces
{
    public interface IUserService : IReaderService<User, Guid>, IWriterService<User, Guid>
    {
        public Guid Add(UserAddForm form);
        public Guid Update(Guid userIdToUpdate, UserAddForm form);
    }
}