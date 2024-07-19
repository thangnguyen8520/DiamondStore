using DiamondBusinessObject.Models;
using DiamondStoreRepository.Interfaces;
using DiamondStoreService.Interfaces;
using Microsoft.Extensions.Logging;
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

        public async Task AddToCart(Cart cart)
        {
            await _cartRepository.AddToCart(cart);
        }

        public async Task<List<Cart>> GetCartItems(string userId)
        {
            return await _cartRepository.GetCartItems(userId);
        }

        public async Task<Cart> GetCartItem(int cartId)
        {
            return await _cartRepository.GetCartItem(cartId);
        }

        public async Task<Cart> GetCartByUserId(string userId)
        {
            return await _cartRepository.GetCartByUserId(userId);
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

        public async Task<Cart> GetCartJewelryByDetails(string userId, int? jewelryId, int? jewelrySizeId)
        {
            return await _cartRepository.GetCartJewelryByDetails(userId, jewelryId, jewelrySizeId);
        }

        public async Task<Cart> GetCartDiamondByDetails(string userId, int? diamondId)
        {
            return await _cartRepository.GetCartDiamondByDetails(userId, diamondId);
        }

        public async Task UpdateCartDiamondQuantity(int cartDiamondId, int quantity)
        {
            var cartDiamond = await _cartRepository.GetCartDiamondById(cartDiamondId);
            if (cartDiamond != null)
            {
                _logger.LogInformation($"Updating cart diamond quantity. CartDiamondId: {cartDiamondId}, Quantity: {quantity}");
                cartDiamond.Quantity = quantity;
                await _cartRepository.UpdateCartDiamond(cartDiamond);
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
                _logger.LogInformation($"Updating cart jewelry quantity. CartJewelryId: {cartJewelryId}, Quantity: {quantity}");
                cartJewelry.Quantity = quantity;
                await _cartRepository.UpdateCartJewelry(cartJewelry);
            }
            else
            {
                _logger.LogWarning($"CartJewelryId: {cartJewelryId} not found.");
            }
        }


        public async Task DeleteCartDiamond(int cartDiamondId)
        {
            await _cartRepository.DeleteCartDiamond(cartDiamondId);
        }

        public async Task DeleteCartJewelry(int cartJewelryId)
        {
            await _cartRepository.DeleteCartJewelry(cartJewelryId);
        }
    }
}
