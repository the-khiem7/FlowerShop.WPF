using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.VisualBasic.ApplicationServices;
using Shop_Flower.BLL;
using Shop_Flower.BLL.Services;
using Shop_Flower.DAL;
using Shop_Flower.DAL.Entities;
using Shop_Flower.DAL.Repository;

namespace Shop_Flower
{
    public partial class UserWindow : Window
    {
        private readonly FlowerInfoService _flowerInfoService;
        private List<FlowerInfo> _flowerList;
        private int _userId; // Đảm bảo UserId được truyền vào
        private decimal _totalPrice = 0;
        private readonly ICartService _cartService;
        public UserWindow() // Nhận UserId khi khởi tạo
        {
            InitializeComponent();

            var context = new ShopContext();
            _flowerInfoService = new FlowerInfoService(new FlowerInfoRepository(context));
            _cartService = new CartService(new CartRepository());
            LoadFlowers();
            UpdateTotalPrice();
        }

        private void LoadFlowers()
        {
            FlowerDataGrid.ItemsSource = _flowerInfoService.GetAllFlowers();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = SearchTextBox.Text.ToLower();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var filteredUsers = _flowerInfoService.GetAllFlowers()
                    .Where(user => user.FlowerName.ToLower().Contains(searchTerm))
                    .ToList();

                FlowerDataGrid.ItemsSource = filteredUsers;
            }
            else
            {

                LoadFlowers();
            }
        }
        private void OpenCartButton_Click(object sender, RoutedEventArgs e)
        {
            var cartWindow = new CartWindow(_cartService, _userId, _totalPrice);
            cartWindow.Show();
        }


        private void AddToCartButton_Click(object sender, RoutedEventArgs e)
        {

            if (sender is Button button && button.DataContext is FlowerInfo selectedFlower)
            {
                var item = new CartItem
                {
                    FlowerId = selectedFlower.FlowerId,
                    FlowerName = selectedFlower.FlowerName,
                    Price = selectedFlower.Price,
                    ImageUrl = selectedFlower.ImageUrl,
                    Quantity = 1
                };
                _cartService.AddToCart(item);
                MessageBox.Show($"{selectedFlower.FlowerName} đã được thêm vào giỏ hàng.");
            }
        }

        private void UpdateTotalPrice()
        {
            TotalPriceTextBlock.Text = _totalPrice.ToString("C");
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            // Truyền UserId và TotalPrice vào OrderWindow
            var orderWindow = new OrderWindow(_userId, _totalPrice);
            orderWindow.Show();
            this.Close();
        }
        private void OpenOrderHistory_Click(object sender, RoutedEventArgs e)
        {
           
            var orderHistoryWindow = new OrderHistoryWindow(_userId);
            orderHistoryWindow.Show();
        }

    }
}
