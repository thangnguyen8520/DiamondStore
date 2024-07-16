using DiamondBusinessObject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondStoreService.Interfaces
{
    public interface ICartService
    {
        Task<List<Cart>> GetCartItems(string userId);
        Task<Cart> GetCartItem(int cartId);
        Task DeleteCartItem(int cartId);
        Task UpdateCartItem(Cart cart);
    }
}
