using Autuas.Data;
using Autuas.Enums;
using Autuas.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace AutuasTests.RepositoryTest
{
    public class SQLDailyCheckInRepoTest : IDisposable
    {
        DbContextOptionsBuilder<ApplicationContext> optionsBuilder;
        ApplicationContext dbContext;
        SQLDailyCheckInRepo _checkInRepo;

        public SQLDailyCheckInRepoTest()
        {
            optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            optionsBuilder.UseInMemoryDatabase("UnitTestInMemoDB2");
            dbContext = new ApplicationContext(optionsBuilder.Options);

            _checkInRepo = new SQLDailyCheckInRepo(dbContext);
        }

        public void Dispose()
        {
            optionsBuilder = null;
            foreach (var checkIn in dbContext.DailyCheckIns)
            {
                dbContext.DailyCheckIns.Remove(checkIn);
            }
            dbContext.SaveChanges();
            dbContext.Dispose();
            _checkInRepo = null;
        }

        [Fact]
        public void GetDailyCheckInsWhenDBIsEmpty_ReturnsZeroItemsTest()
        {
            //Arrange
            //Act
            var result = _checkInRepo.GetAllDailyCheckIns();
            //Assert
            Assert.Empty(result);
        }
        [Fact]
        public void GetDailyCheckInsWithOneCheckInTest()
        {
            //Arrange
            var _testCheckIn = new DailyCheckIn
            {
                Date = DateTime.Today,
                PhysicalState = PhysicalState.GOOD,
                MentalState = MentalState.MEH,
                PositiveFeelings = null,
                NegativeFeelings = NegativeFeelings.LONELY,
                UserID = 2
            };
            dbContext.DailyCheckIns.Add(_testCheckIn);
            dbContext.SaveChanges();

            //Act
            var result = _checkInRepo.GetAllDailyCheckIns();

            //Assert
            Assert.Single(result);
        }
        [Fact]
        public void GetDailyCheckInsTest()
        {
            //Arrange
            var _testCheckIn1 = new DailyCheckIn
            {
                Date = DateTime.Today,
                PhysicalState = PhysicalState.GOOD,
                MentalState = MentalState.MEH,
                PositiveFeelings = null,
                NegativeFeelings = NegativeFeelings.LONELY,
                UserID = 2
            };
            var _testCheckIn2 = new DailyCheckIn
            {
                Date = DateTime.Today,
                PhysicalState = PhysicalState.GREAT,
                MentalState = MentalState.GREAT,
                PositiveFeelings = PositiveFeelings.ENTHUSIASTIC,
                NegativeFeelings = null,
                UserID = 1
            };
            dbContext.DailyCheckIns.Add(_testCheckIn1);
            dbContext.DailyCheckIns.Add(_testCheckIn2);
            dbContext.SaveChanges();

            //Act
            var result = _checkInRepo.GetAllDailyCheckIns();

            //Arrange
            Assert.Equal(2, result.Count());
        }
        [Fact]
        public void GetDailyCheckIns_ReturnsTheCorrectTypeTest()
        {
            //Arrange
            //Act
            var result = _checkInRepo.GetAllDailyCheckIns();
            //Assert
            Assert.IsAssignableFrom<IEnumerable<DailyCheckIn>>(result);
        }
        [Fact]
        public void GetDailyCheckInWithInvalidIdTest()
        {
            //Arrange
            //DB should be empty -> any Id will be invalid
            //Act
            var result = _checkInRepo.GetDailyCheckInById(0);
            Assert.Null(result);
        }
        [Fact]
        public void GetDailyCheckInByID_ReturnsTheCorrectTypeTest()
        {
            //Arrange
            var _testCheckIn = new DailyCheckIn
            {
                Date = DateTime.Today,
                PhysicalState = PhysicalState.GREAT,
                MentalState = MentalState.GREAT,
                PositiveFeelings = PositiveFeelings.ENTHUSIASTIC,
                NegativeFeelings = null,
                UserID = 2
            };
            dbContext.DailyCheckIns.Add(_testCheckIn);
            dbContext.SaveChanges();

            var id = _testCheckIn.ID;
            //Act
            var result = _checkInRepo.GetDailyCheckInById(id);
            //Assert
            Assert.IsAssignableFrom<DailyCheckIn>(result);
        }
        [Fact]
        public void GetDailyCheckInById_ReturnsCorrectResourceTest()
        {
            //Arrange
            var _testCheckIn = new DailyCheckIn
            {
                Date = DateTime.Today,
                PhysicalState = PhysicalState.GREAT,
                MentalState = MentalState.GREAT,
                PositiveFeelings = PositiveFeelings.ENTHUSIASTIC,
                NegativeFeelings = null,
                UserID = 2
            };
            dbContext.DailyCheckIns.Add(_testCheckIn);
            dbContext.SaveChanges();
            var id = _testCheckIn.ID;
            //Act
            var result = _checkInRepo.GetDailyCheckInById(id);
            //Assert 
            Assert.Equal(id, result.ID);
        }
        [Fact]
        public void GetDailyCheckInWithInvalidDateTest()
        {
            //Arrange
            //DB should be empty -> any Id will be invalid
            //Act
            var result = _checkInRepo.GetDailyCheckInsByDate(DateTime.Today);
            Assert.Empty(result);
        }
        [Fact]
        public void GetDailyCheckInByDate_ReturnsTheCorrectTypeTest()
        {
            //Arrange
            var _testCheckIn = new DailyCheckIn
            {
                Date = DateTime.Today,
                PhysicalState = PhysicalState.GREAT,
                MentalState = MentalState.GREAT,
                PositiveFeelings = PositiveFeelings.ENTHUSIASTIC,
                NegativeFeelings = null,
                UserID = 2
            };
            dbContext.DailyCheckIns.Add(_testCheckIn);
            dbContext.SaveChanges();

            var date = _testCheckIn.Date;
            //Act
            var result = _checkInRepo.GetDailyCheckInsByDate(date);
            //Assert
            Assert.IsAssignableFrom<IEnumerable<DailyCheckIn>>(result);
        }
        [Fact]
        public void GetDailyCheckInByDateTest()
        {
            //Arrange
            var _testCheckIn = new DailyCheckIn
            {
                Date = DateTime.Today,
                PhysicalState = PhysicalState.GREAT,
                MentalState = MentalState.GREAT,
                PositiveFeelings = PositiveFeelings.ENTHUSIASTIC,
                NegativeFeelings = null,
                UserID = 2
            };
            dbContext.DailyCheckIns.Add(_testCheckIn);
            dbContext.SaveChanges();
            var date = _testCheckIn.Date;
            //Act
            var result = _checkInRepo.GetDailyCheckInsByDate(date);
            //Assert 
            Assert.Single(result);
        }
        [Fact]
        public void GetDailyCheckInWithInvalidUserIdTest()
        {
            //Arrange
            //DB should be empty -> any Id will be invalid
            //Act
            var result = _checkInRepo.GetDailyCheckInsByUserID(2);
            Assert.Empty(result);
        }
        [Fact]
        public void GetDailyCheckInByUserID_ReturnsTheCorrectTypeTest()
        {
            //Arrange
            var _testCheckIn = new DailyCheckIn
            {
                Date = DateTime.Today,
                PhysicalState = PhysicalState.GREAT,
                MentalState = MentalState.GREAT,
                PositiveFeelings = PositiveFeelings.ENTHUSIASTIC,
                NegativeFeelings = null,
                UserID = 2
            };
            dbContext.DailyCheckIns.Add(_testCheckIn);
            dbContext.SaveChanges();

            var userId = _testCheckIn.UserID;
            //Act
            var result = _checkInRepo.GetDailyCheckInsByUserID(userId);
            //Assert
            Assert.IsAssignableFrom<IEnumerable<DailyCheckIn>>(result);
        }
        [Fact]
        public void GetDailyCheckInByUserIdTest()
        {
            //Arrange
            var _testCheckIn = new DailyCheckIn
            {
                Date = DateTime.Today,
                PhysicalState = PhysicalState.GREAT,
                MentalState = MentalState.GREAT,
                PositiveFeelings = PositiveFeelings.ENTHUSIASTIC,
                NegativeFeelings = null,
                UserID = 2
            };
            dbContext.DailyCheckIns.Add(_testCheckIn);
            dbContext.SaveChanges();
            var userId = _testCheckIn.UserID;
            //Act
            var result = _checkInRepo.GetDailyCheckInsByUserID(userId);
            //Assert 
            Assert.Single(result);
        }
        [Fact]
        public void AddDailyCheckInTest()
        {
            //Arrange
            var _testCheckIn = new DailyCheckIn
            {
                Date = DateTime.Today,
                PhysicalState = PhysicalState.GREAT,
                MentalState = MentalState.GREAT,
                PositiveFeelings = PositiveFeelings.ENTHUSIASTIC,
                NegativeFeelings = null,
                UserID = 2
            };
            var objCount = dbContext.DailyCheckIns.Count();
            //Act
            _checkInRepo.CreateDailyCheckIn(_testCheckIn);
            _checkInRepo.SaveChanges();
            //Assert
            Assert.Equal(objCount + 1, dbContext.DailyCheckIns.Count());
        }
        [Fact]
        public void DeleteDailyCheckInTest()
        {
            //Arrange
            var _testCheckIn = new DailyCheckIn
            {
                Date = DateTime.Today,
                PhysicalState = PhysicalState.GREAT,
                MentalState = MentalState.GREAT,
                PositiveFeelings = PositiveFeelings.ENTHUSIASTIC,
                NegativeFeelings = null,
                UserID = 2
            };
            dbContext.DailyCheckIns.Add(_testCheckIn);
            dbContext.SaveChanges();
            var objCount = dbContext.DailyCheckIns.Count();

            //Act
            _checkInRepo.DeleteDailyCheckInById(_testCheckIn.ID);
            _checkInRepo.SaveChanges();
            //Assert
            Assert.Equal(objCount - 1, dbContext.DailyCheckIns.Count());
        }
        [Fact]
        public void AddNullDailyCheckInTest_ThrowsException()
        {
            //Arrange
            DailyCheckIn checkIn = null;
            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => _checkInRepo.CreateDailyCheckIn(checkIn));
        }
        [Fact]
        public void DeleteNullDailyCheckInTest_ThrowsException()
        {
            //Arrange
            DailyCheckIn checkIn = new DailyCheckIn();
            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => _checkInRepo.DeleteDailyCheckInById(checkIn.ID));

        }
        [Fact]
        public void GetCountOfPositiveFeelingsTest()
        {
            //Arrange 
            //Add dailycheckin with positive feeling
            var _testCheckIn = new DailyCheckIn
            {
                Date = DateTime.Today,
                PhysicalState = PhysicalState.GREAT,
                MentalState = MentalState.GREAT,
                PositiveFeelings = PositiveFeelings.ENTHUSIASTIC,
                NegativeFeelings = null,
                UserID = 2
            };
            dbContext.DailyCheckIns.Add(_testCheckIn);
            dbContext.SaveChanges();
            //Act
            var positiveFeelingsCount = _checkInRepo.GetPositiveFeelingsCount(2);
            //Assert
            Assert.Equal(1, positiveFeelingsCount);
        }
        [Fact]
        public void GetNegativeFeelingsCountTest()
        {
            //Arrange 
            //Add dailycheckin with positive feeling
            var _testCheckIn = new DailyCheckIn
            {
                Date = DateTime.Today,
                PhysicalState = PhysicalState.GREAT,
                MentalState = MentalState.GREAT,
                PositiveFeelings = PositiveFeelings.ENTHUSIASTIC,
                NegativeFeelings = null,
                UserID = 2
            };
            dbContext.DailyCheckIns.Add(_testCheckIn);
            dbContext.SaveChanges();
            //add dailycheckin with negative feelings
            var _testCheckIn2 = new DailyCheckIn
            {
                Date = DateTime.Today,
                PhysicalState = PhysicalState.GREAT,
                MentalState = MentalState.GREAT,
                PositiveFeelings = null,
                NegativeFeelings = NegativeFeelings.APATHETIC,
                UserID = 2
            };
            dbContext.DailyCheckIns.Add(_testCheckIn2);
            dbContext.SaveChanges();
            //Act
            var negativeFeelingsCount = _checkInRepo.GetNegativeFeelingsCount(2);
            //Assert
            Assert.Equal(1, negativeFeelingsCount);
        }
    }
}
