using Timesheet.Domain.Entities.Users;

namespace Timesheet.Domain.Interfaces
{
    public interface IUserRepository : IReaderRepository<User, Guid>, IWriterRepository<User, Guid>
    {
    }
}