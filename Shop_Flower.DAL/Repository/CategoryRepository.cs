using Microsoft.EntityFrameworkCore;
using Shop_Flower.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Flower.DAL.Repository
{
    public class CategoryRepository : ICategoryRepository
    {

        private readonly ShopContext _shopContext;
        public CategoryRepository(ShopContext shopContext) { 

            _shopContext = new ShopContext();
        }

        public void AddCategory(Category category)
        {
            _shopContext.Categories.Add(category);
            _shopContext.SaveChanges();
        }

        public void DeleteCategory(int categoryid)
        {
            var category = _shopContext.Categories.Find(categoryid);
            if (category != null)
            {
                _shopContext.Categories.Remove(category);
                _shopContext.SaveChanges();
            }
        }

        public List<Category> GetCategories()
        {
            return _shopContext.Categories.ToList();
        }

        public Category GetCategory(int categoryid)
        {
            return _shopContext.Categories.Find(categoryid);
        }

        public void UpdateCategory(Category category)
        {
            _shopContext.Categories.Update(category);
            _shopContext.SaveChanges();
        }
    }
}
