using Shop_Flower.DAL.Entities;
using Shop_Flower.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Flower.BLL.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly OrderRepository _orderRepository;
        public OrderServices(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public List<Order> GetAllOrders()
        {
            return _orderRepository.GetAllOrders();
        }
        public Order GetOrder(int id)
        {
            return _orderRepository.GetOrder(id);
        }
        public void UpdateOrder(Order order)
        {
            _orderRepository.UpdateOrder(order);
        }
        public void DeleteOrder(int id)
        {
            _orderRepository.DeleteOrder(id);
        }
        public void AddOrder(Order order) 
        {
            _orderRepository.AddOrder(order);
        }
    }
}
