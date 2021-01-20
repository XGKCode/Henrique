using Autuas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autuas.Data
{
    public interface IReflectiveDiaryLogRepo
    {
        ReflectiveDiaryLog GetReflectiveDiaryLogById(int id);
        IEnumerable<ReflectiveDiaryLog> GetReflectiveDiaryLogsByUserId(int userId);
        IEnumerable<ReflectiveDiaryLog> GetAllReflectiveDiaryLogs();
        void CreateReflectiveDiaryLog(ReflectiveDiaryLog log);
        void UpdateDiaryLog(ReflectiveDiaryLog log);
        void DeleteDiaryLog(int id);
        bool SaveChanges();
    }
}
