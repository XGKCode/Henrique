using Autuas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autuas.Data
{
    public interface IDailyCheckInRepo
    {
        bool SaveChanges();
        IEnumerable<DailyCheckIn> GetAllDailyCheckIns();
        IEnumerable<DailyCheckIn> GetDailyCheckInsByDate(DateTime date);
        IEnumerable<DailyCheckIn> GetDailyCheckInsByUserID(int userID);
        DailyCheckIn GetDailyCheckInById(int id);
        void CreateDailyCheckIn(DailyCheckIn newCheckIn);
        void UpdateDailyCheckIn(DailyCheckIn updatedCheckIn);
        void DeleteDailyCheckInById(int id);

        // for statistics
        int GetPositiveFeelingsCount(int userId);
        int GetNegativeFeelingsCount(int userId);

        //for recommended activities

    }
}
