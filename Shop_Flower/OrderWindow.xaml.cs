using System;
using System.Linq;
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
        private readonly int _userId;
        private readonly OrderServices _orderServices;
        private readonly ICartService _cartService;

<<<<<<< Updated upstream
        // Modified constructor to include UserId
        public OrderWindow(int userId, decimal totalPrice)
=======
        public OrderWindow(int userId, decimal _totalPrice, ICartService cartService)
>>>>>>> Stashed changes
        {
            InitializeComponent();
            _userId = userId;
<<<<<<< Updated upstream
            _totalPrice = totalPrice;
            TotalPriceTextBlock.Text = _totalPrice.ToString("C");

=======
            _cartService = cartService;
            UpdateTotalPrice();
>>>>>>> Stashed changes
            var context = new ShopContext();
            _orderServices = new OrderServices(new OrderRepository(context));
            LoadCartQuantities();
        }

        private void UpdateTotalPrice()
        {
            var totalPrice = _cartService.GetCart().TotalPrice;
            TotalPriceTextBlock.Text = totalPrice.ToString("C");
        }

        private void ConfirmOrderButton_Click(object sender, RoutedEventArgs e)
        {
<<<<<<< Updated upstream

=======
>>>>>>> Stashed changes
            string phoneNumber = PhoneNumberTextBox.Text;
            string paymentMethod = (PaymentMethodComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            string deliveryMethod = (DeliveryMethodComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            string addressId = AddressIdTextBox.Text;

<<<<<<< Updated upstream
  
=======
>>>>>>> Stashed changes
            if (string.IsNullOrWhiteSpace(phoneNumber) || string.IsNullOrWhiteSpace(paymentMethod) ||
                string.IsNullOrWhiteSpace(deliveryMethod) || string.IsNullOrWhiteSpace(addressId))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

<<<<<<< Updated upstream
 
=======
            var totalPrice = _cartService.GetCart().TotalPrice;

>>>>>>> Stashed changes
            var newOrder = new Order
            {
                UserId = _userId,
                PhoneNumber = phoneNumber,
                PaymentMethod = paymentMethod,
                DeliveryMethod = deliveryMethod,
                AddressId = addressId,
                TotalPrice = totalPrice,
                CreatedDate = DateTime.Now
            };

            try
            {
                _orderServices.AddOrder(newOrder);
                UpdateFlowerQuantities();
                MessageBox.Show("Order confirmed and saved! Total: " + totalPrice.ToString("C"));
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving order: " + ex.Message + "\n\nInner Exception: " + ex.InnerException?.Message,
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void UpdateFlowerQuantities()
        {
            var cartItems = _cartService.GetCart().Items;

            using (var context = new ShopContext())
            {
                foreach (var item in cartItems)
                {
                    var flower = context.FlowerInfos.FirstOrDefault(f => f.FlowerId == item.FlowerId);

                    if (flower != null)
                    {
                        flower.AvailableQuantity -= item.Quantity;

                        if (flower.AvailableQuantity < 0)
                        {
                            MessageBox.Show($"Not enough stock for {flower.FlowerName}. Quantity set to 0.", "Stock Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                            flower.AvailableQuantity = 0;
                        }
                    }
                }

                context.SaveChanges();
            }
        }

        private void LoadCartQuantities()
        {
            var cartItems = _cartService.GetCart().Items;

            foreach (var item in cartItems)
            {
                Console.WriteLine($"Flower ID: {item.FlowerId}, Quantity: {item.Quantity}");
            }
        }

        private void ReturnMainPageButton_Click(object sender, RoutedEventArgs e)
        {
            var flowerProductListing = new FlowerProductListing(_userId);
            flowerProductListing.Show();
            this.Close();
        }
    }
}
