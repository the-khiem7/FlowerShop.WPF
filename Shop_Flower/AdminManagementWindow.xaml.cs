using Shop_Flower.BLL.Services;
using Shop_Flower.BLL;
using Shop_Flower.DAL;

using System.Windows;
using System.Windows.Controls;

using System.Windows.Input;
using Microsoft.Win32;

using Shop_Flower.DAL.Repository;
using Shop_Flower.DAL.Entities;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace Shop_Flower
{
    /// <summary>
    /// Interaction logic for AdminManagementWindow.xaml
    /// </summary>
    public partial class AdminManagementWindow : Window
    {
        private readonly OrderServices orderServices;
        private readonly UserService userService;
        private readonly FlowerInfoService flowerInfoService;
        private readonly CategoryService categoryService;

        private bool isUpdating = false;
        private User updatingUser = null;
        private FlowerInfo currentFlower = null;
        public AdminManagementWindow()
        {
            InitializeComponent();
            var context = new ShopContext();
            orderServices = new OrderServices(new OrderRepository(context));
            userService = new UserService(new UserRepository(context));
            flowerInfoService = new FlowerInfoService(new FlowerInfoRepository(context));
            categoryService = new CategoryService(new CategoryRepository(context));
            LoadOrder();
            userService = new UserService(new UserRepository(context));
            LoadUser();
            LoadFlower();
            LoadCategory();
        }
        private void LoadOrder()
        {
            OrderDataGrid.ItemsSource = orderServices.GetAllOrders();
        }
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1();
            window1.Show();
            this.Close();
           
        }
        public void LoadFlower()
        {
            FlowerDataGrid.ItemsSource = flowerInfoService.GetAllFlowers();
        }

        public void LoadCategory()
        {
            CategoryDataGrid.ItemsSource = categoryService.GetCategories();

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

        private void PopupSave_Click1(object sender, RoutedEventArgs e)
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

        private void PopupCancel_Click1(object sender, RoutedEventArgs e)
        {
            FlowerPopup.IsOpen = false;
        }

        private void ClearPopupFields1()
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

                // Kiểm tra nếu đường dẫn là một file local
                if (System.IO.File.Exists(imagePath))
                {
                    ImageViewerWindow imageViewer = new ImageViewerWindow(imagePath);
                    imageViewer.Show();
                }

                else if (Uri.IsWellFormedUriString(imagePath, UriKind.Absolute))
                {
                    try
                    {

                        Uri imageUri = new Uri(imagePath);

                        ImageViewerWindow imageViewer = new ImageViewerWindow(imageUri);
                        imageViewer.Show();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading image from URL: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Image file or URL not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            var inputName = Microsoft.VisualBasic.Interaction.InputBox("Enter the category name:", "Add Category", "");


            if (inputName == "")
            {
                return;
            }

            if (!string.IsNullOrWhiteSpace(inputName))
            {
                var category = new Category { CategoryName = inputName };
                categoryService.AddCategory(category);
                LoadCategory();
                MessageBox.Show("Category added successfully.");
            }
            else
            {
                MessageBox.Show("Category name cannot be empty.");
            }
        }

        private void UpdateCategory_Click(object sender, RoutedEventArgs e)
        {
            if (CategoryDataGrid.SelectedItem is Category selectedCategory)
            {
                var inputName = Microsoft.VisualBasic.Interaction.InputBox("Enter the new category name:", "Update Category", selectedCategory.CategoryName);


                if (inputName == "")
                {
                    return;
                }

                if (!string.IsNullOrWhiteSpace(inputName))
                {
                    selectedCategory.CategoryName = inputName;
                    categoryService.UpdateCategory(selectedCategory);
                    LoadCategory();
                    MessageBox.Show("Category updated successfully.");
                }
                else
                {
                    MessageBox.Show("Category name cannot be empty.");
                }
            }
            else
            {
                MessageBox.Show("Please select a category to update.");
            }
        }

        private void DeleteCategory_Click(object sender, RoutedEventArgs e)
        {
            if (CategoryDataGrid.SelectedItem is Category selectedCategory)
            {
                var result = MessageBox.Show($"Are you sure you want to delete the category '{selectedCategory.CategoryName}'?", "Delete Category", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    categoryService.DeleteCategory(selectedCategory.CategoryId);
                    LoadCategory();
                    MessageBox.Show("Category deleted successfully.");
                }
            }
            else
            {
                MessageBox.Show("Please select a category to delete.");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1();
            window1.Show();
            this.Close();
        }

        private void ManageUser_Click(object sender, RoutedEventArgs e)
        {
            DashboardContent.Visibility = Visibility.Collapsed;
            CategoryContent.Visibility = Visibility.Collapsed;
            FlowerContent.Visibility = Visibility.Collapsed;
            OrderContent.Visibility = Visibility.Collapsed;

            // Hiển thị phần Manage User
            UserContent.Visibility = Visibility.Visible;
        }

        private void ManageCategory_Click(object sender, RoutedEventArgs e)
        {
            DashboardContent.Visibility = Visibility.Collapsed;
            UserContent.Visibility = Visibility.Collapsed;
            FlowerContent.Visibility = Visibility.Collapsed;
            OrderContent.Visibility = Visibility.Collapsed;

            // Hiển thị phần Manage Category
            CategoryContent.Visibility = Visibility.Visible;
        }

        private void ManageFlower_Click(object sender, RoutedEventArgs e)
        {
            DashboardContent.Visibility = Visibility.Collapsed;
            UserContent.Visibility = Visibility.Collapsed;
            CategoryContent.Visibility = Visibility.Collapsed;
            OrderContent.Visibility = Visibility.Collapsed;

            // Hiển thị phần Manage Flower
            FlowerContent.Visibility = Visibility.Visible;
        }

        private void ManageOrder_Click(object sender, RoutedEventArgs e)
        {
            DashboardContent.Visibility = Visibility.Collapsed;
            UserContent.Visibility = Visibility.Collapsed;
            CategoryContent.Visibility = Visibility.Collapsed;
            FlowerContent.Visibility = Visibility.Collapsed;

            // Hiển thị phần Manage Order
            OrderContent.Visibility = Visibility.Visible;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string keyword = CategorySearchTextBox.Text;


            var result = categoryService.SearchCategories(keyword);


            CategoryDataGrid.ItemsSource = result;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string username = UserSearchTextBox.Text;
            var results = userService.SearchUsersByUsername(username);
            UserDataGrid.ItemsSource = results;
        }
    }
    }
