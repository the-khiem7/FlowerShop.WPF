using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop_Flower.DAL.Entities;
using Shop_Flower.DAL.Repository;
namespace Shop_Flower.BLL.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public Cart GetCart()
        {
            return _cartRepository.GetCart();
        }

        public void AddToCart(CartItem item)
        {
            _cartRepository.AddToCart(item);
        }

        public void RemoveFromCart(int flowerId)
        {
            _cartRepository.RemoveFromCart(flowerId);
        }

        public void ClearCart()
        {
            _cartRepository.ClearCart();
        }
    }
}
