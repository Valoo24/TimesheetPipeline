using Timesheet.Application.Mappers;
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
            if (entity is null || entity == default) throw new ArgumentNullException();

            _context.Users.Add(entity.ToDTO());
            _context.SaveChanges();

            return entity.Id;
        }

        public Guid Update(User entity)
        {
            if (entity is null) throw new ArgumentNullException("The entity to update can't be null");

            if (entity.Id == Guid.Empty) throw new ArgumentNullException("The guid of the updated entity can't be null.");

            UserDTO entityToUpdate = _context.Users.FirstOrDefault(u => u.Id == entity.Id);

            if (entityToUpdate is null || entityToUpdate == default) throw new ArgumentNullException("The user you try to update doesn't exist");

            entityToUpdate.FirstName = entity.FirstName;
            entityToUpdate.LastName = entity.LastName;
            entityToUpdate.MailAdress = entity.MailAdress;

            _context.SaveChanges();

            return entityToUpdate.Id;
        }

        public IEnumerable<User> GetAll()
        {
            foreach(var user in _context.Users)
            {
                yield return user.ToEntity();
            }
        }

        public User GetById(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentNullException(id.ToString());

            return _context.Users.FirstOrDefault(u => u.Id == id).ToEntity();
        }

        public Guid Delete(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentNullException();

            UserDTO userToDelete = _context.Users.FirstOrDefault(u => u.Id == id);

            if (userToDelete is null || userToDelete == default) throw new ArgumentNullException();

            _context.Remove(userToDelete);
            _context.SaveChanges();

            return userToDelete.Id;
        }

        public string GetUserHashedPasswordByMailAdress(string mailAdress)
        {
            return _context.Users.FirstOrDefault(u => u.MailAdress == mailAdress).HashedPassword;
        }

        public User GetByMailAdress(string mailAdress)
        {
            return _context.Users.FirstOrDefault(u => u.MailAdress == mailAdress).ToEntity();
        }
    }
}