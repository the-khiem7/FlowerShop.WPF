using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop_Flower.DAL.Entities;
using Shop_Flower.DAL.Repository;
namespace Shop_Flower.BLL.Services
{
    public class FlowerInfoService : IFlowerInfoService
    {
        private readonly FlowerInfoRepository _flowerInfoRepository;
        public FlowerInfoService(FlowerInfoRepository flowerInfoRepository)
        {
            _flowerInfoRepository = flowerInfoRepository;
        }
        public List<FlowerInfo> GetAllFlowers()
        {
            return _flowerInfoRepository.GetAllWithCategory().ToList();
        }
        public FlowerInfo GetFlower(int id)
        {
            return _flowerInfoRepository.GetFlower(id);
        }
        public void AddFlower(FlowerInfo flower)
        {
            _flowerInfoRepository.AddFlower(flower);
        }
        public void UpdateFlower(FlowerInfo flower)
        {
            _flowerInfoRepository.UpdateFlower(flower);
        }
        public void DeleteFlower(int flowerId)
        {
            _flowerInfoRepository.DeleteFlower(flowerId);
        }
        public void UpdateFlowerQuantity(int flowerId, int newQuantity)
        {
            var flower = _flowerInfoRepository.GetFlower(flowerId);
            if (flower != null)
            {
                flower.AvailableQuantity = newQuantity;
                _flowerInfoRepository.UpdateFlower(flower);
            }
        }
    }
}
