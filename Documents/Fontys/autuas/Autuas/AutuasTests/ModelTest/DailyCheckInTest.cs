using Autuas.Enums;
using Autuas.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AutuasTests.ModelTest
{
    public class DailyCheckInTest
    {
        private DailyCheckIn _testCheckIn;

        public DailyCheckInTest()
        {
            _testCheckIn = new DailyCheckIn
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
            Assert.Equal(DateTime.Today, _testCheckIn.Date);
        }
        [Fact]
        public void TestSetDate()
        {
            _testCheckIn.Date = DateTime.Today.AddDays(2);
            Assert.Equal(DateTime.Today.AddDays(2), _testCheckIn.Date);
        }
        [Fact]
        public void TestGetPhysicalState()
        {
            Assert.Equal(PhysicalState.GOOD, _testCheckIn.PhysicalState);
        }
        [Fact]
        public void TestSetPhysicalState()
        {
            _testCheckIn.PhysicalState = PhysicalState.BAD;
            Assert.Equal(PhysicalState.BAD, _testCheckIn.PhysicalState);
        }
        [Fact]
        public void TestGetMentalState()
        {
            Assert.Equal(MentalState.MEH, _testCheckIn.MentalState);
        }
        [Fact]
        public void TestSetMentalState()
        {
            _testCheckIn.MentalState = MentalState.GOOD;
            Assert.Equal(MentalState.GOOD, _testCheckIn.MentalState);
        }
        [Fact]
        public void TestGetPositiveFeelings()
        {
            Assert.Null(_testCheckIn.PositiveFeelings);
        }
        [Fact]
        public void TestSetPositiveFeelings()
        {
            _testCheckIn.PositiveFeelings = PositiveFeelings.CALM;
            Assert.Equal(PositiveFeelings.CALM, _testCheckIn.PositiveFeelings);
        }
        [Fact]
        public void TestGetNegativeFeelings()
        {
            Assert.Equal(NegativeFeelings.LONELY, _testCheckIn.NegativeFeelings);
        }
        [Fact]
        public void TestSetNegativeFeelings()
        {
            _testCheckIn.NegativeFeelings = null;
            Assert.Null(_testCheckIn.NegativeFeelings);
        }
        [Fact]
        public void TestGetUserId()
        {
            Assert.Equal(2, _testCheckIn.UserID);
        }
        [Fact]
        public void TestDateTimeType()
        {
            Assert.IsType<DateTime>(_testCheckIn.Date);
        }
        [Fact]
        public void TestPhysicalStateType()
        {
            Assert.IsAssignableFrom<Enum>(_testCheckIn.PhysicalState);
        }
        [Fact]
        public void TestMentalStateType()
        {
            Assert.IsAssignableFrom<Enum>(_testCheckIn.MentalState);
        }
        [Fact]
        public void TestPositiveFeelingsType()
        {
            _testCheckIn.PositiveFeelings = PositiveFeelings.CALM;
            Assert.IsAssignableFrom<Enum>(_testCheckIn.PositiveFeelings);
        }
        [Fact]
        public void TestNegativeFeelingsType()
        {
            Assert.IsAssignableFrom<Enum>(_testCheckIn.NegativeFeelings);
        }
        [Fact]
        public void TestUserIdType()
        {
            Assert.IsType<int>(_testCheckIn.UserID);
        }
        [Fact]
        public void TestIdType()
        {
            Assert.IsType<int>(_testCheckIn.ID);
        }

    }
}

