using Shop_Flower.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Flower.DAL.Repository
{

    public class OrderRepository : IOrderRepository
{
    private readonly ShopContext _context;

    public OrderRepository(ShopContext context)
    {
        _context = context;
    }
    public List<Order> GetAllOrders()
    {
        return _context.Orders.ToList();
    }
    public Order GetOrder(int id) {
        {
            return _context.Orders.FirstOrDefault(Order => Order.OrderId == id);
        }
    }
    public void AddOrder(Order order) 
    {
        _context.Orders.Add(order);
        _context.SaveChanges();
    }
    public void DeleteOrder(int id)
    {
        var OrderDeleted = _context.Orders.Find(id);
        if (OrderDeleted != null) 
        {
           _context.Orders.Remove(OrderDeleted);
        }
        _context.SaveChanges();
    }
    public void UpdateOrder(Order order)
    {
        _context.Orders.Update(order);
        _context.SaveChanges();

    }
}
