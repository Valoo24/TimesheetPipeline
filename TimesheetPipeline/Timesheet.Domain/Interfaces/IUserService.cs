using Timesheet.Domain.Entities.Users;

namespace Timesheet.Domain.Interfaces
{
    public interface IUserService : IReaderService<User, Guid>, IWriterService<User, Guid>
    {
        Task<Guid> AddAsync(UserAddForm form);
        Task<Guid> UpdateAsync(Guid userIdToUpdate, UserUpdateForm form);
        Task<User> LoginAsync(LoginForm form);
    }
}