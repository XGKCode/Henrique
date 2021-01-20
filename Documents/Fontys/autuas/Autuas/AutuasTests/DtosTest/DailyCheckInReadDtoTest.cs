using Autuas.Dtos;
using Autuas.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AutuasTests.DtosTest
{
    public class DailyCheckInReadDtoTest
    {
        private DailyCheckInReadDto _readDto;

        public DailyCheckInReadDtoTest()
        {
            _readDto = new DailyCheckInReadDto()
            {
                ID = 1,
                Date = DateTime.Today,
                PhysicalState = PhysicalState.GOOD,
                MentalState = MentalState.MEH,
                PositiveFeelings = null,
                NegativeFeelings = NegativeFeelings.LONELY,
                UserId = 2
            };
        }
        [Fact]
        public void TestGetId()
        {
            Assert.Equal(1, _readDto.ID);
        }
        [Fact]
        public void TestSetId()
        {
            _readDto.ID = 2;
            Assert.Equal(2, _readDto.ID);
        }
        [Fact]
        public void TestGetDate()
        {
            Assert.Equal(DateTime.Today, _readDto.Date);
        }
        [Fact]
        public void TestSetDate()
        {
            _readDto.Date = DateTime.Today.AddDays(2);
            Assert.Equal(DateTime.Today.AddDays(2), _readDto.Date);
        }
        [Fact]
        public void TestGetPhysicalState()
        {
            Assert.Equal(PhysicalState.GOOD, _readDto.PhysicalState);
        }
        [Fact]
        public void TestSetPhysicalState()
        {
            _readDto.PhysicalState = PhysicalState.BAD;
            Assert.Equal(PhysicalState.BAD, _readDto.PhysicalState);
        }
        [Fact]
        public void TestGetMentalState()
        {
            Assert.Equal(MentalState.MEH, _readDto.MentalState);
        }
        [Fact]
        public void TestSetMentalState()
        {
            _readDto.MentalState = MentalState.GOOD;
            Assert.Equal(MentalState.GOOD, _readDto.MentalState);
        }
        [Fact]
        public void TestGetPositiveFeelings()
        {
            Assert.Null(_readDto.PositiveFeelings);
        }
        [Fact]
        public void TestSetPositiveFeelings()
        {
            _readDto.PositiveFeelings = PositiveFeelings.CALM;
            Assert.Equal(PositiveFeelings.CALM, _readDto.PositiveFeelings);
        }
        [Fact]
        public void TestGetNegativeFeelings()
        {
            Assert.Equal(NegativeFeelings.LONELY, _readDto.NegativeFeelings);
        }
        [Fact]
        public void TestSetNegativeFeelings()
        {
            _readDto.NegativeFeelings = null;
            Assert.Null(_readDto.NegativeFeelings);
        }
        [Fact]
        public void TestGetUserId()
        {
            Assert.Equal(2, _readDto.UserId);
        }
        [Fact]
        public void TestDateTimeType()
        {
            Assert.IsType<DateTime>(_readDto.Date);
        }
        [Fact]
        public void TestPhysicalStateType()
        {
            Assert.IsAssignableFrom<Enum>(_readDto.PhysicalState);
        }
        [Fact]
        public void TestMentalStateType()
        {
            Assert.IsAssignableFrom<Enum>(_readDto.MentalState);
        }
        [Fact]
        public void TestPositiveFeelingsType()
        {
            _readDto.PositiveFeelings = PositiveFeelings.CALM;
            Assert.IsAssignableFrom<Enum>(_readDto.PositiveFeelings);
        }
        [Fact]
        public void TestNegativeFeelingsType()
        {
            Assert.IsAssignableFrom<Enum>(_readDto.NegativeFeelings);
        }
        [Fact]
        public void TestUserIdType()
        {
            Assert.IsType<int>(_readDto.UserId);
        }
        [Fact]
        public void TestIdType()
        {
            Assert.IsType<int>(_readDto.ID);
        }
    }
}

