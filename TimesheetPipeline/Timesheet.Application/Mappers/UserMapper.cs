using Isopoh.Cryptography.Argon2;
using Timesheet.Domain.Entities.Users;

namespace Timesheet.Application.Mappers
{
    public static class UserMapper
    {
        public static User ToEntity(this UserAddForm form)
        {
            return new User
            {
                Id = Guid.NewGuid(),
                FirstName = form.FirstName,
                LastName = form.LastName,
                MailAdress = form.MailAdress,
                HashedPassword = Argon2.Hash(form.Password),
                Role = RoleType.Regular
            };
        }

        public static User ToEntity(this UserUpdateForm form, Guid id) 
        {
            return new User
            {
                Id = id,
                FirstName = form.FirstName,
                LastName = form.LastName,
                MailAdress = form.MailAdress,
                HashedPassword = Argon2.Hash(form.Password),
                Role = form.Role
            };
        }
    }
}