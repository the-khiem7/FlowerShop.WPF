using Shop_Flower.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Flower.DAL.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ShopContext _context;

        public UserRepository(ShopContext context)
        {
            _context = context;
        }

        public User? GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.UserId == id);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            var existingUser = GetUserById(user.UserId);
            if (existingUser != null)
            {
                existingUser.Username = user.Username;
                existingUser.Password = user.Password;
                existingUser.Email = user.Email;
                existingUser.Role = user.Role;
                _context.SaveChanges();
            }
        }

        public void DeleteUser(int id)
        {
            var user = GetUserById(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public User? getUserbyEmailAndPassword(string email, string password)
        {
            return _context.Users.FirstOrDefault(c => c.Username == email && c.Password == password);
        }
    }
}
