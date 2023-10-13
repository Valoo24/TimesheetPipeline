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
                HashedPassword = form.Password
            };
        }

        public static User ToEntity(this UserAddForm form, Guid id)
        {
            return new User
            {
                Id = id,
                FirstName = form.FirstName,
                LastName = form.LastName,
                MailAdress = form.MailAdress,
                HashedPassword = form.Password
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
                HashedPassword = form.Password,
                Role = form.Role
            };
        }

        public static User ToEntity(this UserDTO dto)
        {
            return new User
            {
                Id = dto.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                MailAdress = dto.MailAdress,
                HashedPassword = dto.HashedPassword,
                Role = dto.Role,
                Timesheets = dto.Timesheets
            };
        }

        public static UserDTO ToDTO(this User user) 
        {
            return new UserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                MailAdress = user.MailAdress,
                HashedPassword = user.HashedPassword,
                Role = user.Role,
                Timesheets = user.Timesheets
            };
        }
    }
}