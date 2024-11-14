using Shop_Flower.BLL.Services;
using Shop_Flower.DAL;
using Shop_Flower.DAL.Entities;
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
    public partial class FlowerProductListing : Window
    {
        private readonly FlowerInfoService _flowerInfoService;
        private List<FlowerInfo> _flowerList;
        private readonly int _userId;
        public FlowerProductListing()
        {
            InitializeComponent();
            var context = new ShopContext();
            _flowerInfoService = new FlowerInfoService(new FlowerInfoRepository(context));
            LoadFlowers();
        }

        private void LoadFlowers()
        {
            FlowerProductList.ItemsSource = _flowerInfoService.GetAllFlowers();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnFlowerDetail_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnMyOrder_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
