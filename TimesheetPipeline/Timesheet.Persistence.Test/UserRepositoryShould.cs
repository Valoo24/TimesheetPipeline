using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Domain.Entities.Users;
using Timesheet.Domain.Interfaces;
using Timesheet.Infrastrucutre.DataAccess;
using Timesheet.Persistence.Repositories;

namespace Timesheet.Persistence.Test
{
    public class UserRepositoryShould
    {
        private DbContextOptions<TimesheetContext> _options;

        private TimesheetContext _context;

        private IUserRepository _repository;

        public UserRepositoryShould()
        {
            _options = new DbContextOptionsBuilder<TimesheetContext>()
            .UseInMemoryDatabase(databaseName: "TimesheetTestDB")
            .Options;

            _context = new TimesheetContext(_options);

            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            _context.SaveChanges();

            _repository = new UserRepository(_context);
        }

        [Fact]
        public void AddAUserEntity()
        {
            //Arrange
            User userToTest = new User 
            { 
                Id = Guid.NewGuid(),
                FirstName = "Toto",
                LastName = "Foo",
                MailAdress = "Test@mail.com",
            };

            //Act
            var result = _repository.Add(userToTest);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(userToTest.Id, result);
        }
    }
}