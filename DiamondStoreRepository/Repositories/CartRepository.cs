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

        public async Task AddToCart(Cart cart)
        {
            await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();
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

        public async Task<List<Cart>> GetCartItemsByUserId(string userId)
        {
            var cartItems = await _context.Carts
                .Where(c => c.UserId == userId)
                .Include(c => c.Jewelry)
                    .ThenInclude(j => j.MainDiamonds)
                        .ThenInclude(md => md.Diamond)
                .Include(c => c.Jewelry)
                    .ThenInclude(j => j.SecondaryDiamonds)
                        .ThenInclude(sd => sd.Diamond)
                .Include(c => c.Jewelry)
                    .ThenInclude(j => j.Image)
                .Include(c => c.Diamond)
                    .ThenInclude(d => d.Image)
                .ToListAsync();

            foreach (var cartItem in cartItems)
            {
                if (cartItem.JewelryId.HasValue)
                {
                    float mainDiamondPrice = cartItem.Jewelry.MainDiamonds.Sum(md => md.Diamond?.DiamondPrice ?? 0);
                    float secondaryDiamondPrice = cartItem.Jewelry.SecondaryDiamonds.Sum(sd => sd.Diamond?.DiamondPrice ?? 0);
                    cartItem.Jewelry.TotalPrice = 1.3f * (mainDiamondPrice + secondaryDiamondPrice + cartItem.Jewelry.JewelryPrice + cartItem.Jewelry.LaborCost);
                }
            }

            return cartItems;
        }
    }
}
