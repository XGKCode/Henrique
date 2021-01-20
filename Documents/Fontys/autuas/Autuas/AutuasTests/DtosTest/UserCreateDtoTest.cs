using Autuas.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AutuasTests.DtosTest
{
    public class UserCreateDtoTest
    {
        private UserCreateDto _createDto;
        public UserCreateDtoTest()
        {
            _createDto = new UserCreateDto()
            {
                Name = "John Doe",
                Username = "johnD",
                Email = "johnd@test.com",
                Password = "testPass001",
                Created_at = DateTime.Today
            };
        }
        [Fact]
        public void TestGetEmail()
        {
            Assert.Equal("johnd@test.com", _createDto.Email);
        }
        [Fact]
        public void TestSetEmail()
        {
            _createDto.Email = "johnnyD@test.com";
            Assert.Equal("johnnyD@test.com", _createDto.Email);
        }

        [Fact]
        public void TestGetName()
        {
            Assert.Equal("John Doe", _createDto.Name);
        }
        [Fact]
        public void TestSetName()
        {
            _createDto.Name = "Jane Doe";
            Assert.Equal("Jane Doe", _createDto.Name);

        }
        [Fact]
        public void TestGetUsername()
        {
            Assert.Equal("johnD", _createDto.Username);
        }
        [Fact]
        public void TestSetUsername()
        {
            _createDto.Username = "johhnyboy";
            Assert.Equal("johhnyboy", _createDto.Username);
        }
        [Fact]
        public void TestGetPassword()
        {
            Assert.Equal("testPass001", _createDto.Password);
        }
        [Fact]
        public void TestSetPassword()
        {
            _createDto.Password = "passWord001";
            Assert.Equal("passWord001", _createDto.Password);
        }
        [Fact]
        public void TestGetCreated_at()
        {
            Assert.Equal(DateTime.Today, _createDto.Created_at);
        }
        [Fact]
        public void TestSetCreated_at()
        {
            _createDto.Created_at = DateTime.Today.AddDays(2);
            Assert.Equal(DateTime.Today.AddDays(2), _createDto.Created_at);
        }
        [Fact]
        public void TestNameType()
        {
            Assert.IsType<string>(_createDto.Name);
        }
        [Fact]
        public void TestUsernameType()
        {
            Assert.IsType<string>(_createDto.Username);
        }
        [Fact]
        public void TestPasswordType()
        {
            Assert.IsType<string>(_createDto.Password);
        }
        [Fact]
        public void TestEmailType()
        {
            Assert.IsType<string>(_createDto.Email);
        }
        [Fact]
        public void TestDateTimeType()
        {
            Assert.IsType<DateTime>(_createDto.Created_at);
        }
    }
}
