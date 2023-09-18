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
    public class UserService : IReaderService<User, Guid>
    {
        private UserRepository _repository;

        public UserService(UserRepository Repository)
        {
            _repository = Repository;
        }
        public IEnumerable<User> GetAll()
        {
            return _repository.GetAll();
        }

        public User GetById(Guid id)
        {
            return _repository.GetById(id);
        }
    }
}