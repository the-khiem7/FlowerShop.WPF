using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Shop_Flower.BLL.Services;
using Shop_Flower.DAL.Entities;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;

namespace Shop_Flower
{
    public partial class UserWindow : Window
    {
        private readonly FlowerInfoService _flowerInfoService;
        private readonly CartService _cartService;
        private List<FlowerInfo> _flowerList;

        public UserWindow(FlowerInfoService flowerInfoService, CartService cartService)
        {
            InitializeComponent();
            _flowerInfoService = flowerInfoService;
            _cartService = cartService;
            LoadFlowers();
        }

        private void LoadFlowers()
        {
            _flowerList = _flowerInfoService.GetAllFlowers();
            FlowerItemsControl.ItemsSource = _flowerList;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var searchQuery = SearchTextBox.Text.ToLower();
            var filteredFlowers = _flowerList
                .Where(f => f.FlowerName.ToLower().Contains(searchQuery))
                .ToList();
            FlowerItemsControl.ItemsSource = filteredFlowers;
        }

        private void AddToCartButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is FlowerInfo selectedFlower)
            {
                var cartItem = new CartItem
                {
                    FlowerId = selectedFlower.FlowerId,
                    FlowerName = selectedFlower.FlowerName,
                    Price = selectedFlower.Price,
                    Quantity = 1
                };
                _cartService.AddToCart(cartItem);
                MessageBox.Show($"{selectedFlower.FlowerName} đã được thêm vào giỏ hàng.");
            }
        }

        private void ViewCartButton_Click(object sender, RoutedEventArgs e)
        {
            var cartWindow = new CartWindow(_cartService);
            cartWindow.Show();
        }
    }
}
