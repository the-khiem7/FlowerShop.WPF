using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop_Flower.BLL;
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
        private readonly IServiceProvider _serviceProvider;

        public App()
        {
            var services = new ServiceCollection();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            services.AddDbContext<ShopContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Shop")));

            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IFlowerInfoRepository, FlowerInfoRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IOrderServices, OrderServices>();
            services.AddTransient<IFlowerInfoService, FlowerInfoService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICartService,CartService>();
            services.AddTransient<ICartRepository, CartRepository>();

            services.AddTransient<OrderHistoryWindow>();
            services.AddTransient<CartWindow>();
            services.AddTransient<AdminManagementWindow>();
            services.AddTransient<AdminWindow>();
            services.AddTransient<Window1>();
            services.AddTransient<ManagementCategoryWindow>();
            services.AddTransient<ManagementOrderWindow>();
            services.AddTransient<ManagementCategoryWindow>();
            services.AddTransient<UserWindow>();
            services.AddTransient<OrderWindow>();

            _serviceProvider = services.BuildServiceProvider();
        }
    }


}
