using Autuas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autuas.Data
{
    public interface IUserRepo
    {
        User Authenticate(string username, string password);
        bool SaveChanges();
        IEnumerable<User> GetAllUsers();
        User GetUserByID(int id);
        void CreateUser(User newUser);
        User CreateUser(User newUser, string passowrd);
        void UpdateUser(User updateUser);
        void UpdateUser(User updateUser, string password = null);
        void DeleteUser(User deleteUser);
        void DeleteUser(int id);
    }
}
