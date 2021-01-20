using Autuas.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AutuasTests.DtosTest
{
    public class AuthenticateUserModelTest
    {
        private AuthenticateUserModel _userModel;
        public AuthenticateUserModelTest()
        {
            _userModel = new AuthenticateUserModel()
            {
                Username = "test",
                Password = "123456789"
            };
        }
        [Fact]
        public void TestGetUsername()
        {
            Assert.Equal("test", _userModel.Username);
        }
        [Fact]
        public void TestSetUsername()
        {
            _userModel.Username = "test123";
            Assert.Equal("test123", _userModel.Username);
        }
        [Fact]
        public void TestGetPassword()
        {
            Assert.Equal("123456789", _userModel.Password);
        }
        [Fact]
        public void TestSetPassword()
        {
            _userModel.Password = "123";
            Assert.Equal("123", _userModel.Password);
        }
        [Fact]
        public void TestUsernameType()
        {
            Assert.IsType<string>(_userModel.Username);
        }
        [Fact]
        public void TestPasswordType()
        {
            Assert.IsType<string>(_userModel.Password);
        }
    }
}
