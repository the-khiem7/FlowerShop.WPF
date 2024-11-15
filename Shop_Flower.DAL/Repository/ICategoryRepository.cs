using Shop_Flower.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Flower.DAL.Repository
{
    public interface ICategoryRepository
    {
        List<Category> GetCategories();

        Category GetCategory(int categoryid);

        void DeleteCategory(int id);

        void UpdateCategory(Category category);

        void AddCategory (Category category);
    }
}
