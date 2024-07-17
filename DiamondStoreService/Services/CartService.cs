using DiamondBusinessObject.Models;
using DiamondStoreRepository.Interfaces;
using DiamondStoreRepository.Repositories;
using DiamondStoreService.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondStoreService.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IJewelryRepository _jewelryRepository;

        public CartService(ICartRepository cartRepository, IJewelryRepository jewelryRepository)
        {
            _cartRepository = cartRepository;
            _jewelryRepository = jewelryRepository;
        }

        public async Task AddToCart(Cart cart)
        {
            await _cartRepository.AddToCart(cart);
        }

        public async Task<List<Cart>> GetCartItems(string userId)
        {
            return await _cartRepository.GetCartItemsByUserId(userId);
        }

        public async Task<Cart> GetCartItem(int cartId)
        {
            return await _cartRepository.GetCartItem(cartId);
        }

        public async Task DeleteCartItem(int cartId)
        {
            await _cartRepository.DeleteCartItem(cartId);
            Console.WriteLine("Cart item deleted: " + cartId);
        }

        public async Task UpdateCartItem(Cart cart)
        {
            var existingCartItem = await _cartRepository.GetCartItem(cart.CartId);
            if (existingCartItem != null)
            {
                existingCartItem.Quantity = cart.Quantity;
                await _cartRepository.UpdateCartItem(existingCartItem);
                Console.WriteLine("Cart item updated: " + existingCartItem.CartId);
            }
        }

        public async Task<Cart> GetCartItemByDetails(string userId, int? jewelryId, int? diamondId, int? jewelrySizeId)
        {
            return await _cartRepository.GetCartItemByDetails(userId, jewelryId, diamondId, jewelrySizeId);
        }
    }
}
