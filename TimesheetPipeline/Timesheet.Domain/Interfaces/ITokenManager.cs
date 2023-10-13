using Timesheet.Domain.Entities.Users;

namespace Timesheet.Domain.Interfaces
{
    public interface ITokenManager
    {
        public string GenerateToken(User user);
    }
}