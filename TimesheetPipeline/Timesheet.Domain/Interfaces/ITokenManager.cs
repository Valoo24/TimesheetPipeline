using Timesheet.Domain.Entities.Users;

namespace Timesheet.Domain.Interfaces
{
    public interface ITokenManager
    {
        public string Issuer { get; }
        public string Audience { get; }
        public string Secret { get; }

        public string GenerateToken(User user);
    }
}