using Shop_Flower.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Flower.DAL.Repository
{

    public interface IOrderRepository
{
    List<Order> GetAllOrders();
    Order GetOrder(int id);
    void AddOrder(Order order);
    void UpdateOrder(Order order);
    void DeleteOrder(int orderId);
}

}
