using Autuas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autuas.Data
{
    public class SQLGoalRepo : IGoalRepo
    {
        private readonly ApplicationContext _context;
        public SQLGoalRepo(ApplicationContext context)
        {
            _context = context;
        }
        public void CreateGoal(Goal goal)
        {
            if (goal == null)
            {
                throw new ArgumentNullException(nameof(goal));
            }
            _context.Goals.Add(goal);
            _context.SaveChanges();
        }

        public void DeleteGoal(int id)
        {
            var goal = _context.Goals.Find(id);
            if (goal == null)
            {
                throw new ArgumentNullException(nameof(goal));
            }
            _context.Goals.Remove(goal);
            _context.SaveChanges();
        }

        public IEnumerable<Goal> GetAllGoals()
        {
            return _context.Goals.ToList();
        }

        public IEnumerable<Goal> GetAllGoalsByCompletion(bool completed)
        {
            return _context.Goals.Where(p => p.Completed == completed).ToList();
        }

        public IEnumerable<Goal> GetAllGoalsByType(string goalType)
        {
            return _context.Goals.Where(p => p.GoalType.Equals(goalType)).ToList();
        }

        public IEnumerable<Goal> GetAllGoalsById(int userId)
        {
            return _context.Goals.Where(p => p.UserId == userId).ToList();
        }

        public Goal GetGoalById(int id)
        {
            return _context.Goals.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        public void UpdateGoal(Goal goal)
        {
            // The update is being taken care of by the DBContext, however since this class implements the IUser interface we need to implement it
            // Nothing :) 
        }
        public int GetCountOfAllGoals(int userId)
        {
            return _context.Goals.Count(p => p.UserId == userId);

        }

        public int GetCompletedGoalsCount(int userId)
        {
            return _context.Goals.Count(p => p.Completed == true && p.UserId == userId);
        }

        public int GetIncompletedGoalsCount(int userId)
        {
            return _context.Goals.Count(p => p.Completed == false && p.UserId == userId);
        }
    }
}
