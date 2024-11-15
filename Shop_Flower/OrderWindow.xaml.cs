using System;
using System.Windows;
using System.Windows.Controls;
using Shop_Flower.BLL.Services;
using Shop_Flower.DAL;
using Shop_Flower.DAL.Entities;
using Shop_Flower.DAL.Repository;

namespace Shop_Flower
{
    public partial class OrderWindow : Window
    {
       
        private readonly decimal _totalPrice;
        private readonly OrderServices _orderServices;

        public OrderWindow(int userId, decimal totalPrice)
        {
            InitializeComponent();
            
            _totalPrice = totalPrice;
            TotalPriceTextBlock.Text = _totalPrice.ToString("C");

            // Khởi tạo OrderServices để lưu đơn hàng
            var context = new ShopContext();
            _orderServices = new OrderServices(new OrderRepository(context));
        }

        private void ConfirmOrderButton_Click(object sender, RoutedEventArgs e)
        {
            // Lấy thông tin từ các trường nhập liệu
            string phoneNumber = PhoneNumberTextBox.Text;
            string paymentMethod = (PaymentMethodComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            string deliveryMethod = (DeliveryMethodComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            string addressId = AddressIdTextBox.Text;

            // Kiểm tra các trường nhập liệu
            if (string.IsNullOrWhiteSpace(phoneNumber) || string.IsNullOrWhiteSpace(paymentMethod) ||
                string.IsNullOrWhiteSpace(deliveryMethod) || string.IsNullOrWhiteSpace(addressId))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Tạo đối tượng Order và lưu vào cơ sở dữ liệu
            var newOrder = new Order
            {
               
                PhoneNumber = phoneNumber,
                PaymentMethod = paymentMethod,
                DeliveryMethod = deliveryMethod,
                AddressId = addressId,
                TotalPrice = _totalPrice,
                CreatedDate = DateTime.Now
            };

            try
            {
                _orderServices.AddOrder(newOrder);
                MessageBox.Show("Order confirmed and saved! Total: " + _totalPrice.ToString("C"));
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving order: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
