using Shop_Flower.DAL.Entities;

namespace Shop_Flower.BLL
{
    public interface IUserService
    {
        User GetUserById(int id);
        IEnumerable<User> GetAllUsers();
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
    }
}