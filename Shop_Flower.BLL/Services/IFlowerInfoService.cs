using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop_Flower.DAL.Entities;
using Shop_Flower.DAL.Repository;
namespace Shop_Flower.BLL.Services
{
    internal interface IFlowerInfoService
    {
        List<FlowerInfo> GetAllFlowers();
        FlowerInfo GetFlower(int id);
        void AddFlower(FlowerInfo flower);
        void UpdateFlower(FlowerInfo flower);
        void DeleteFlower(int flowerId);
    }
}
