using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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
        private readonly int _userId; // Đảm bảo UserId được truyền vào
        private decimal _totalPrice = 0;

        public UserWindow(int userId) // Nhận UserId khi khởi tạo
        {
            InitializeComponent();
            _userId = userId; // Lưu UserId
            var context = new ShopContext();
            _flowerInfoService = new FlowerInfoService(new FlowerInfoRepository(context));
            LoadFlowers();
            UpdateTotalPrice();
        }

        public UserWindow()
        {
        }

        private void LoadFlowers()
        {
            FlowerDataGrid.ItemsSource = _flowerInfoService.GetAllFlowers();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var searchQuery = SearchTextBox.Text.ToLower();
            var filteredFlowers = _flowerList
                .Where(f => f.FlowerName.ToLower().Contains(searchQuery))
                .ToList();
            FlowerDataGrid.ItemsSource = filteredFlowers;
        }

        private void AddToCartButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is FlowerInfo selectedFlower)
            {
                _totalPrice += selectedFlower.Price;
                UpdateTotalPrice();

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
    }
}
