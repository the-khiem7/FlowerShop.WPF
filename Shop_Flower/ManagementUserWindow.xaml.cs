using Shop_Flower.BLL;
using Shop_Flower.DAL;
using Shop_Flower.DAL.Entities;
using Shop_Flower.DAL.Repository;
using System.Windows;

namespace Shop_Flower
{
    public partial class ManagementUserWindow : Window
    {
        private readonly UserService userService;
        private bool isUpdating = false; 
        private User updatingUser = null; 

        public ManagementUserWindow()
        {
            InitializeComponent();
            var context = new ShopContext();
            userService = new UserService(new UserRepository(context));
            LoadUser();
        }

        public void LoadUser()
        {
            UserDataGrid.ItemsSource = userService.GetAllUsers();
        }

        private void AddUsers_Click(object sender, RoutedEventArgs e)
        {
            isUpdating = false;
            ClearPopupFields();
            PopupRole.IsEnabled = false; 
            PopupRole.Text = "2"; 
            UserPopup.IsOpen = true;
        }

        private void UpdateUser_Click(object sender, RoutedEventArgs e)
        {
            if (UserDataGrid.SelectedItem is User selectedUser)
            {
                isUpdating = true;
                updatingUser = selectedUser;
                FillPopupFields(selectedUser);
                PopupRole.IsEnabled = false; 
                UserPopup.IsOpen = true;
            }
            else
            {
                MessageBox.Show("Please select a user to update.");
            }
        }

        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (UserDataGrid.SelectedItem is User selectedUser)
            {
                var result = MessageBox.Show($"Are you sure you want to delete user '{selectedUser.Username}'?", "Delete User", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    userService.DeleteUser(selectedUser.UserId);
                    LoadUser();
                    MessageBox.Show("User deleted successfully.");
                }
            }
            else
            {
                MessageBox.Show("Please select a user to delete.");
            }
        }

        private void PopupSave_Click(object sender, RoutedEventArgs e)
        {
            string username = PopupUsername.Text;
            string password = PopupPassword.Text;
            string email = PopupEmail.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("All fields must be filled.");
                return;
            }

            if (isUpdating)
            {
                updatingUser.Username = username;
                updatingUser.Password = password;
                updatingUser.Email = email;
                userService.UpdateUser(updatingUser);
                MessageBox.Show("User updated successfully.");
            }
            else
            {
                var newUser = new User { Username = username, Password = password, Email = email, Role = 2 };
                userService.AddUser(newUser);
                MessageBox.Show("User added successfully.");
            }

            LoadUser();
            UserPopup.IsOpen = false;
        }

        private void PopupCancel_Click(object sender, RoutedEventArgs e)
        {
            UserPopup.IsOpen = false;
        }

        private void ClearPopupFields()
        {
            PopupUsername.Text = string.Empty;
            PopupPassword.Text = string.Empty;
            PopupEmail.Text = string.Empty;
        }

        private void FillPopupFields(User user)
        {
            PopupUsername.Text = user.Username;
            PopupPassword.Text = user.Password;
            PopupEmail.Text = user.Email;
            PopupRole.Text = user.Role.ToString();
        }

        private void SearchUser_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = SearchTextBox.Text.ToLower();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var filteredUsers = userService.GetAllUsers()
                    .Where(user => user.Username.ToLower().Contains(searchTerm))
                    .ToList();

                UserDataGrid.ItemsSource = filteredUsers;
            }
            else
            {
                
                LoadUser();
            }
        }
    }
}
