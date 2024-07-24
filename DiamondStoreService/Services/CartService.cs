using DiamondBusinessObject.Models;
using DiamondStoreRepository.Interfaces;
using DiamondStoreService.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiamondStoreService.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ILogger<CartService> _logger;

        public CartService(ICartRepository cartRepository, ILogger<CartService> logger)
        {
            _cartRepository = cartRepository;
            _logger = logger;
        }

        public async Task<Cart> GetActiveCartByUserId(string userId)
        {
            return await _cartRepository.GetActiveCartByUserIdAsync(userId);
        }

        public async Task AddToCart(Cart cart)
        {
            await _cartRepository.AddToCart(cart);
        }

        public void Add(Cart cart)
        {
            _cartRepository.Add(cart);
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
            _logger.LogInformation($"Updating cart item. CartId: {cart.CartId}");
            await _cartRepository.UpdateCartItem(cart);
        }

        public async Task<Cart> GetCartByUserId(string userId)
        {
            return await _cartRepository.GetCartByUserId(userId);
        }

        public async Task AddCartPromotion(CartPromotion cartPromotion)
        {
            await _cartRepository.AddCartPromotion(cartPromotion);
        }

        public async Task RemoveCartPromotion(int cartPromotionId)
        {
            await _cartRepository.RemoveCartPromotion(cartPromotionId);
        }

        public async Task<List<CartPromotion>> GetCartPromotions(string userId)
        {
            return await _cartRepository.GetCartPromotions(userId);
        }

        public async Task DeleteCartDiamond(int cartDiamondId)
        {
            await _cartRepository.DeleteCartDiamond(cartDiamondId);
        }

        public async Task DeleteCartJewelry(int cartJewelryId)
        {
            await _cartRepository.DeleteCartJewelry(cartJewelryId);
        }

        public async Task UpdateCartDiamondQuantity(int cartDiamondId, int quantity)
        {
            var cartDiamond = await _cartRepository.GetCartDiamondById(cartDiamondId);
            if (cartDiamond != null)
            {
                if (cartDiamond.Diamond != null)
                {
                    _logger.LogInformation($"Updating cart diamond quantity. CartDiamondId: {cartDiamondId}, Quantity: {quantity}");
                    cartDiamond.Quantity = quantity;
                    await _cartRepository.UpdateCartDiamond(cartDiamond);
                }
                else
                {
                    _logger.LogWarning($"Diamond details not found for CartDiamondId: {cartDiamondId}");
                    throw new Exception("Diamond details not found.");
                }
            }
            else
            {
                _logger.LogWarning($"CartDiamondId: {cartDiamondId} not found.");
            }
        }

        public async Task UpdateCartJewelryQuantity(int cartJewelryId, int quantity)
        {
            var cartJewelry = await _cartRepository.GetCartJewelryById(cartJewelryId);
            if (cartJewelry != null)
            {
                if (cartJewelry.Jewelry != null)
                {
                    _logger.LogInformation($"Updating cart jewelry quantity. CartJewelryId: {cartJewelryId}, Quantity: {quantity}");
                    cartJewelry.Quantity = quantity;
                    await _cartRepository.UpdateCartJewelry(cartJewelry);
                }
                else
                {
                    _logger.LogWarning($"Jewelry details not found for CartJewelryId: {cartJewelryId}");
                    throw new Exception("Jewelry details not found.");
                }
            }
            else
            {
                _logger.LogWarning($"CartJewelryId: {cartJewelryId} not found.");
            }
        }

        public async Task<CartDiamond> GetCartDiamondById(int cartDiamondId)
        {
            return await _cartRepository.GetCartDiamondById(cartDiamondId);
        }

        public async Task<CartJewelry> GetCartJewelryById(int cartJewelryId)
        {
            return await _cartRepository.GetCartJewelryById(cartJewelryId);
        }
    }
}
