using Isopoh.Cryptography.Argon2;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Guid> AddAsync(User entity)
        {
            if (entity is null || entity == default) throw new ArgumentNullException();

            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task<Guid> UpdateAsync(User entity)
        {
            if (entity is null) throw new ArgumentNullException("The entity to update can't be null");

            if (entity.Id == Guid.Empty) throw new ArgumentNullException("The guid of the updated entity can't be null.");

            User? userToUpdate = await _context.Users.FirstOrDefaultAsync(u => u.Id == entity.Id);

            if (userToUpdate is null || userToUpdate == default) throw new ArgumentNullException("L'id du User à mettre à jour n'existe pas.");

            userToUpdate.FirstName = entity.FirstName;
            userToUpdate.LastName = entity.LastName;
            userToUpdate.MailAdress = entity.MailAdress;
            userToUpdate.HashedPassword = entity.HashedPassword;
            userToUpdate.Role = entity.Role;

            await _context.SaveChangesAsync();

            return userToUpdate.Id;
        }

        public async Task<Guid> UpdateRoleAsync(User entity)
        {
            if (entity is null) throw new ArgumentNullException("The entity to update can't be null");

            if (entity.Id == Guid.Empty) throw new ArgumentNullException("The guid of the updated entity can't be null.");

            User? userToUpdate = await _context.Users.FirstOrDefaultAsync(u => u.Id == entity.Id);

            if (userToUpdate is null || userToUpdate == default) throw new ArgumentNullException("L'id du User à mettre à jour n'existe pas.");

            userToUpdate.Role = entity.Role;

            await _context.SaveChangesAsync();

            return userToUpdate.Id;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentNullException($"Impossible de rechercher un id vide : {id}");

            User? userToFind = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (userToFind is null || userToFind == default) throw new ArgumentNullException($"Le user avec l'ID {id} n'existe pas.");

            return userToFind;
        }

        public async Task<Guid> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentNullException();

            User? userToDelete = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (userToDelete is null || userToDelete == default) throw new ArgumentNullException();

            _context.Remove(userToDelete);
            await _context.SaveChangesAsync();

            return userToDelete.Id;
        }

        public async Task<string> GetUserHashedPasswordByMailAdressAsync(string mailAdress)
        {
            User? userForPassword = await _context.Users.FirstOrDefaultAsync(u => u.MailAdress == mailAdress);

            if (userForPassword is null || userForPassword == default) throw new ArgumentNullException();

            return userForPassword.HashedPassword;
        }

        public async Task<User> GetByMailAdressAsync(string mailAdress)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.MailAdress == mailAdress);

            if(user is null || user == default) throw new ArgumentNullException();

            return user;
        }

        public async Task InitializeDatabaseAsync()
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