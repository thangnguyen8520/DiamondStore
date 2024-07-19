using DiamondBusinessObject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondStoreService.Interfaces
{
    public interface ICartService
    {
        Task AddToCart(Cart cart);
        Task<List<Cart>> GetCartItems(string userId);
        Task<Cart> GetCartItem(int cartId);
        Task<Cart> GetCartByUserId(string userId);

        Task DeleteCartItem(int cartId);
        Task UpdateCartItem(Cart cart);
        Task<Cart> GetCartDiamondByDetails(string userId, int? diamondId);
        Task<Cart> GetCartJewelryByDetails(string userId, int? jewelryId, int? jewelrySizeId);
        Task UpdateCartDiamondQuantity(int cartDiamondId, int quantity);
        Task UpdateCartJewelryQuantity(int cartJewelryId, int quantity);
        Task DeleteCartDiamond(int cartDiamondId);
        Task DeleteCartJewelry(int cartJewelryId);
    }

}
