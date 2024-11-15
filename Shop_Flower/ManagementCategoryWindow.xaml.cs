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


namespace Shop_Flower
{
    /// <summary>
    /// Interaction logic for ManagementCategoryWindow.xaml
    /// </summary>
    public partial class ManagementCategoryWindow : Window
    {
        private readonly CategoryService categoryService;
        public ManagementCategoryWindow()
        {
            InitializeComponent();
            var context = new ShopContext();
            categoryService = new CategoryService(new CategoryRepository(context));
            LoadCategory();

        }

        public void LoadCategory()
        {
            CategoryDataGrid.ItemsSource = categoryService.GetCategories();
        }

        private void AddCateogry_Click(object sender, RoutedEventArgs e)
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

        private void UpdateCateogry_Click(object sender, RoutedEventArgs e)
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
    }
}
