﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Flower.DAL.Repository
{
<<<<<<< Updated upstream
    internal interface IOrderRepository
    {
    }
=======
    public interface IOrderRepository
{
    List<Order> GetAllOrders();
    Order GetOrder(int id);
    void AddOrder(Order room);
    void UpdateOrder(Order room);
    void DeleteOrder(int roomId);
}
>>>>>>> Stashed changes
}
