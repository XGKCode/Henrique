using Autuas.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AutuasTests.DtosTest
{
    public class GoalUpdateDtoTest
    {
        private GoalUpdateDto _goal;
        public GoalUpdateDtoTest()
        {
            _goal = new GoalUpdateDto()
            {
                Title = "Learn Vue",
                Description = "Learn Vue and create a project with it",
                Completed = false,
                GoalType = "yearly"
            };
        }
        [Fact]
        public void TestTitleType()
        {
            Assert.IsType<string>(_goal.Title);
        }
        [Fact]
        public void TestDescriptionType()
        {
            Assert.IsType<string>(_goal.Description);
        }
        [Fact]
        public void TestCompletedType()
        {
            Assert.IsType<bool>(_goal.Completed);
        }
        [Fact]
        public void TestGoalType()
        {
            Assert.IsType<string>(_goal.GoalType);
        }
        [Fact]
        public void TestGetTitle()
        {
            Assert.Equal("Learn Vue", _goal.Title);
        }
        [Fact]
        public void TestSetTitle()
        {
            _goal.Title = "Meditate";
            Assert.Equal("Meditate", _goal.Title);
        }
        [Fact]
        public void TestGetDescription()
        {
            Assert.Equal("Learn Vue and create a project with it", _goal.Description);
        }
        [Fact]
        public void TestSetDescription()
        {
            _goal.Description = "Learn Vue";
            Assert.Equal("Learn Vue", _goal.Description);
        }
        [Fact]
        public void TestGetCompleted()
        {
            Assert.False(_goal.Completed);
        }
        [Fact]
        public void TestSetCompleted()
        {
            _goal.Completed = true;
            Assert.True(_goal.Completed);
        }
        [Fact]
        public void TestGetGoalType()
        {
            Assert.Equal("yearly", _goal.GoalType);
        }
        [Fact]
        public void TestSetGoalType()
        {
            _goal.GoalType = "daily";
            Assert.Equal("daily", _goal.GoalType);
        }

    }
}
