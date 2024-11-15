using Shop_Flower.DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Shop_Flower.DAL.Repository
{
    public interface IFlowerInfoRepository
    {
        List<FlowerInfo> GetAllFlowers();
        List<FlowerInfo> GetAllWithCategory(); 
        FlowerInfo GetFlower(int id);
        void AddFlower(FlowerInfo flower);
        void UpdateFlower(FlowerInfo flower);
        void DeleteFlower(int flowerId);
    }
}
