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
            if(entity is null || entity == default) throw new ArgumentNullException();

            _context.Users.Add(entity);
            _context.SaveChanges();

            return entity.Id;
        }

        public Guid Update(User entity)
        {
            User entityToUpdate = _context.Users.Find(entity.Id);

            if (entityToUpdate is null || entityToUpdate == default) throw new ArgumentNullException();

            entity.Timesheets = entityToUpdate.Timesheets;
            entityToUpdate = entity;

            _context.SaveChanges();

            return entityToUpdate.Id;
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