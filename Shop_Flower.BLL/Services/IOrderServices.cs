using Shop_Flower.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Flower.BLL.Services
{
    public interface IOrderServices
    {
        List<Order> GetAllOrders();
        Order GetOrder(int id);
        void AddOrder(Order room);
        void UpdateOrder(Order room);
        void DeleteOrder(int roomId);
    }
}
