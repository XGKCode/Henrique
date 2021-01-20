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
    public class SQLGoalRepoTest : IDisposable
    {
        DbContextOptionsBuilder<ApplicationContext> optionsBuilder;
        ApplicationContext dbContext;
        SQLGoalRepo _goalRepo;

        public SQLGoalRepoTest()
        {
            optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            optionsBuilder.UseInMemoryDatabase("UnitTestInMemoDB1");
            dbContext = new ApplicationContext(optionsBuilder.Options);

            _goalRepo = new SQLGoalRepo(dbContext);
        }
        public void Dispose()
        {
            optionsBuilder = null;
            foreach (var goal in dbContext.Goals)
            {
                dbContext.Goals.Remove(goal);
            }
            dbContext.SaveChanges();
            dbContext.Dispose();
            _goalRepo = null;
        }

        [Fact]
        public void GetGoalsWhenDBIsEmpty_ReturnsZeroItemsTest()
        {
            //Arrange
            //Act
            var result = _goalRepo.GetAllGoals();
            //Assert
            Assert.Empty(result);
        }
        [Fact]
        public void GetGoalsWithOneGoalInTheDBTest()
        {
            //Arrange
            var _goal = new Goal
            {
                Title = "Learn Vue",
                Description = "Learn Vue and create a project with it",
                Completed = false,
                GoalType = "yearly",
                UserId = 2
            };
            dbContext.Goals.Add(_goal);
            dbContext.SaveChanges();
            //Act
            var result = _goalRepo.GetAllGoals();
            //Assert
            Assert.Single(result);
        }
        [Fact]
        public void GetAllGoalsTest()
        {
            //Arrange
            var _goal = new Goal
            {
                Title = "Learn Vue",
                Description = "Learn Vue and create a project with it",
                Completed = false,
                GoalType = "yearly",
                UserId = 2
            };
            var _goal2 = new Goal
            {
                Title = "Watch all 2020 movies",
                Description = "Watch all 2020 top imdb movies",
                Completed = false,
                GoalType = "yearly",
                UserId = 1
            };
            dbContext.Goals.Add(_goal);
            dbContext.Goals.Add(_goal2);
            dbContext.SaveChanges();
            //Act
            var result = _goalRepo.GetAllGoals();
            //Assert
            Assert.Equal(2, result.Count());
        }
        [Fact]
        public void GetGoals_ReturnsCorrectTypeTest()
        {
            //Arrange
            //Act
            var result = _goalRepo.GetAllGoals();
            //Assert
            Assert.IsAssignableFrom<IEnumerable<Goal>>(result);
        }
        [Fact]
        public void GetGoalsWithInvalidIdTest()
        {
            //Arrange
            //DB is empty so any id will be invalid
            //Act
            var result = _goalRepo.GetGoalById(1);
            //Assert
            Assert.Null(result);
        }
        [Fact]
        public void GetGoalById_ReturnsTheCorrectTypeTest()
        {
            var _goal2 = new Goal
            {
                Title = "Watch all 2020 movies",
                Description = "Watch all 2020 top imdb movies",
                Completed = false,
                GoalType = "yearly",
                UserId = 1
            };
            dbContext.Goals.Add(_goal2);
            dbContext.SaveChanges();

            var id = _goal2.Id;
            //Act
            var result = _goalRepo.GetGoalById(id);
            //Assert
            Assert.IsType<Goal>(result);
        }
        [Fact]
        public void GetGoalById_ReturnsCorrectResourceTest()
        {
            //Arrange
            var _goal2 = new Goal
            {
                Title = "Watch all 2020 movies",
                Description = "Watch all 2020 top imdb movies",
                Completed = false,
                GoalType = "yearly",
                UserId = 1
            };
            dbContext.Goals.Add(_goal2);
            dbContext.SaveChanges();
            var id = _goal2.Id;
            //Act
            var result = _goalRepo.GetGoalById(id);
            //Arrange
            Assert.Equal(id, result.Id);
        }
        [Fact]
        public void AddGoalTest()
        {
            //Arrange
            var _goal2 = new Goal
            {
                Title = "Watch all 2020 movies",
                Description = "Watch all 2020 top imdb movies",
                Completed = false,
                GoalType = "yearly",
                UserId = 1
            };
            var objCount = dbContext.Goals.Count();
            //Act
            _goalRepo.CreateGoal(_goal2);
            _goalRepo.SaveChanges();

            //Assert
            Assert.Equal(objCount + 1, dbContext.Goals.Count());
        }
        [Fact]
        public void DeleteGoal()
        {
            //Arrange
            var _goal2 = new Goal
            {
                Title = "Watch all 2020 movies",
                Description = "Watch all 2020 top imdb movies",
                Completed = false,
                GoalType = "yearly",
                UserId = 1
            };
            dbContext.Goals.Add(_goal2);
            dbContext.SaveChanges();
            var objCount = dbContext.Goals.Count();
            var id = _goal2.Id;
            //Act
            _goalRepo.DeleteGoal(id);
            _goalRepo.SaveChanges();
            //Assert
            Assert.Equal(objCount - 1, dbContext.Goals.Count());
        }
        [Fact]
        public void AddNullGoal_ThrowsException()
        {
            //Arrange
            Goal goal = null;
            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => _goalRepo.CreateGoal(goal));
        }
        [Fact]
        public void DeleteNullGoal_ThrowsException()
        {
            //Arrange
            Goal goal = new Goal();
            //Act&Assert
            Assert.Throws<ArgumentNullException>(() => _goalRepo.DeleteGoal(goal.Id));
        }
        [Fact]
        public void GetGoalsWithInvalidUserIdTest()
        {
            //Arrange
            //DB is empty so any id will be invalid
            //Act
            var result = _goalRepo.GetAllGoalsById(1);
            //Assert
            Assert.Empty(result);
        }
        [Fact]
        public void GetGoalsByUserId_ReturnsTheCorrectTypeTest()
        {
            var _goal2 = new Goal
            {
                Title = "Watch all 2020 movies",
                Description = "Watch all 2020 top imdb movies",
                Completed = false,
                GoalType = "yearly",
                UserId = 1
            };
            dbContext.Goals.Add(_goal2);
            dbContext.SaveChanges();

            var userId = _goal2.UserId;
            //Act
            var result = _goalRepo.GetAllGoalsById(userId);
            //Assert
            Assert.IsAssignableFrom<IEnumerable<Goal>>(result);
        }
        [Fact]
        public void GetGoalsByCompletion_ReturnsTheCorrectTypeTest()
        {
            var _goal2 = new Goal
            {
                Title = "Watch all 2020 movies",
                Description = "Watch all 2020 top imdb movies",
                Completed = false,
                GoalType = "yearly",
                UserId = 1
            };
            dbContext.Goals.Add(_goal2);
            dbContext.SaveChanges();

            var completed = _goal2.Completed;
            //Act
            var result = _goalRepo.GetAllGoalsByCompletion(completed);
            //Assert
            Assert.IsAssignableFrom<IEnumerable<Goal>>(result);
        }
        [Fact]
        public void GetGoalsByGoalType_ReturnsTheCorrectTypeTest()
        {
            var _goal2 = new Goal
            {
                Title = "Watch all 2020 movies",
                Description = "Watch all 2020 top imdb movies",
                Completed = false,
                GoalType = "yearly",
                UserId = 1
            };
            dbContext.Goals.Add(_goal2);
            dbContext.SaveChanges();

            var type = _goal2.GoalType;
            //Act
            var result = _goalRepo.GetAllGoalsByType(type);
            //Assert
            Assert.IsAssignableFrom<IEnumerable<Goal>>(result);
        }
        [Fact]
        public void GetGoalsWithInvalidCompletedTest()
        {
            //Arrange
            //DB is empty so any result will be empty
            //Act
            var result = _goalRepo.GetAllGoalsByCompletion(false);
            //Assert
            Assert.Empty(result);
        }
        [Fact]
        public void GetGoalsWithInvalidGoalTest()
        {
            //Arrange
            //DB is empty so any result will be empty
            //Act
            var result = _goalRepo.GetAllGoalsByType("Life");
            //Assert
            Assert.Empty(result);
        }
        [Fact]
        public void GetCountOfAllGoalsTest()
        {
            //Arrange
            var _goal2 = new Goal
            {
                Title = "Eat pizza",
                Description = "Eat a large pizza all by myself",
                Completed = false,
                GoalType = "life",
                UserId = 2
            };
            dbContext.Goals.Add(_goal2);
            dbContext.SaveChanges();
            //Act
            var allGoalsCount = _goalRepo.GetCountOfAllGoals(_goal2.UserId);
            //Assert
            Assert.Equal(1, allGoalsCount);
        }
        [Fact]
        public void GetCompleteGoalsCountTest()
        {
            //Arrange
            var _goal1 = new Goal
            {
                Title = "Eat pizza",
                Description = "Eat a large pizza all by myself",
                Completed = true,
                GoalType = "life",
                UserId = 2
            };
            dbContext.Goals.Add(_goal1);
            dbContext.SaveChanges();
            var _goal2 = new Goal
            {
                Title = "Watch all 2020 movies",
                Description = "Watch all 2020 top imdb movies",
                Completed = false,
                GoalType = "yearly",
                UserId = 2
            };
            dbContext.Goals.Add(_goal2);
            dbContext.SaveChanges();
            //Act
            var completedGoalsCount = _goalRepo.GetCompletedGoalsCount(2);
            //Assert
            Assert.Equal(1, completedGoalsCount);
        }
        [Fact]
        public void GetIncompletedGoalsCountTest()
        {
            var _goal2 = new Goal
            {
                Id = 1,
                Title = "Watch all 2020 movies",
                Description = "Watch all 2020 top imdb movies",
                Completed = false,
                GoalType = "yearly",
                UserId = 1
            };
            dbContext.Goals.Add(_goal2);
            dbContext.SaveChanges();
            var _goal3 = new Goal
            {
                Id = 2,
                Title = "Watch the top 100 imdb movies",
                Description = "Watch all the classics and the new movies in the top 100 imdb",
                Completed = true,
                GoalType = "life",
                UserId = 1
            };
            dbContext.Goals.Add(_goal3);
            dbContext.SaveChanges();
            //Act
            var incompletedGoalsCount = _goalRepo.GetIncompletedGoalsCount(1);
            //Assert
            Assert.Equal(1, incompletedGoalsCount);

        }
    }
}
