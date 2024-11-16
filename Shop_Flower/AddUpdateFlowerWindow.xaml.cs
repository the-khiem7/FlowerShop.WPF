using Microsoft.Win32;
using Shop_Flower.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Shop_Flower
{
    public partial class AddUpdateFlowerWindow : Window
    {
        public string FlowerName => FlowerNameTextBox.Text;
        public string Description => DescriptionTextBox.Text;
        public decimal Price => decimal.TryParse(PriceTextBox.Text, out decimal price) ? price : 0;
        public string ImageUrl => ImageUrlTextBox.Text;
        public int Quantity => int.TryParse(QuantityTextBox.Text, out int quantity) ? quantity : 0;
        public int CategoryId => (int)(CategoryComboBox.SelectedValue ?? 0);

        public AddUpdateFlowerWindow(IEnumerable<Category> categories, FlowerInfo flower = null)
        {
            InitializeComponent();
            CategoryComboBox.ItemsSource = categories;
            if (flower != null)
            {
                FlowerNameTextBox.Text = flower.FlowerName;
                DescriptionTextBox.Text = flower.FlowerDescription;
                PriceTextBox.Text = flower.Price.ToString();
                ImageUrlTextBox.Text = flower.ImageUrl;
                QuantityTextBox.Text = flower.AvailableQuantity.ToString();
                CategoryComboBox.SelectedValue = flower.CategoryId;
            }
        }

        private void BrowseImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                ImageUrlTextBox.Text = openFileDialog.FileName;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }


    }
}