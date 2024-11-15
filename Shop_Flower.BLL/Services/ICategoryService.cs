using Shop_Flower.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Flower.BLL.Services
{
    public interface ICategoryService
    {
        List<Category> GetCategories();

        Category GetCategory(int id);

        void DeleteCategory(int id);

        void UpdateCategory(Category category);

        void AddCategory(Category category);
    }
}
