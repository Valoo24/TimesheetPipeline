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
using Xunit;

namespace Timesheet.Persistence.Test
{
    public class UserRepositoryShould
    {
        private IList<User> _testUsers = new List<User>()
        {
            new User
            {
                Id = Guid.NewGuid(),
                FirstName = "Brice",
                LastName = "DeNice",
                MailAdress = "BriceDeNice@mail.com",
            },
            new User
            {
                Id = Guid.NewGuid(),
                FirstName = "Elon",
                LastName = "Musk",
                MailAdress = "ElonMusk@mail.com",
            },
            new User
            {
                Id = Guid.NewGuid(),
                FirstName = "Tom",
                LastName = "Cruise",
                MailAdress = "TomCruise@mail.com",
            }
        };

        private DbContextOptions<TimesheetContext> _options;

        private TimesheetContext _context;

        private IUserRepository _repository;

        public UserRepositoryShould()
        {
            _options = new DbContextOptionsBuilder<TimesheetContext>()
            .UseInMemoryDatabase(databaseName: "TimesheetTestDBForUserRepository")
            .Options;

            _context = new TimesheetContext(_options);

            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            foreach(var user in _testUsers) 
            { 
                _context.Users.Add(user);
            }
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
            Assert.NotNull(userToTest.Timesheets);
            Assert.NotNull(result);
            Assert.IsType<Guid>(result);
            Assert.Equal(userToTest.Id, result);
        }

        [Fact]
        public void ThrowNullReferenceException()
        {
            try 
            {
                var result = _repository.Add(null);
            }
            catch (ArgumentNullException ex) 
            {
                Assert.NotNull(ex);
                Assert.IsType<ArgumentNullException>(ex);
            }
        }

        [Fact]
        public void GetAllUserEntity()
        {
            //Arrange & Act
            var result = _repository.GetAll();

            //Assert
            Assert.NotNull(result);
            foreach(var user in result)
            {
                Assert.NotNull(user.Id);
                Assert.Equal(_testUsers.FirstOrDefault(tu => tu.Id == user.Id).FirstName, user.FirstName);
                Assert.Equal(_testUsers.FirstOrDefault(tu => tu.Id == user.Id).LastName, user.LastName);
                Assert.Equal(_testUsers.FirstOrDefault(tu => tu.Id == user.Id).MailAdress, user.MailAdress);
            }
        }

        [Fact]
        public void GetElonMuskEntity()
        {
            var result = _repository.GetById(_testUsers.FirstOrDefault(tu => tu.FirstName == "Elon" && tu.LastName == "Musk" && tu.MailAdress == "ElonMusk@mail.com").Id);

            Assert.NotNull(result);
            Assert.Equal(_testUsers.FirstOrDefault(tu => tu.Id == result.Id), result);
        }
    }
}