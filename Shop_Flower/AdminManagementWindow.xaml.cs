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

namespace Shop_Flower
{
    /// <summary>
    /// Interaction logic for AdminManagementWindow.xaml
    /// </summary>
    public partial class AdminManagementWindow : Window
    {
        public AdminManagementWindow()
        {
            InitializeComponent();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void ManageCategory_Click(object sender, RoutedEventArgs e)
        {
            ManagementCategoryWindow managementCategoryWindow = new ManagementCategoryWindow();
            managementCategoryWindow.ShowDialog();

        }

        private void ManageFlowers_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow managementWindow = new AdminWindow();
            managementWindow.ShowDialog();
        }

        private void ManageOrders_Click(object sender, RoutedEventArgs e)
        {
            ManagementOrderWindow managementOrderWindow = new ManagementOrderWindow();
            managementOrderWindow.ShowDialog();
        }

        private void ManageUsers_Click(object sender, RoutedEventArgs e)
        {
            ManagementUserWindow managementUserWindow = new ManagementUserWindow();
            managementUserWindow.ShowDialog();
        }
    }
}
