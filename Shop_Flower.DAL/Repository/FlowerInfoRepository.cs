using Microsoft.EntityFrameworkCore;
using Shop_Flower.DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Shop_Flower.DAL.Repository
{
    public class FlowerInfoRepository : IFlowerInfoRepository
    {
        private readonly ShopContext _context;

        public FlowerInfoRepository(ShopContext context)
        {
            _context = context;
        }

      
        public List<FlowerInfo> GetAllFlowers()
        {
            return _context.FlowerInfos.Include(f => f.Category).ToList(); 
        }

        public List<FlowerInfo> GetAllWithCategory()
        {
            return _context.FlowerInfos.Include(f => f.Category).ToList();
        }

        public FlowerInfo GetFlower(int id)
        {
            return _context.FlowerInfos.Include(f => f.Category)
                                      .FirstOrDefault(f => f.FlowerId == id);
        }

        public void AddFlower(FlowerInfo flower)
        {
            _context.FlowerInfos.Add(flower);
            _context.SaveChanges();
        }

        public void UpdateFlower(FlowerInfo flower)
        {
            _context.FlowerInfos.Update(flower);
            _context.SaveChanges();
        }

        public void DeleteFlower(int id)
        {
            var flower = _context.FlowerInfos.Find(id);
            if (flower != null)
            {
                _context.FlowerInfos.Remove(flower);
                _context.SaveChanges();
            }
        }
    }
}
