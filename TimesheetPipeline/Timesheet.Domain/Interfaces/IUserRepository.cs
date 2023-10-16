using Timesheet.Domain.Entities.Users;

namespace Timesheet.Domain.Interfaces
{
    public interface IUserRepository : IReaderRepository<User, Guid>, IWriterRepository<User, Guid>
    {
        Task<User> GetByMailAdressAsync(string mailAdress);
        Task<string> GetUserHashedPasswordByMailAdressAsync(string mailAdress);
        Task InitializeDatabaseAsync();
        Task<Guid> UpdateRoleAsync(User entity);
    }
}