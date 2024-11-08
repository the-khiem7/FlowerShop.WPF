using Shop_Flower.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return _context.FlowerInfos.ToList();
        }
        public FlowerInfo GetFlower(int id)
        {
            {
                return _context.FlowerInfos.FirstOrDefault(Flower => Flower.FlowerId == id);
            }
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
            }
            _context.SaveChanges();
        }
    }
}
