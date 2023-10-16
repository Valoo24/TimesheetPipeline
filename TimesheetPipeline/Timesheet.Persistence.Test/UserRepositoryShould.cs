using Isopoh.Cryptography.Argon2;
using Microsoft.EntityFrameworkCore;
using Timesheet.Application.Mappers;
using Timesheet.Domain.Entities.Users;
using Timesheet.Domain.Interfaces;
using Timesheet.Infrastrucutre.DataAccess;
using Timesheet.Persistence.Repositories;

namespace Timesheet.Persistence.Test
{
    public class UserRepositoryShould
    {
        #region Properties
        private IList<UserDTO> _testUsers = new List<UserDTO>()
        {
            new UserDTO
            {
                Id = Guid.NewGuid(),
                FirstName = "Brice",
                LastName = "DeNice",
                MailAdress = "BriceDeNice@mail.com",
                HashedPassword = Argon2.Hash("Turlututu")
            },
            new UserDTO
            {
                Id = Guid.NewGuid(),
                FirstName = "Elon",
                LastName = "Musk",
                MailAdress = "ElonMusk@mail.com",
                HashedPassword = Argon2.Hash("Chapopointu")
            },
            new UserDTO
            {
                Id = Guid.NewGuid(),
                FirstName = "Tom",
                LastName = "Cruise",
                MailAdress = "TomCruise@mail.com",
                HashedPassword = Argon2.Hash("Topgun")
            }
        };

        private DbContextOptions<TimesheetContext> _options;

        private TimesheetContext _context;

        private IUserRepository _repository;
        #endregion

        #region Constructors
        public UserRepositoryShould()
        {
            _options = new DbContextOptionsBuilder<TimesheetContext>()
            .UseInMemoryDatabase(databaseName: "TimesheetTestDBForUserRepository")
            .Options;

            _context = new TimesheetContext(_options);

            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            foreach (var user in _testUsers)
            {
                _context.Users.Add(user);
            }
            _context.SaveChanges();

            _repository = new UserRepository(_context);
        }
        #endregion

        #region TestAddMethods
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
                HashedPassword = Argon2.Hash("AttaqueNapolitaine")
            };

            //Act
            var result = _repository.AddAsync(userToTest);

