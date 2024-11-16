using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.VisualBasic.ApplicationServices;
using Shop_Flower.BLL.Services;
using Shop_Flower.DAL;
using Shop_Flower.DAL.Entities;
using Shop_Flower.DAL.Repository;
namespace Shop_Flower
{
    /// <summary>
    /// Interaction logic for OderHistoryWindow.xaml
    /// </summary>
    public partial class OrderHistoryWindow : Window
    {
        private readonly IOrderServices _orderService;
        private int _userId;

        public OrderHistoryWindow(int userId)
        {
            var orderService = new OrderServices(new OrderRepository(new ShopContext()));
            InitializeComponent();
            _userId = userId;
            _orderService = orderService;
            LoadOrders();
        }

        private void LoadOrders()
        {
            var orders = _orderService.GetOrdersByUserId(_userId);
            if (orders == null || !orders.Any())
            {
                MessageBox.Show("No orders found for the user.");
            }
            OrderHistoryDataGrid.ItemsSource = orders;
        }

    }
}