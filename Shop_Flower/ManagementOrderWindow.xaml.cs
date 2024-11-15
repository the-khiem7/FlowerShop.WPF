using Shop_Flower.BLL;
using Shop_Flower.BLL.Services;
using Shop_Flower.DAL;
using Shop_Flower.DAL.Repository;
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

namespace Shop_Flower
{
    /// <summary>
    /// Interaction logic for ManagementOrderWindow.xaml
    /// </summary>
    public partial class ManagementOrderWindow : Window
    {

        private readonly OrderServices orderServices;
        private readonly UserService userService;
        
        public ManagementOrderWindow()
        {
            InitializeComponent();
            var context = new ShopContext();
            orderServices = new OrderServices(new OrderRepository(context));  
            userService = new UserService(new UserRepository(context));
            LoadOrder();
        }

       

        private void LoadOrder()
        {
            OrderDataGrid.ItemsSource = orderServices.GetAllOrders();
        }
    }
}