            //Assert
            Assert.NotNull(userToTest.Timesheets);
            Assert.NotNull(result);
            Assert.IsType<Guid>(result);
            Assert.Equal(userToTest.Id, result);
        }

        [Fact]
        public void ThrowArgumentNullExceptionWhenAddNull()
        {
            try
            {
                var result = _repository.AddAsync(null);
            }
            catch (ArgumentNullException ex)
            {
                Assert.NotNull(ex);
                Assert.IsType<ArgumentNullException>(ex);
            }
        }
        #endregion

        #region TestGetMethods
        [Fact]
        public void GetAllUserEntity()
        {
            //Arrange & Act
            var result = _repository.GetAllAsync();

            //Assert
            Assert.NotNull(result);
            foreach (var user in result)
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
            Assert.Equal(_testUsers.FirstOrDefault(tu => tu.Id == result.Id).FirstName, result.FirstName);
            Assert.Equal(_testUsers.FirstOrDefault(tu => tu.Id == result.Id).LastName, result.LastName);
            Assert.Equal(_testUsers.FirstOrDefault(tu => tu.Id == result.Id).MailAdress, result.MailAdress);
            Assert.Equal(_testUsers.FirstOrDefault(tu => tu.Id == result.Id).HashedPassword, result.HashedPassword);
        }

        [Fact]
        public void ThrowAnArgumentNullExceptionWhenGetByIdEmpty()
        {
            try
            {
                var result = _repository.GetByIdAsync(Guid.Empty);
            }
            catch (ArgumentNullException ex)
            {
                Assert.NotNull(ex);
                Assert.IsType<ArgumentNullException>(ex);
            }
        }
        #endregion

        #region TestUpdateMethods
        [Fact]
        public void UpdateTomCruiseEntityIntoTestEntity()
        {
            //Arrange
            User userToUpdate = new User
            {
                Id = _testUsers.FirstOrDefault(tu =>
                tu.FirstName == "Tom" &&
                tu.LastName == "Cruise" &&
                tu.MailAdress == "TomCruise@mail.com").Id,
                FirstName = "Toto",
                LastName = "Foo",
                MailAdress = "Test@mail.com"
            };

            //Act
            var result = _repository.UpdateAsync(userToUpdate);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(userToUpdate.Id, result);
            Assert.NotEqual(_testUsers.FirstOrDefault(tu =>
            tu.FirstName == "Tom" &&
            tu.LastName == "Cruise" &&
            tu.MailAdress == "TomCruise@mail.com"),
            _repository.GetByIdAsync(userToUpdate.Id).ToDTO());
        }

        [Fact]
        public void ThrowAnArgumentNullExceptionWhenUpdateANonExistingUserId()
        {
            //Arrange
            User userThatDoesNotExist = new User
            {
                Id = Guid.NewGuid(),
                FirstName = "Not",
                LastName = "Existing",
                MailAdress = "NotExisiting@Mail.com"
            };

            //Act & Assert
            try
            {
                var result = _repository.UpdateAsync(userThatDoesNotExist);
            }
            catch (ArgumentNullException ex)
            {
                Assert.NotNull(ex);
                Assert.IsType<ArgumentNullException>(ex);
            }
        }

        [Fact]
        public void ThrowAnArgumentNullExceptionWhenUpdateWithANullEntity()
        {
            try
            {
                var result = _repository.UpdateAsync(null);
            }
            catch (ArgumentNullException ex)
            {
                Assert.NotNull(ex);
                Assert.IsType<ArgumentNullException>(ex);
            }
        }

        [Fact]
        public void ThrowAnArgumentNullExceptionWhenUpdatedUserHasAnEmptyGuidAsId()
        {
            User userWithABadGuid = new User
            {
                Id = Guid.Empty,
                FirstName = "A",
                LastName = "B",
                MailAdress = "C"
            };

            try
            {
                var result = _repository.UpdateAsync(userWithABadGuid);
            }
            catch(ArgumentNullException ex)
            {
                Assert.NotNull(ex);
                Assert.IsType<ArgumentNullException>(ex);
            }
        }
        #endregion

        #region TestDeleteMethods
        [Fact]
        public void DeleteBriceDeNice()
        {
            UserDTO userToDelete = _testUsers.FirstOrDefault(tu => tu.FirstName == "Brice" && tu.LastName == "DeNice" && tu.MailAdress == "BriceDeNice@mail.com");

            var result = _repository.Delete(userToDelete.Id);

            var userList = _repository.GetAllAsync();

            IList<UserDTO> userToCompare = new List<UserDTO>();

            foreach(var user in userList) 
            {
                userToCompare.Add(user.ToDTO());
            }

            Assert.NotNull(result);
            Assert.Equal(userToDelete.Id, result);
            Assert.NotEqual(_testUsers, userToCompare);
        }

        [Fact]
        public void ThrowAnArgumentNullExceptionWhenDeletingAnEmptyGuid()
        {
            try
            {
                var result = _repository.DeleteAsync(Guid.Empty);
            }
            catch(ArgumentNullException ex) 
            {
                Assert.NotNull(ex);
                Assert.IsType<ArgumentNullException>(ex);
            }
        }

        [Fact]
        public void ThrowAnArgumentNullExceptionWhenDeletingANonExistingId()
        {
            try
            {
                var result = _repository.DeleteAsync(Guid.NewGuid());
            }
            catch(ArgumentNullException ex)
            {
                Assert.NotNull(ex);
                Assert.IsType<ArgumentNullException>(ex);
            }
        }
        #endregion
    }
}