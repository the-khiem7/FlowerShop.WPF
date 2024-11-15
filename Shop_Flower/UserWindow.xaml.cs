using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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
        private readonly int _userId; // Đảm bảo UserId được truyền vào
        private decimal _totalPrice = 0;

        public UserWindow(int userId)
        {
            InitializeComponent();
            _userId = userId; 

            var context = new ShopContext();
            _flowerInfoService = new FlowerInfoService(new FlowerInfoRepository(context));
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

        private void AddToCartButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is FlowerInfo selectedFlower)
            {
                if (selectedFlower.AvailableQuantity > 0)
                {
                    
                    int newQuantity = selectedFlower.AvailableQuantity - 1;
                    _flowerInfoService.UpdateFlowerQuantity(selectedFlower.FlowerId, newQuantity);

                    
                    _totalPrice += selectedFlower.Price;
                    UpdateTotalPrice();

                  
                    MessageBox.Show($"{selectedFlower.FlowerName} đã được thêm vào giỏ hàng.");

                    LoadFlowers();
                }
                else
                {
                    MessageBox.Show($"Hoa {selectedFlower.FlowerName} đã hết hàng.");
                }
            }
        }

        private void UpdateTotalPrice()
        {
            TotalPriceTextBlock.Text = _totalPrice.ToString("C");
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {

            var orderWindow = new OrderWindow(_userId, _totalPrice);
            orderWindow.ShowDialog();
        }
    }
}
