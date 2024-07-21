using DiamondBusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondStoreRepository.Interfaces
{
    public interface ICartRepository
    {
        Task AddToCart(Cart cart);
        Task<List<Cart>> GetCartItems(string userId);
        Task<Cart> GetCartItem(int cartId);
        Task<Cart> GetCartByUserId(string userId);
        Task DeleteCartItem(int cartId);
        Task UpdateCartItem(Cart cart);
        Task AddCartPromotion(CartPromotion cartPromotion);
        Task RemoveCartPromotion(int cartPromotionId);
        Task<List<CartPromotion>> GetCartPromotions(string userId);
        Task<CartDiamond> GetCartDiamondById(int cartDiamondId);
        Task<CartJewelry> GetCartJewelryById(int cartJewelryId);
        Task UpdateCartDiamond(CartDiamond cartDiamond);
        Task UpdateCartJewelry(CartJewelry cartJewelry);
        Task DeleteCartDiamond(int cartDiamondId);
        Task DeleteCartJewelry(int cartJewelryId);
    }
}
