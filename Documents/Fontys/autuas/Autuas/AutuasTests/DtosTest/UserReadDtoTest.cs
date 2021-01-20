using Autuas.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AutuasTests.DtosTest
{
    public class UserReadDtoTest
    {
        private UserReadDto _readDto;

        public UserReadDtoTest()
        {
            _readDto = new UserReadDto()
            {
                UserID = 1,
                Name = "Test User",
                Username = "test123",
                Email = "test@test.com"
            };
        }
        [Fact]
        public void TestGetUserID()
        {
            Assert.Equal(1, _readDto.UserID);
        }
        [Fact]
        public void TestSetUserID()
        {
            _readDto.UserID = 2;
            Assert.Equal(2, _readDto.UserID);
        }
        [Fact]
        public void TestGetName()
        {
            Assert.Equal("Test User", _readDto.Name);
        }
        [Fact]
        public void TestSetName()
        {
            _readDto.Name = "Test User123";
            Assert.Equal("Test User123", _readDto.Name);
        }
        [Fact]
        public void TestGetUsername()
        {
            Assert.Equal("test123", _readDto.Username);
        }
        [Fact]
        public void TestSetUsername()
        {
            _readDto.Username = "test";
            Assert.Equal("test", _readDto.Username);
        }
        [Fact]
        public void TestGetEmail()
        {
            Assert.Equal("test@test.com", _readDto.Email);
        }
        [Fact]
        public void TestSetEmail()
        {
            _readDto.Email = "test@com";
            Assert.Equal("test@com", _readDto.Email);
        }
        [Fact]
        public void TestUserIDType()
        {
            Assert.IsType<int>(_readDto.UserID);
        }
        [Fact]
        public void TestNameType()
        {
            Assert.IsType<string>(_readDto.Name);
        }
        [Fact]
        public void TestUsernameType()
        {
            Assert.IsType<string>(_readDto.Username);
        }
        [Fact]
        public void TestEmailType()
        {
            Assert.IsType<string>(_readDto.Email);
        }
    }
}
