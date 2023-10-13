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

        public Guid Add(User entity)
        {
            entity.Role = RoleType.Regular;
            string hashedPassword = Argon2.Hash(entity.HashedPassword);
            entity.HashedPassword = hashedPassword;
            return _repository.Add(entity);
        }

        public Guid Add(UserAddForm form) 
        {
            string hashedPassword = Argon2.Hash(form.Password);
            form.Password = hashedPassword;
            User entityToAdd = form.ToEntity();
            entityToAdd.Role = RoleType.Regular;
            return _repository.Add(entityToAdd);
        }

        public Guid Delete(Guid id)
        {
            return _repository.Delete(id);
        }

        public IEnumerable<User> GetAll()
        {
            return _repository.GetAll();
        }

        public User GetById(Guid id)
        {
            return _repository.GetById(id);
        }

        public User Login(LoginForm form)
        {
            string hashedPassword = _repository.GetUserHashedPasswordByMailAdress(form.Credentials);

            if(Argon2.Verify(hashedPassword,form.Password))
            {
                return _repository.GetByMailAdress(form.Credentials);
            }
            else
            {
                throw new Exception("Le mot de passe de corresponds pas.");
            }
        }

        public Guid Update(User entity)
        {
            return _repository.Update(entity);
        }

        public Guid Update(Guid userIdToUpdate, UserAddForm form)
        {
            return _repository.Update(form.ToEntity(userIdToUpdate));
        }
    }
}