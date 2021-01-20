using Autuas.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autuas.Data
{
    public class ApplicationContext : DbContext
    {
        //define a constructor
        public ApplicationContext(DbContextOptions<ApplicationContext> opt) : base(opt) { }

        //create a database representation of the user model
        public DbSet<User> Users { get; set; }
        public DbSet<DailyCheckIn> DailyCheckIns { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<ReflectiveDiaryLog> DiaryLogs { get; set; }
    }
}
