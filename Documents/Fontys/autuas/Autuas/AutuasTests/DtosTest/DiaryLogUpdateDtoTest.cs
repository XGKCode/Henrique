using Autuas.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AutuasTests.DtosTest
{
    public class DiaryLogUpdateDtoTest
    {
        private DiaryLogUpdateDto _log;
        public DiaryLogUpdateDtoTest()
        {
            _log = new DiaryLogUpdateDto()
            {
                Description = "I am here to update this field"
            };
        }
        [Fact]
        public void TestGetDescription()
        {
            Assert.Equal("I am here to update this field", _log.Description);
        }
        [Fact]
        public void TestSetDescription()
        {
            _log.Description = "Updated";
            Assert.Equal("Updated", _log.Description);
        }
        [Fact]
        public void TestDescriptionType()
        {
            Assert.IsType<string>(_log.Description);
        }

    }
}
