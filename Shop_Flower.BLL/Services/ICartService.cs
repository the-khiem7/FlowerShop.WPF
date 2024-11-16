using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop_Flower.DAL.Entities;
namespace Shop_Flower.BLL.Services
{
    public interface ICartService
    {
        Cart GetCart();
        void AddToCart(CartItem item);
        void RemoveFromCart(int flowerId);
        void ClearCart();

        
    }
}
