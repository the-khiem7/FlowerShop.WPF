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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Shop_Flower.BLL.Services;
using Shop_Flower.DAL;
using Shop_Flower.DAL.Entities;
using Shop_Flower.DAL.Repository;
namespace Shop_Flower
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private  IFlowerInfoService _flowerInfoService;

        public AdminWindow()
        {
            InitializeComponent();
            // _flowerInfoService will be set later
        }

        // Method to set the FlowerInfoService after initialization
        public void SetFlowerInfoService(IFlowerInfoService flowerInfoService)
        {
            _flowerInfoService = flowerInfoService;
            LoadFlowers();
        }

        private void LoadFlowers()
        {
            FlowerDataGrid.ItemsSource = _flowerInfoService?.GetAllFlowers();
        }

        private void AddFlower_Click(object sender, RoutedEventArgs e)
        {
            var newFlower = new FlowerInfo
            {
                FlowerName = "New Flower",
                FlowerDescription = "Description",
                Price = 0,
                AvailableQuantity = 0,
                CategoryId = null
            };
            _flowerInfoService.AddFlower(newFlower);
            LoadFlowers();
        }

        private void UpdateFlower_Click(object sender, RoutedEventArgs e)
        {
            if (FlowerDataGrid.SelectedItem is FlowerInfo selectedFlower)
            {
                selectedFlower.FlowerName = "Updated Flower";
                _flowerInfoService.UpdateFlower(selectedFlower);
                LoadFlowers();
            }
        }

        private void DeleteFlower_Click(object sender, RoutedEventArgs e)
        {
            if (FlowerDataGrid.SelectedItem is FlowerInfo selectedFlower)
            {
                _flowerInfoService.DeleteFlower(selectedFlower.FlowerId);
                LoadFlowers();
            }
        }

    }
}
