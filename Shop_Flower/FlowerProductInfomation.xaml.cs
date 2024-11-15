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
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Shop_Flower
{
    /// <summary>
    /// Interaction logic for FlowerProductInfomation.xaml
    /// </summary>
    public partial class FlowerProductInfomation : Window
    {
        private readonly FlowerInfoService _flowerInfoService;


        public int FlowerId { get; set; }

        public FlowerProductInfomation(int flowerId)
        {
            _flowerInfoService = new FlowerInfoService(new FlowerInfoRepository(new ShopContext()));
            InitializeComponent();
            FlowerId = flowerId;
            LoadFlowerInfo();
        }

        private void LoadFlowerInfo()
        {
            var flower = _flowerInfoService.GetFlower(FlowerId);
            FlowerInfoListing.ItemsSource = new List<FlowerInfo> { flower };
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
