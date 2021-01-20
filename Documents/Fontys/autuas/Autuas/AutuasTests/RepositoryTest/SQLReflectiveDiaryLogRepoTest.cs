using Autuas.Data;
using Autuas.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace AutuasTests.RepositoryTest
{
    public class SQLReflectiveDiaryLogRepoTest : IDisposable
    {
        DbContextOptionsBuilder<ApplicationContext> optionsBuilder;
        ApplicationContext dbContext;
        SQLReflectiveDiaryLogRepo _diaryLogRepo;

        public SQLReflectiveDiaryLogRepoTest()
        {
            optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            optionsBuilder.UseInMemoryDatabase("UnitTestInMemoDB");
            dbContext = new ApplicationContext(optionsBuilder.Options);

            _diaryLogRepo = new SQLReflectiveDiaryLogRepo(dbContext);
        }
        public void Dispose()
        {
            optionsBuilder = null;
            foreach (var log in dbContext.DiaryLogs)
            {
                dbContext.DiaryLogs.Remove(log);
            }
            dbContext.SaveChanges();
            dbContext.Dispose();
            _diaryLogRepo = null;
        }
        [Fact]
        public void GetDiaryLogsWhenDBIsEmpty_ReturnsZeroItemsTest()
        {
            //Arrange
            //Act
            var result = _diaryLogRepo.GetAllReflectiveDiaryLogs();
            //Assert
            Assert.Empty(result);
        }
        [Fact]
        public void GetDiaryLogsWithOneLogInTheDBTest()
        {
            //Arrange
            var _log = new ReflectiveDiaryLog
            {
                Type = "Life",
                Description = "Have some fun",
                DateLog = DateTime.Today,
                UserId = 2
            };
            dbContext.DiaryLogs.Add(_log);
            dbContext.SaveChanges();
            //Act
            var result = _diaryLogRepo.GetAllReflectiveDiaryLogs();
            //Assert 
            Assert.Single(result);
        }
        [Fact]
        public void GetDiaryLogs()
        {
            //Arrange
            var _log = new ReflectiveDiaryLog
            {
                Type = "Life",
                Description = "Have some fun",
                DateLog = DateTime.Today,
                UserId = 2
            };
            var _log2 = new ReflectiveDiaryLog
            {
                Type = "Laugh",
                Description = "Have some fun2",
                DateLog = DateTime.Today,
                UserId = 1
            };
            dbContext.DiaryLogs.Add(_log);
            dbContext.DiaryLogs.Add(_log2);
            dbContext.SaveChanges();
            //Act
            var result = _diaryLogRepo.GetAllReflectiveDiaryLogs();
            //Assert
            Assert.Equal(2, result.Count());
        }
        [Fact]
        public void GetDiaryLogs_ReturnsTheCorrectTypeTest()
        {
            //Arrange
            //Act
            var result = _diaryLogRepo.GetAllReflectiveDiaryLogs();
            //Assert
            Assert.IsAssignableFrom<IEnumerable<ReflectiveDiaryLog>>(result);
        }
        [Fact]
        public void GetDiaryLogWithInvalidIdTest()
        {
            //Arrange
            //Db is empty so any Id is invalid
            //Act
            var result = _diaryLogRepo.GetReflectiveDiaryLogById(1);
            //Assert
            Assert.Null(result);
        }
        [Fact]
        public void GetDiaryLogWithInvalidUserIdTest()
        {
            //Arrange
            //Act
            var result = _diaryLogRepo.GetReflectiveDiaryLogsByUserId(1);
            //Assert
            Assert.Empty(result);
        }
        [Fact]
        public void GetDiaryLogById_ReturnsTheCorrectTypeTest()
        {
            //Arrange
            var _log = new ReflectiveDiaryLog
            {
                Type = "Life",
                Description = "Have some fun",
                DateLog = DateTime.Today,
                UserId = 2
            };
            dbContext.DiaryLogs.Add(_log);
            dbContext.SaveChanges();
            var id = _log.Id;
            //Act
            var result = _diaryLogRepo.GetReflectiveDiaryLogById(id);
            //Assert
            Assert.IsType<ReflectiveDiaryLog>(result);
        }
        [Fact]
        public void GetDiaryLogsByUserId_ReturnsTheCorrectTypeTest()
        {
            //Arrange
            var _log = new ReflectiveDiaryLog
            {
                Type = "Life",
                Description = "Have some fun",
                DateLog = DateTime.Today,
                UserId = 2
            };
            dbContext.DiaryLogs.Add(_log);
            dbContext.SaveChanges();
            var userId = _log.UserId;
            //Act
            var result = _diaryLogRepo.GetReflectiveDiaryLogsByUserId(userId);
            //Assert
            Assert.IsAssignableFrom<IEnumerable<ReflectiveDiaryLog>>(result);
        }
        [Fact]
        public void GetDiaryLogById_ReturnsCorrectResourceTest()
        {
            //Arrange
            var _log = new ReflectiveDiaryLog
            {
                Type = "Life",
                Description = "Have some fun",
                DateLog = DateTime.Today,
                UserId = 2
            };
            dbContext.DiaryLogs.Add(_log);
            dbContext.SaveChanges();
            var id = _log.Id;
            //Act
            var result = _diaryLogRepo.GetReflectiveDiaryLogById(id);
            //Assert
            Assert.Equal(id, result.Id);
        }
        [Fact]
        public void AddDiaryLog()
        {
            //Arrange
            var _log = new ReflectiveDiaryLog
            {
                Type = "Life",
                Description = "Have some fun",
                DateLog = DateTime.Today,
                UserId = 2
            };
            var objCount = dbContext.DiaryLogs.Count();
            //Act
            _diaryLogRepo.CreateReflectiveDiaryLog(_log);
            _diaryLogRepo.SaveChanges();
            //Assert
            Assert.Equal(objCount + 1, dbContext.DiaryLogs.Count());
        }
        [Fact]
        public void DeleteDiaryLog()
        {
            //Arrange
            var _log = new ReflectiveDiaryLog
            {
                Type = "Life",
                Description = "Have some fun",
                DateLog = DateTime.Today,
                UserId = 2
            };
            dbContext.DiaryLogs.Add(_log);
            dbContext.SaveChanges();
            var id = _log.Id;
            var objCount = dbContext.DiaryLogs.Count();
            //Act
            _diaryLogRepo.DeleteDiaryLog(id);
            _diaryLogRepo.SaveChanges();
            //Assert
            Assert.Equal(objCount - 1, dbContext.DailyCheckIns.Count());
        }
        [Fact]
        public void AddNullDiaryLogTest_ThrowsException()
        {
            //Arrange
            ReflectiveDiaryLog log = null;
            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => _diaryLogRepo.CreateReflectiveDiaryLog(log));
        }
        [Fact]
        public void DeleteNullDiaryLog_ThrowsException()
        {
            //Arrange
            ReflectiveDiaryLog log = new ReflectiveDiaryLog();
            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => _diaryLogRepo.DeleteDiaryLog(log.Id));
        }
    }
}
