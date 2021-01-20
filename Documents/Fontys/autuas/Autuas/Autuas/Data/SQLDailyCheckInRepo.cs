using Autuas.Enums;
using Autuas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autuas.Data
{
    public class SQLDailyCheckInRepo : IDailyCheckInRepo
    {
        private readonly ApplicationContext _context;

        public SQLDailyCheckInRepo(ApplicationContext context)
        {
            _context = context;
        }
        public void CreateDailyCheckIn(DailyCheckIn newCheckIn)
        {
            if (newCheckIn == null)
            {
                throw new ArgumentNullException(nameof(newCheckIn));
            }
            _context.DailyCheckIns.Add(newCheckIn);

        }

        public void DeleteDailyCheckInById(int id)
        {
            var checkIn = _context.DailyCheckIns.Find(id);
            if (checkIn == null)
            {
                throw new ArgumentNullException(nameof(checkIn));
            }
            _context.DailyCheckIns.Remove(checkIn);
            _context.SaveChanges();
        }

        public IEnumerable<DailyCheckIn> GetAllDailyCheckIns()
        {
            return _context.DailyCheckIns.ToList();
        }

        public DailyCheckIn GetDailyCheckInById(int id)
        {
            return _context.DailyCheckIns.FirstOrDefault(p => p.ID == id);

        }

        public IEnumerable<DailyCheckIn> GetDailyCheckInsByDate(DateTime date)
        {
            return _context.DailyCheckIns.Where(p => p.Date == date).ToList();
        }

        public IEnumerable<DailyCheckIn> GetDailyCheckInsByUserID(int userID)
        {
            return _context.DailyCheckIns.Where(p => p.UserID == userID).ToList();
        }
        //For statistics
        public int GetNegativeFeelingsCount(int userId)
        {
            return _context.DailyCheckIns.Count(p => p.NegativeFeelings != null && p.UserID == userId);
        }


        public int GetPositiveFeelingsCount(int userId)
        {
            return _context.DailyCheckIns.Count(p => p.PositiveFeelings != null && p.UserID == userId);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        public void UpdateDailyCheckIn(DailyCheckIn updatedCheckIn)
        {
            // The update is being taken care of by the DBContext, however since this class implements the IUser interface we need to implement it
            // Nothing :) 
        }

    }
}
