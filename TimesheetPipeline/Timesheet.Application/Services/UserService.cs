using Isopoh.Cryptography.Argon2;
using Timesheet.Application.Mappers;
using Timesheet.Domain.Entities.Users;
using Timesheet.Domain.Interfaces;

namespace Timesheet.Application.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _repository;

        public UserService(IUserRepository Repository)
        {
            _repository = Repository;
        }

        public async Task<Guid> AddAsync(User entity)
        {
            string hashedPassword = Argon2.Hash(entity.HashedPassword);
            entity.HashedPassword = hashedPassword;
            entity.Role = RoleType.Regular;
            return await _repository.AddAsync(entity);
        }

        public async Task<Guid> AddAsync(UserAddForm form) 
        {
            string hashedPassword = Argon2.Hash(form.Password);
            form.Password = hashedPassword;
            User entityToAdd = form.ToEntity();
            entityToAdd.Role = RoleType.Regular;
            return await _repository.AddAsync(entityToAdd);
        }

        public async Task<Guid> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<User> LoginAsync(LoginForm form)
        {
            string hashedPassword = await _repository.GetUserHashedPasswordByMailAdressAsync(form.MailAdress);

            if(Argon2.Verify(hashedPassword,form.Password))
            {
                return await _repository.GetByMailAdressAsync(form.MailAdress);
            }
            else
            {
                throw new Exception("Le mot de passe de corresponds pas.");
            }
        }

        public async Task<Guid> UpdateAsync(User entity)
        {
            return await _repository.UpdateAsync(entity);
        }

        public async Task<Guid> UpdateAsync(Guid userIdToUpdate, UserUpdateForm form)
        {
            string hashedPassword = Argon2.Hash(form.Password);
            form.Password = hashedPassword;
            return await _repository.UpdateAsync(form.ToEntity(userIdToUpdate));
        }
    }
}