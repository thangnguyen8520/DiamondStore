using DiamondBusinessObject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondStoreService.Interfaces
{
    public interface ICartService
    {
        void Add(Cart cart);
        Task<Cart> GetActiveCartByUserId(string userId);
        Task AddToCart(Cart cart);
        Task<List<Cart>> GetCartItems(string userId);
        Task<Cart> GetCartItem(int cartId);
        Task DeleteCartItem(int cartId);
        Task UpdateCartItem(Cart cart);
        Task<Cart> GetCartByUserId(string userId);
        Task AddCartPromotion(CartPromotion cartPromotion);
        Task RemoveCartPromotion(int cartPromotionId);
        Task<List<CartPromotion>> GetCartPromotions(string userId);
        Task DeleteCartDiamond(int cartDiamondId);
        Task DeleteCartJewelry(int cartJewelryId);
        Task UpdateCartDiamondQuantity(int cartDiamondId, int quantity);
        Task UpdateCartJewelryQuantity(int cartJewelryId, int quantity);
        Task<CartDiamond> GetCartDiamondById(int cartDiamondId); 
        Task<CartJewelry> GetCartJewelryById(int cartJewelryId); 
    }
}
