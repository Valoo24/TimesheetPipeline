using Timesheet.Domain.Entities.Users;

namespace Timesheet.Domain.Interfaces
{
    public interface IUserService : IReaderService<User, Guid>, IWriterService<User, Guid>
    {
        Task<Guid> AddAsync(User entity, RoleType entityRole);
        Task<User> LoginAsync(LoginForm form);
        Task<Guid> UpdateAsync(Guid id, RoleType entityRole);
    }
}