using DiamondBusinessObject.Models;
using DiamondStoreRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiamondStoreRepository.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly DiamondStoreContext _context;

        public CartRepository(DiamondStoreContext context)
        {
            _context = context;
        }

        public async Task<List<Cart>> GetCartItems(string userId)
        {
            return await _context.Carts
                .Include(c => c.Diamond)
                .Include(c => c.Jewelry)
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }

        public async Task<Cart> GetCartItem(int cartId)
        {
            return await _context.Carts
                .Include(c => c.Diamond)
                .Include(c => c.Jewelry)
                .FirstOrDefaultAsync(c => c.CartId == cartId);
        }

        public async Task DeleteCartItem(int cartId)
        {
            var cartItem = await _context.Carts.FindAsync(cartId);
            if (cartItem != null)
            {
                _context.Carts.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateCartItem(Cart cart)
        {
            _context.Carts.Update(cart);
            await _context.SaveChangesAsync();
        }
    }
}
