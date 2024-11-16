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
        private readonly ICartService _cartService;
        private readonly FlowerInfoService _flowerInfoService;
        private List<FlowerInfo> _flowerList;
        private readonly int _userId;
        private int _totalPrice = 0;
        public FlowerProductListing(int userId)
        {
            _userId = userId;
            _cartService = new CartService(new CartRepository());
            InitializeComponent();
            var context = new ShopContext();
            _flowerInfoService = new FlowerInfoService(new FlowerInfoRepository(context));
            LoadFlowers();
        }

        private void LoadFlowers()
        {
            FlowerProductList.ItemsSource = null;
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
            if(sender is Button button && button.DataContext is FlowerInfo selectedFlower)
            {
                FlowerProductInfomation flowerProductInfomation = new FlowerProductInfomation(selectedFlower.FlowerId);
                flowerProductInfomation.ShowDialog();
            }
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scrollViewer = (ScrollViewer)sender;
            if (scrollViewer != null)
            {
                if (e.Delta > 0)
                {
                    scrollViewer.LineUp();
                }
                else
                {
                    scrollViewer.LineDown();
                }
                e.Handled = true;
            }
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1();
            window1.Show();
            this.Close();
        }

        private void btnCategory_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnMyCart_Click(object sender, RoutedEventArgs e)
        {
            var cartWindow = new CartWindow(_cartService);
            cartWindow.Show();
        }

        private void btnAddToCart_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is FlowerInfo selectedFlower)
            {
                var item = new CartItem
                {
                    FlowerId = selectedFlower.FlowerId,
                    FlowerName = selectedFlower.FlowerName,
                    Price = selectedFlower.Price,
                    Quantity = 1
                };
                _cartService.AddToCart(item);
                MessageBox.Show($"{selectedFlower.FlowerName} đã được thêm vào giỏ hàng.");
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            var result = _flowerInfoService.SearchFlower(FlowerNameSearch_txt.Text);
            FlowerProductList.ItemsSource = null;
            FlowerProductList.ItemsSource = result;
        }

        private void btnOrderHistory_Click(object sender, RoutedEventArgs e)
        {
            var orderHistoryWindow = new OrderHistoryWindow(_userId);
            orderHistoryWindow.Show();
        }
    }
}
