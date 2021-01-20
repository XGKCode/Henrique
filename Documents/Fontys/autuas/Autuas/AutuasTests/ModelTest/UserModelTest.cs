using Autuas.Models;
using System;
using Xunit;

namespace AutuasTests
{
    public class UserModelTest
    {
        private User _testUser;
        public UserModelTest()
        {
            _testUser = new User
            {
                UserID = 1,
                Name = "John Doe",
                Username = "johnD",
                Email = "test@test.com",
                //Password = "testPass001",
                Created_at = DateTime.Today
            };
        }
        [Fact]
        public void TestGetEmail()
        {
            Assert.Equal("test@test.com", _testUser.Email);
        }
        [Fact]
        public void TestSetEmail()
        {
            _testUser.Email = "test";
            Assert.Equal("test", _testUser.Email);
        }
        [Fact]
        public void TestGetUserID()
        {
            Assert.Equal(1, _testUser.UserID);
        }
        [Fact]
        public void TestSetUserID()
        {
            _testUser.UserID = 2;
            Assert.Equal(2, _testUser.UserID);
        }
        [Fact]
        public void TestGetName()
        {
            Assert.Equal("John Doe", _testUser.Name);
        }
        [Fact]
        public void TestSetName()
        {
            _testUser.Name = "Jane Doe";
            Assert.Equal("Jane Doe", _testUser.Name);

        }
        [Fact]
        public void TestGetUsername()
        {
            Assert.Equal("johnD", _testUser.Username);
        }
        [Fact]
        public void TestSetUsername()
        {
            _testUser.Username = "johhnyboy";
            Assert.Equal("johhnyboy", _testUser.Username);
        }
        /* [Fact]
         public void TestGetPassword()
         {
             Assert.Equal("testPass001", _testUser.Password);
         }
         [Fact]
         public void TestSetPassword()
         {
             _testUser.Password = "passWord001";
             Assert.Equal("passWord001", _testUser.Password);
         }*/
        [Fact]
        public void TestGetCreated_at()
        {
            Assert.Equal(DateTime.Today, _testUser.Created_at);
        }
        [Fact]
        public void TestSetCreated_at()
        {
            _testUser.Created_at = DateTime.Today.AddDays(2);
            Assert.Equal(DateTime.Today.AddDays(2), _testUser.Created_at);
        }
        [Fact]
        public void TestNameType()
        {
            Assert.IsType<string>(_testUser.Name);
        }
        [Fact]
        public void TestUsernameType()
        {
            Assert.IsType<string>(_testUser.Username);
        }
        /*[Fact]
        public void TestPasswordType()
        {
            Assert.IsType<string>(_testUser.Password);
        }*/
        [Fact]
        public void TestEmailType()
        {
            Assert.IsType<string>(_testUser.Email);
        }
        [Fact]
        public void TestDateTimeType()
        {
            Assert.IsType<DateTime>(_testUser.Created_at);
        }



    }
}
