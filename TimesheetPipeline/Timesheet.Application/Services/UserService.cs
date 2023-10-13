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
            string hashedPassword = Argon2.Hash(entity.HashedPassword);
            entity.HashedPassword = hashedPassword;
            return _repository.Add(entity);
        }

        public Guid Add(UserAddForm form) 
        {
            string hashedPassword = Argon2.Hash(form.Password);
            form.Password = hashedPassword;
            return _repository.Add(form.ToEntity());
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