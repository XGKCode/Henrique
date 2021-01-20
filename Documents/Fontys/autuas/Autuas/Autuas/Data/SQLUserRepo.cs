using Autuas.Helpers;
using Autuas.Models;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Autuas.Data
{
    public class SQLUserRepo : IUserRepo
    {
        private readonly ApplicationContext _context;

        public SQLUserRepo(ApplicationContext context)
        {
            _context = context;
        }

        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return null;
            }
            var user = _context.Users.SingleOrDefault(x => x.Username == username);
            //check if username exists
            if (user == null)
            {
                return null;
            }
            //check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }
            //authentication successful
            return user;
        }

        public void CreateUser(User newUser)
        {
            if (newUser == null)
            {
                throw new ArgumentNullException(nameof(newUser));
            }
            _context.Users.Add(newUser);
        }

        public User CreateUser(User newUser, string passowrd)
        {
            //validation
            if (string.IsNullOrEmpty(passowrd))
            {
                throw new AppException("Password is required!");
            }
            if (_context.Users.Any(x => x.Username == newUser.Username))
            {
                throw new AppException("Username \"" + newUser.Username + "\" is already taken");

            }
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(passowrd, out passwordHash, out passwordSalt);
            newUser.PasswordHash = passwordHash;
            newUser.PasswordSalt = passwordSalt;

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return newUser;
        }

        public void DeleteUser(User deleteUser)
        {
            if (deleteUser == null)
            {
                throw new ArgumentNullException(nameof(deleteUser));
            }
            _context.Users.Remove(deleteUser);

        }

        public void DeleteUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUserByID(int id)
        {
            return _context.Users.FirstOrDefault(p => p.UserID == id);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }


        public void UpdateUser(User updateUser)
        {
            // The update is being taken care of by the DBContext, however since this class implements the IUser interface we need to implement it
            // Nothing :) 
        }

        public void UpdateUser(User updateUser, string password = null)
        {
            var user = _context.Users.Find(updateUser.UserID);
            if (user == null)
            {
                throw new AppException("User not found");
            }
            //update username if it has changed
            if (!string.IsNullOrWhiteSpace(updateUser.Username) && updateUser.Username != user.Username)
            {
                // throw error if the new username is already taken
                if (_context.Users.Any(x => x.Username == updateUser.Username))
                    throw new AppException("Username " + updateUser.Username + " is already taken");

                user.Username = updateUser.Username;
            }
            //update name if provided
            if (!string.IsNullOrEmpty(updateUser.Name))
            {
                user.Name = updateUser.Name;
            }
            //update email if provided
            if (!string.IsNullOrEmpty(updateUser.Email))
            {
                user.Email = updateUser.Email;
            }
            // update password if provided
            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            _context.Users.Update(user);
            _context.SaveChanges();

        }
        // private helper methods

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        //using sha512 with salt
        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
