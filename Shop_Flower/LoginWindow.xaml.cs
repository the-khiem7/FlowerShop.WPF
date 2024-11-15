using Shop_Flower.BLL;
using Shop_Flower.BLL.Services;
using Shop_Flower.DAL;
using Shop_Flower.DAL.Entities;
using Shop_Flower.DAL.Repository;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Shop_Flower
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private readonly UserService _userService;

     
        public Window1()
        {
            InitializeComponent();
            var context = new ShopContext();
            _userService = new UserService(new UserRepository(context));

          
        }

        private void Login_btn_Click(object sender, RoutedEventArgs e)
        {
            String email = Username_txt.Text.Trim();
            String password = Password_txt.Text.Trim();
            User user = _userService.getUserbyEmailAndPassword(email, password);
            if (user != null)
            {
                if (user.Role == 1)
                {
                    AdminManagementWindow adminManagementWindow = new AdminManagementWindow();
                    adminManagementWindow.Show();
                    this.Close();
                }
                else if (user.Role == 2)
                {
                    FlowerInfoRepository flowerInfoRepository = new FlowerInfoRepository(new ShopContext());
                    FlowerInfoService flowerInfoService = new FlowerInfoService(flowerInfoRepository);
                   
                    FlowerProductListing flowerProductListing = new FlowerProductListing(user.UserId);
                    flowerProductListing.Show();
                    this.Close();
                
                }
            }
            else
            {
                MessageBox.Show("Invalid Username or Password !!!");
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Username_txt_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
