using Autuas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autuas.Data
{
    public class SQLReflectiveDiaryLogRepo : IReflectiveDiaryLogRepo
    {
        private readonly ApplicationContext _context;

        public SQLReflectiveDiaryLogRepo(ApplicationContext context)
        {
            _context = context;
        }

        public void CreateReflectiveDiaryLog(ReflectiveDiaryLog log)
        {
            if (log == null)
            {
                throw new ArgumentNullException();
            }
            _context.DiaryLogs.Add(log);
            _context.SaveChanges();
        }

        public void DeleteDiaryLog(int id)
        {
            var log = _context.DiaryLogs.Find(id);
            if (log == null)
            {
                throw new ArgumentNullException();
            }
            _context.DiaryLogs.Remove(log);
            _context.SaveChanges();
        }

        public IEnumerable<ReflectiveDiaryLog> GetAllReflectiveDiaryLogs()
        {
            return _context.DiaryLogs.ToList();
        }

        public ReflectiveDiaryLog GetReflectiveDiaryLogById(int id)
        {
            return _context.DiaryLogs.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<ReflectiveDiaryLog> GetReflectiveDiaryLogsByUserId(int userId)
        {
            return _context.DiaryLogs.Where(p => p.UserId == userId).ToList();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        public void UpdateDiaryLog(ReflectiveDiaryLog log)
        {
            // The update is being taken care of by the DBContext, however since this class implements the IUser interface we need to implement it
            // Nothing :) 
        }
    }
}
