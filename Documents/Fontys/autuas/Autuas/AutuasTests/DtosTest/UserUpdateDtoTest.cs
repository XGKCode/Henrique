using Autuas.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AutuasTests.DtosTest
{
    public class UserUpdateDtoTest
    {
        private UserUpdateDto _updateDto;

        public UserUpdateDtoTest()
        {
            _updateDto = new UserUpdateDto()
            {
                Name = "John Doe",
                Username = "johnD",
                Email = "johnd@test.com",
                Password = "testPass001",
            };
        }
        [Fact]
        public void TestGetEmail()
        {
            Assert.Equal("johnd@test.com", _updateDto.Email);
        }
        [Fact]
        public void TestSetEmail()
        {
            _updateDto.Email = "johnnyD@test.com";
            Assert.Equal("johnnyD@test.com", _updateDto.Email);
        }

        [Fact]
        public void TestGetName()
        {
            Assert.Equal("John Doe", _updateDto.Name);
        }
        [Fact]
        public void TestSetName()
        {
            _updateDto.Name = "Jane Doe";
            Assert.Equal("Jane Doe", _updateDto.Name);

        }
        [Fact]
        public void TestGetUsername()
        {
            Assert.Equal("johnD", _updateDto.Username);
        }
        [Fact]
        public void TestSetUsername()
        {
            _updateDto.Username = "johhnyboy";
            Assert.Equal("johhnyboy", _updateDto.Username);
        }
        [Fact]
        public void TestGetPassword()
        {
            Assert.Equal("testPass001", _updateDto.Password);
        }
        [Fact]
        public void TestSetPassword()
        {
            _updateDto.Password = "passWord001";
            Assert.Equal("passWord001", _updateDto.Password);
        }
        [Fact]
        public void TestNameType()
        {
            Assert.IsType<string>(_updateDto.Name);
        }
        [Fact]
        public void TestUsernameType()
        {
            Assert.IsType<string>(_updateDto.Username);
        }
        [Fact]
        public void TestPasswordType()
        {
            Assert.IsType<string>(_updateDto.Password);
        }
        [Fact]
        public void TestEmailType()
        {
            Assert.IsType<string>(_updateDto.Email);
        }

    }
}
