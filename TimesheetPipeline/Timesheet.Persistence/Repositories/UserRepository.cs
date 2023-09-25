using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Domain.Entities.Users;
using Timesheet.Domain.Interfaces;
using Timesheet.Infrastrucutre.DataAccess;

namespace Timesheet.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private TimesheetContext _context;

        public UserRepository(TimesheetContext Context)
        {
            _context = Context;
        }

        public Guid Add(User entity)
        {

            _context.Users.Add(entity);
            _context.SaveChanges();

            return entity.Id;
        }

        public Guid Update(User entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User GetById(Guid id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }


        public Guid Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}