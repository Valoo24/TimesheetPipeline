using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Domain.Entities;
using Timesheet.Domain.Entities.Users;
using Timesheet.Domain.Interfaces;
using Timesheet.Persistence.Repositories;

namespace Timesheet.Application.Services
{
    public class UserService : IReaderService<User, Guid>, IWriterService<User,Guid>
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
            User NewUser = new User 
            { 
                Id = Guid.NewGuid(),
                FirstName = form.FirstName,
                LastName = form.LastName,
                MailAdress = form.MailAdress,
            };

            return _repository.Add(NewUser);
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
            User NewUser = new User
            {
                Id = userIdToUpdate,
                FirstName = form.FirstName,
                LastName = form.LastName,
                MailAdress = form.MailAdress,
            };

            return _repository.Update(NewUser);
        }
    }
}