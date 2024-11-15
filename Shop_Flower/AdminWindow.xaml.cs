using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Shop_Flower.BLL.Services;
using Shop_Flower.DAL.Entities;
using Shop_Flower.DAL.Repository;
using Shop_Flower.DAL;

namespace Shop_Flower
{
    public partial class AdminWindow : Window
    {
        private readonly FlowerInfoService flowerInfoService;
        private readonly CategoryService categoryService;
        private bool isUpdating = false;
        private FlowerInfo currentFlower = null;

        public AdminWindow()
        {
            InitializeComponent();
            var context = new ShopContext();
            flowerInfoService = new FlowerInfoService(new FlowerInfoRepository(context));
            categoryService = new CategoryService(new CategoryRepository(context));
            LoadFlower();
            LoadCategory();
        }

        public void LoadFlower()
        {
            FlowerDataGrid.ItemsSource = flowerInfoService.GetAllFlowers();
        }

        public void LoadCategory()
        {
            FlowerDataGrid.ItemsSource = flowerInfoService.GetAllFlowers();

        }



        private void AddFlower_Click(object sender, RoutedEventArgs e)
        {
            var categories = categoryService.GetCategories();
            var addFlowerWindow = new AddUpdateFlowerWindow(categories);

            if (addFlowerWindow.ShowDialog() == true)
            {
                var newFlower = new FlowerInfo
                {
                    FlowerName = addFlowerWindow.FlowerName,
                    FlowerDescription = addFlowerWindow.Description,
                    Price = addFlowerWindow.Price,
                    ImageUrl = addFlowerWindow.ImageUrl,
                    AvailableQuantity = addFlowerWindow.Quantity,
                    CategoryId = addFlowerWindow.CategoryId
                };

                flowerInfoService.AddFlower(newFlower);
                LoadFlower();
                MessageBox.Show("Flower added successfully.");
            }
        }


        private void UpdateFlower_Click(object sender, RoutedEventArgs e)
        {
            if (FlowerDataGrid.SelectedItem is FlowerInfo selectedFlower)
            {
                var categories = categoryService.GetCategories();
                var updateFlowerWindow = new AddUpdateFlowerWindow(categories, selectedFlower);

                if (updateFlowerWindow.ShowDialog() == true)
                {
                    selectedFlower.FlowerName = updateFlowerWindow.FlowerName;
                    selectedFlower.FlowerDescription = updateFlowerWindow.Description;
                    selectedFlower.Price = updateFlowerWindow.Price;
                    selectedFlower.ImageUrl = updateFlowerWindow.ImageUrl;
                    selectedFlower.AvailableQuantity = updateFlowerWindow.Quantity;
                    selectedFlower.CategoryId = updateFlowerWindow.CategoryId;

                    flowerInfoService.UpdateFlower(selectedFlower);
                    LoadFlower();
                    MessageBox.Show("Flower updated successfully.");
                }
            }
            else
            {
                MessageBox.Show("Please select a flower to update.");
            }
        }


        private void DeleteFlower_Click(object sender, RoutedEventArgs e)
        {
            if (FlowerDataGrid.SelectedItem is FlowerInfo selectedFlower)
            {
                var result = MessageBox.Show($"Are you sure you want to delete the flower '{selectedFlower.FlowerName}'?", "Delete Flower", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    flowerInfoService.DeleteFlower(selectedFlower.FlowerId);
                    LoadFlower();
                    MessageBox.Show("Flower deleted successfully.");
                }
            }
            else
            {
                MessageBox.Show("Please select a flower to delete.");
            }
        }

        private void BrowseImage_Click(object sender, RoutedEventArgs e)
        {
            bool wasOpen = FlowerPopup.IsOpen;
            FlowerPopup.StaysOpen = true;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                PopupImageUrl.Text = openFileDialog.FileName;
            }
            FlowerPopup.StaysOpen = false;
        }

        private void PopupSave_Click(object sender, RoutedEventArgs e)
        {
            string name = PopupFlowerName.Text;
            string description = PopupFlowerDescription.Text;
            string priceText = PopupFlowerPrice.Text;
            string imageUrl = PopupImageUrl.Text;
            string quantityText = PopupFlowerQuantity.Text;
            string categoryText = PopupFlowerCategory.Text;

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(priceText) || string.IsNullOrWhiteSpace(imageUrl))
            {
                MessageBox.Show("Name, Price, and Image URL are required.");
                return;
            }

            if (!decimal.TryParse(priceText, out decimal price) || !int.TryParse(quantityText, out int quantity) || !int.TryParse(categoryText, out int categoryId))
            {
                MessageBox.Show("Invalid data for Price, Quantity, or Category.");
                return;
            }

            if (isUpdating)
            {
                currentFlower.FlowerName = name;
                currentFlower.FlowerDescription = description;
                currentFlower.Price = price;
                currentFlower.ImageUrl = imageUrl;
                currentFlower.AvailableQuantity = quantity;
                currentFlower.CategoryId = categoryId;
                flowerInfoService.UpdateFlower(currentFlower);
                MessageBox.Show("Flower updated successfully.");
            }
            else
            {
                var newFlower = new FlowerInfo
                {
                    FlowerName = name,
                    FlowerDescription = description,
                    Price = price,
                    ImageUrl = imageUrl,
                    AvailableQuantity = quantity,
                    CategoryId = categoryId
                };
                flowerInfoService.AddFlower(newFlower);
                MessageBox.Show("Flower added successfully.");
            }

            LoadFlower();
            FlowerPopup.IsOpen = false;
        }

        private void PopupCancel_Click(object sender, RoutedEventArgs e)
        {
            FlowerPopup.IsOpen = false;
        }

        private void ClearPopupFields()
        {
            PopupFlowerName.Text = string.Empty;
            PopupFlowerDescription.Text = string.Empty;
            PopupFlowerPrice.Text = string.Empty;
            PopupImageUrl.Text = string.Empty;
            PopupFlowerQuantity.Text = string.Empty;
            PopupFlowerCategory.Text = string.Empty;
        }

        private void FillPopupFields(FlowerInfo flower)
        {
            PopupFlowerName.Text = flower.FlowerName;
            PopupFlowerDescription.Text = flower.FlowerDescription;
            PopupFlowerPrice.Text = flower.Price.ToString();
            PopupImageUrl.Text = flower.ImageUrl;
            PopupFlowerQuantity.Text = flower.AvailableQuantity.ToString();
            PopupFlowerCategory.Text = flower.CategoryId.ToString();
        }

        private void ImageLink_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is TextBlock textBlock)
            {
                string imagePath = textBlock.Text;
                if (System.IO.File.Exists(imagePath))
                {
                    ImageViewerWindow imageViewer = new ImageViewerWindow(imagePath);
                    imageViewer.Show();
                }
                else
                {
                    MessageBox.Show("Image file not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
