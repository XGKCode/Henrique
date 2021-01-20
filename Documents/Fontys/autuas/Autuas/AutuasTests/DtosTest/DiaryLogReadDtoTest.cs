using Autuas.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AutuasTests.DtosTest
{
    public class DiaryLogReadDtoTest
    {
        private DiaryLogReadDto _log;

        public DiaryLogReadDtoTest()
        {
            _log = new DiaryLogReadDto()
            {
                Id = 1,
                Type = "Life",
                Description = "Have some fun",
                DateLog = DateTime.Today,
                UserId = 2
            };
        }
        [Fact]
        public void TestGetId()
        {
            Assert.Equal(1, _log.Id);
        }
        [Fact]
        public void TestSetId()
        {
            _log.Id = 2;
            Assert.Equal(2, _log.Id);
        }
        [Fact]
        public void TestGetType()
        {
            Assert.Equal("Life", _log.Type);
        }
        [Fact]
        public void TestSetType()
        {
            _log.Type = "Courage";
            Assert.Equal("Courage", _log.Type);
        }
        [Fact]
        public void TestGetDescription()
        {
            Assert.Equal("Have some fun", _log.Description);
        }
        [Fact]
        public void TestSetDescription()
        {
            _log.Description = "Today I went ...";
            Assert.Equal("Today I went ...", _log.Description);
        }
        [Fact]
        public void TestGetDateLog()
        {
            Assert.Equal(DateTime.Today, _log.DateLog);
        }
        [Fact]
        public void TestSetDateLog()
        {
            _log.DateLog = DateTime.Today.AddDays(2);
            Assert.Equal(DateTime.Today.AddDays(2), _log.DateLog);
        }
        [Fact]
        public void TestGetUserId()
        {
            Assert.Equal(2, _log.UserId);
        }
        [Fact]
        public void TestSetUserId()
        {
            _log.UserId = 1;
            Assert.Equal(1, _log.UserId);
        }
        [Fact]
        public void TestUserIdType()
        {
            Assert.IsType<int>(_log.UserId);
        }
        [Fact]
        public void TestDateLogType()
        {
            Assert.IsType<DateTime>(_log.DateLog);
        }
        [Fact]
        public void TestDescriptionType()
        {
            Assert.IsType<string>(_log.Description);
        }
        [Fact]
        public void TestTypeType()
        {
            Assert.IsType<string>(_log.Type);
        }
        [Fact]
        public void TestIdType()
        {
            Assert.IsType<int>(_log.Id);
        }
    }
}
