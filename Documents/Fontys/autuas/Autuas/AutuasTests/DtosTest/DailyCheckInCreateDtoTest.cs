using Autuas.Dtos;
using Autuas.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AutuasTests.DtosTest
{
    public class DailyCheckInCreateDtoTest
    {
        private DailyCheckInCreateDto _createDto;
        public DailyCheckInCreateDtoTest()
        {
            _createDto = new DailyCheckInCreateDto()
            {
                Date = DateTime.Today,
                PhysicalState = PhysicalState.GOOD,
                MentalState = MentalState.MEH,
                PositiveFeelings = null,
                NegativeFeelings = NegativeFeelings.LONELY,
                UserID = 2
            };
        }
        [Fact]
        public void TestGetDate()
        {
            Assert.Equal(DateTime.Today, _createDto.Date);
        }
        [Fact]
        public void TestSetDate()
        {
            _createDto.Date = DateTime.Today.AddDays(2);
            Assert.Equal(DateTime.Today.AddDays(2), _createDto.Date);
        }
        [Fact]
        public void TestGetPhysicalState()
        {
            Assert.Equal(PhysicalState.GOOD, _createDto.PhysicalState);
        }
        [Fact]
        public void TestSetPhysicalState()
        {
            _createDto.PhysicalState = PhysicalState.BAD;
            Assert.Equal(PhysicalState.BAD, _createDto.PhysicalState);
        }
        [Fact]
        public void TestGetMentalState()
        {
            Assert.Equal(MentalState.MEH, _createDto.MentalState);
        }
        [Fact]
        public void TestSetMentalState()
        {
            _createDto.MentalState = MentalState.GOOD;
            Assert.Equal(MentalState.GOOD, _createDto.MentalState);
        }
        [Fact]
        public void TestGetPositiveFeelings()
        {
            Assert.Null(_createDto.PositiveFeelings);
        }
        [Fact]
        public void TestSetPositiveFeelings()
        {
            _createDto.PositiveFeelings = PositiveFeelings.CALM;
            Assert.Equal(PositiveFeelings.CALM, _createDto.PositiveFeelings);
        }
        [Fact]
        public void TestGetNegativeFeelings()
        {
            Assert.Equal(NegativeFeelings.LONELY, _createDto.NegativeFeelings);
        }
        [Fact]
        public void TestSetNegativeFeelings()
        {
            _createDto.NegativeFeelings = null;
            Assert.Null(_createDto.NegativeFeelings);
        }
        [Fact]
        public void TestGetUserId()
        {
            Assert.Equal(2, _createDto.UserID);
        }
        [Fact]
        public void TestDateTimeType()
        {
            Assert.IsType<DateTime>(_createDto.Date);
        }
        [Fact]
        public void TestPhysicalStateType()
        {
            Assert.IsAssignableFrom<Enum>(_createDto.PhysicalState);
        }
        [Fact]
        public void TestMentalStateType()
        {
            Assert.IsAssignableFrom<Enum>(_createDto.MentalState);
        }
        [Fact]
        public void TestPositiveFeelingsType()
        {
            _createDto.PositiveFeelings = PositiveFeelings.CALM;
            Assert.IsAssignableFrom<Enum>(_createDto.PositiveFeelings);
        }
        [Fact]
        public void TestNegativeFeelingsType()
        {
            Assert.IsAssignableFrom<Enum>(_createDto.NegativeFeelings);
        }
        [Fact]
        public void TestUserIdType()
        {
            Assert.IsAssignableFrom<int>(_createDto.UserID);
        }

    }
}
