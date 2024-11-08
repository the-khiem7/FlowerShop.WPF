using Shop_Flower.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Flower.DAL.Repository
{
    internal interface IFlowerInfoRepository
    {
        List<FlowerInfo> GetAllFlowers();
        FlowerInfo GetFlower(int id);
        void AddFlower(FlowerInfo flower);
        void UpdateFlower(FlowerInfo flower);
        void DeleteFlower(int flowerId);
    }
}
