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
using Shop_Flower.BLL;
using Shop_Flower.BLL.Services;
using Shop_Flower.DAL.Entities;
using Shop_Flower.DAL.Repository;
namespace Shop_Flower
{
    /// <summary>
    /// Interaction logic for Cart.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        private readonly ICartService _cartService;
        private int _userId;
        private decimal _totalPrice;

        public CartWindow(ICartService cartService, int userid, decimal totalPrice)
        {
            InitializeComponent();
            _cartService = cartService;
            _userId = userid;
            _totalPrice = totalPrice;
            LoadCart();
        }

        private void LoadCart()
        {
            CartDataGrid.ItemsSource = null;
            CartDataGrid.ItemsSource = _cartService.GetCart().Items;
            TotalPriceTextBlock.Text = _cartService.GetCart().TotalPrice.ToString("C");
        }
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void RemoveFromCartButton_Click(object sender, RoutedEventArgs e)
        {
            if (CartDataGrid.SelectedItem is CartItem selectedItem)
            {
                _cartService.RemoveFromCart(selectedItem.FlowerId);
                LoadCart();
            }
            else
            {
                MessageBox.Show("Please select an item to remove.");
            }
        }

        private void ClearCartButton_Click(object sender, RoutedEventArgs e)
        {
            _cartService.ClearCart();
            LoadCart();
        }

        private void IncreaseQuantityButton_Click(object sender, RoutedEventArgs e)
        {
            if (CartDataGrid.SelectedItem is CartItem selectedItem)
            {
                int selectedIndex = CartDataGrid.SelectedIndex;
                selectedItem.Quantity++;
                LoadCart();
                CartDataGrid.SelectedIndex = selectedIndex;
            }
            else
            {
                MessageBox.Show("Please select an item to increase quantity.");
            }
        }

        private void DecreaseQuantityButton_Click(object sender, RoutedEventArgs e)
        {
            if (CartDataGrid.SelectedItem is CartItem selectedItem)
            {
                if (selectedItem.Quantity > 1)
                {
                    int selectedIndex = CartDataGrid.SelectedIndex;
                    selectedItem.Quantity--;
                    LoadCart();
                    CartDataGrid.SelectedIndex = selectedIndex;
                }
                else
                {
                    _cartService.RemoveFromCart(selectedItem.FlowerId);
                    LoadCart();
                }
            }
            else
            {
                MessageBox.Show("Please select an item to decrease quantity.");
            }
        }
        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            var orderWindow = new OrderWindow(_userId, _totalPrice, _cartService);
            orderWindow.Show();
            Close();
        }
    }
}
