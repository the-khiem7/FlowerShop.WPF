using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop_Flower.DAL.Entities;
namespace Shop_Flower.DAL.Repository
{
    public class CartRepository : ICartRepository
    {
        private Cart _cart = new Cart();

        public Cart GetCart()
        {
            return _cart;
        }

        public void AddToCart(CartItem item)
        {
            var existingItem = _cart.Items.FirstOrDefault(i => i.FlowerId == item.FlowerId);
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                _cart.Items.Add(item);
            }
        }

        public void RemoveFromCart(int flowerId)
        {
            var item = _cart.Items.FirstOrDefault(i => i.FlowerId == flowerId);
            if (item != null)
            {
                _cart.Items.Remove(item);
            }
        }

        public void ClearCart()
        {
            _cart.Items.Clear();
        }
    }
}
