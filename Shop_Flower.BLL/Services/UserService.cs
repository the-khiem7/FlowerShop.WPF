using Shop_Flower.DAL.Entities;
using Shop_Flower.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Flower.BLL
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User GetUserById(int id)
        {
            return _userRepository.GetUserById(id);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }

        public void AddUser(User user)
        {
            _userRepository.AddUser(user);
        }

        public void UpdateUser(User user)
        {
            _userRepository.UpdateUser(user);
        }

        public void DeleteUser(int id)
        {
            _userRepository.DeleteUser(id);
        }
        public User getUserbyEmailAndPassword(string email, string password)
        {
            return _userRepository.getUserbyEmailAndPassword(email, password);
        }

        public IEnumerable<User> SearchUsersByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return new List<User>();

            return _userRepository.GetAllUsers()
                                  .Where(user => user.Username.Contains(username, System.StringComparison.OrdinalIgnoreCase))
                                  .ToList();
        }
    }
}