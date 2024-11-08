using Shop_Flower.BLL.Services;
using Shop_Flower.DAL.Entities;
using System;
using System.Windows;

namespace Shop_Flower
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        private readonly OrderServices _orderServices;

        public OrderWindow(OrderServices orderServices, int orderId)
        {
            InitializeComponent();
            _orderServices = orderServices;
            LoadOrderData(orderId);
        }

        private void LoadOrderData(int orderId)
        {
            Order order = _orderServices.GetOrder(orderId);
            if (order != null)
            {
                OrderIdTextBlock.Text = order.OrderId.ToString();
                UserIdTextBlock.Text = order.UserId?.ToString();
                PhoneNumberTextBlock.Text = order.PhoneNumber;
                PaymentMethodTextBlock.Text = order.PaymentMethod;
                DeliveryMethodTextBlock.Text = order.DeliveryMethod;
                CreatedDateTextBlock.Text = order.CreatedDate?.ToString("g");
                AddressIdTextBlock.Text = order.AddressId;
                TotalPriceTextBlock.Text = order.TotalPrice?.ToString("C");
            }
        }
    }
}
