using Shop_Flower.BLL;
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

namespace Shop_Flower
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private readonly UserService _userService;
        public Window1(UserService userService)
        {
            InitializeComponent();
            _userService = userService;
        }

        private void Login_btn_Click(object sender, RoutedEventArgs e)
        {
            String username = Username_txt.Text.Trim();
            String password = Password_txt.Text.Trim();
            User user = _userService.getUserbyUsernameAndPassword(username, password);
            if (user != null)
            {

            }
            else
            {
                MessageBox.Show("Invalid Username or Password !!!");
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
