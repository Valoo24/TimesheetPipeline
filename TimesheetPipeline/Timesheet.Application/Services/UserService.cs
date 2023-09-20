using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Application.Mappers;
using Timesheet.Domain.Entities;
using Timesheet.Domain.Entities.Users;
using Timesheet.Domain.Interfaces;
using Timesheet.Persistence.Repositories;

namespace Timesheet.Application.Services
{
    public class UserService : IUserService
    {
        private UserRepository _repository;

        public UserService(UserRepository Repository)
        {
            _repository = Repository;
        }

        public Guid Add(User entity)
        {
            return _repository.Add(entity);
        }

        public Guid Add(UserAddForm form) 
        { 
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