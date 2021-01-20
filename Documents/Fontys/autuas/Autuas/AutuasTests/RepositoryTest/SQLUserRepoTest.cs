using Autuas.Data;
using Autuas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace AutuasTests.RepositoryTest
{
    public class SQLUserRepoTest : IDisposable
    {
        DbContextOptionsBuilder<ApplicationContext> optionsBuilder;
        ApplicationContext dbContext;
        SQLUserRepo _userRepo;

        public SQLUserRepoTest()
        {
            optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            optionsBuilder.UseInMemoryDatabase("UnitTestInMemoDB");
            dbContext = new ApplicationContext(optionsBuilder.Options);

            _userRepo = new SQLUserRepo(dbContext);
        }
        public void Dispose()
        {
            optionsBuilder = null;
            foreach (var user in dbContext.Users)
            {
                dbContext.Users.Remove(user);
            }
            dbContext.SaveChanges();
            dbContext.Dispose();
            _userRepo = null;
        }

        [Fact]
        public void GetUsersWhenDBIsEmpty_ReturnsZeroItemsTest()
        {
            //Arrange
            //Act
            var result = _userRepo.GetAllUsers();

            //Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetUsersWithOnlyOneUserInTheDBTest()
        {
            //Arrange
            var user = new User
            {

                Name = "John Doe",
                Username = "johnD",
                Email = "johnD@test.com",
                // Password = "MysafePassword123",
                Created_at = DateTime.Now
            };
            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            //Act
            var result = _userRepo.GetAllUsers();

            //Assert
            Assert.Single(result);
        }
        [Fact]
        public void GetUsersTest()
        {
            //Arrange
            var user = new User
            {
                Name = "John Doe",
                Username = "johnD",
                Email = "johnD@test.com",
                //Password = "MysafePassword123",
                Created_at = DateTime.Now
            };
            var user2 = new User
            {
                Name = "Jane Doe",
                Username = "janeD",
                Email = "janeD@test.com",
                //Password = "SafePassword987",
                Created_at = DateTime.Now
            };
            dbContext.Users.Add(user);
            dbContext.Users.Add(user2);
            dbContext.SaveChanges();

            //Act
            var result = _userRepo.GetAllUsers();

            //Assert
            Assert.Equal(2, result.Count());
        }
        [Fact]
        public void GetUsers_ReturnsTheCorrectTypeTest()
        {
            //Arrange

            //Act
            var result = _userRepo.GetAllUsers();
            //Assert
            Assert.IsAssignableFrom<IEnumerable<User>>(result);
        }
        [Fact]
        public void GetUserWithInvalidIDTest()
        {
            //Arrange
            //DB should be empty, therefore any ID is invalid

            //Act
            var result = _userRepo.GetUserByID(0);

            //Assert
            Assert.Null(result);
        }
        [Fact]
        public void GetUserByID_ReturnsTheCorrectTypeTest()
        {
            //Arrange
            var user = new User
            {
                Name = "John Doe",
                Username = "johnD",
                Email = "johnD@test.com",
                //Password = "MysafePassword123",
                Created_at = DateTime.Now
            };
            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            var id = user.UserID;

            //Act
            var result = _userRepo.GetUserByID(id);

            //Assert
            Assert.IsType<User>(result);
        }

        [Fact]
        public void GetUserByID_ReturnsCorrectResourceTest()
        {
            //Arrange
            var user = new User
            {
                Name = "John Doe",
                Username = "johnD",
                Email = "johnD@test.com",
                //  Password = "MysafePassword123",
                Created_at = DateTime.Now
            };
            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            var id = user.UserID;

            //Act
            var result = _userRepo.GetUserByID(id);

            //Assert
            Assert.Equal(id, result.UserID);
        }
        [Fact]
        public void AddUserTest()
        {
            //Arrange
            var user = new User
            {
                Name = "John Doe",
                Username = "johnD",
                Email = "johnD@test.com",
                // Password = "MysafePassword123",
                Created_at = DateTime.Now
            };
            var objCount = dbContext.Users.Count();

            //Act
            _userRepo.CreateUser(user);
            _userRepo.SaveChanges();

            //Assert
            Assert.Equal(objCount + 1, dbContext.Users.Count());
        }
        [Fact]
        public void DeleteUserTest()
        {
            //Arrange
            var user = new User
            {
                Name = "John Doe",
                Username = "johnD",
                Email = "johnD@test.com",
                //  Password = "MysafePassword123",
                Created_at = DateTime.Now
            };

            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            var objCount = dbContext.Users.Count();


            //Act
            _userRepo.DeleteUser(user);
            _userRepo.SaveChanges();

            //Assert
            Assert.Equal(objCount - 1, dbContext.Users.Count());
        }
        [Fact]
        public void AddNullUserTest_ThrowsException()
        {
            //Arrange
            User user = null;
            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => _userRepo.CreateUser(user));
        }
        [Fact]
        public void DeleteNullUser_ThrowsException()
        {
            //Arrange
            User user = null;
            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => _userRepo.DeleteUser(user));
        }
    }


}

