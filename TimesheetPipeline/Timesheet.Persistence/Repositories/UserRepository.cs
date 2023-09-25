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
            IList<User> userList = GetAll().ToList();

            int index = userList.IndexOf(userList.FirstOrDefault(u => u.Id == entity.Id));

            userList.Insert(index, entity);

            userList.RemoveAt(index + 1);

            foreach (var user in userList)
            {
                Add(user);
            }

            return entity.Id;
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
            IList<User> userList = GetAll().ToList();

            userList.Remove(userList.FirstOrDefault(u => u.Id == id));

            foreach (var user in userList)
            {
                Add(user);
            }

            return id;
        }
    }
}