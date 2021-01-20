using Autuas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autuas.Data
{
    public interface IGoalRepo
    {
        void CreateGoal(Goal goal);
        IEnumerable<Goal> GetAllGoals();
        IEnumerable<Goal> GetAllGoalsById(int userId);
        IEnumerable<Goal> GetAllGoalsByType(string goalType);
        IEnumerable<Goal> GetAllGoalsByCompletion(bool completed);
        Goal GetGoalById(int id);
        void UpdateGoal(Goal goal);
        void DeleteGoal(int id);
        bool SaveChanges();

        //for statistics
        int GetCountOfAllGoals(int userId);
        int GetCompletedGoalsCount(int userId);
        int GetIncompletedGoalsCount(int userId);

    }
}
