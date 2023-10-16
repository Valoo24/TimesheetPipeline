using Isopoh.Cryptography.Argon2;
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

            _context.Users.Add(entity);
            _context.SaveChanges();

            return entity.Id;
        }

        public Guid Update(User entity)
        {
            if (entity is null) throw new ArgumentNullException("The entity to update can't be null");

            if (entity.Id == Guid.Empty) throw new ArgumentNullException("The guid of the updated entity can't be null.");

            User userToUpdate = _context.Users.FirstOrDefault(u => u.Id == entity.Id);

            if (userToUpdate is null || userToUpdate == default) throw new ArgumentNullException("L'id du User à mettre à jour n'existe pas.");

            //UserDTO UpdatedEntity = entity.ToDTO();

            userToUpdate.FirstName = entity.FirstName;
            userToUpdate.LastName = entity.LastName;
            userToUpdate.MailAdress = entity.MailAdress;
            userToUpdate.HashedPassword = entity.HashedPassword;
            userToUpdate.Role = entity.Role;

            //_context.Users.Update(UpdatedEntity);

            _context.SaveChanges();

            return userToUpdate.Id;
        }

        public IEnumerable<User> GetAll()
        {
            foreach(var user in _context.Users)
            {
                yield return user;
            }
        }

        public User GetById(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentNullException($"Impossible de rechercher un id vide : {id}");

            User? userToFind = _context.Users.FirstOrDefault(u => u.Id == id);

            if (userToFind is null || userToFind == default) throw new ArgumentNullException($"Le user avec l'ID {id} n'existe pas.");

            return userToFind;
        }

        public Guid Delete(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentNullException();

            User? userToDelete = _context.Users.FirstOrDefault(u => u.Id == id);

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
            return _context.Users.FirstOrDefault(u => u.MailAdress == mailAdress);
        }

        public async Task InitializeDatabase()
        {
            await _context.Database.EnsureCreatedAsync();

            _context.Users.Add(new User
            {
                Id = Guid.NewGuid(),
                FirstName = "Admin",
                LastName = "System",
                MailAdress = "AdminSystem@mail.com",
                HashedPassword = Argon2.Hash("SysAdmin1234!"),
                Role = RoleType.Admin
            });

            await _context.SaveChangesAsync();
        }
    }
}