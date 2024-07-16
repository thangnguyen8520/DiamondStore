using DiamondBusinessObject.Models;
using DiamondStoreRepository.Interfaces;
using DiamondStoreService.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondStoreService.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<List<Cart>> GetCartItems(string userId)
        {
            return await _cartRepository.GetCartItems(userId);
        }

        public async Task<Cart> GetCartItem(int cartId)
        {
            return await _cartRepository.GetCartItem(cartId);
        }

        public async Task DeleteCartItem(int cartId)
        {
            await _cartRepository.DeleteCartItem(cartId);
        }

        public async Task UpdateCartItem(Cart cart)
        {
            await _cartRepository.UpdateCartItem(cart);
        }
    }
}
