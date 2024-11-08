using System.Configuration;
using System.Data;
using System.Windows;
using Shop_Flower.BLL.Services;
using Shop_Flower.DAL;
using Shop_Flower.DAL.Repository;
namespace Shop_Flower
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Create necessary dependencies
            var context = new ShopContext();
            var flowerInfoRepository = new FlowerInfoRepository(context);
            var flowerInfoService = new FlowerInfoService(flowerInfoRepository);

            // Create and configure AdminWindow
            var adminWindow = new AdminWindow();
            adminWindow.SetFlowerInfoService(flowerInfoService);

            // Show the window
            adminWindow.Show();
        }
    }


}
