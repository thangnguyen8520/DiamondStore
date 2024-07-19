using DiamondBusinessObject.Models;
using DiamondStoreRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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
            if (cart.CreateDate == default(DateTime))
            {
                cart.CreateDate = DateTime.Now;
            }

            foreach (var cartDiamond in cart.CartDiamonds)
            {
                if (cartDiamond.AddDate == default(DateTime))
                {
                    cartDiamond.AddDate = DateTime.Now;
                }
            }

            foreach (var cartJewelry in cart.CartJewelries)
            {
                if (cartJewelry.AddDate == default(DateTime))
                {
                    cartJewelry.AddDate = DateTime.Now;
                }
            }

            await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Cart>> GetCartItems(string userId)
        {
            return await _context.Carts
                .Include(c => c.CartDiamonds).ThenInclude(cd => cd.Diamond)
                .Include(c => c.CartJewelries).ThenInclude(cj => cj.Jewelry)
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }

        public async Task<Cart> GetCartItem(int cartId)
        {
            return await _context.Carts
                .Include(c => c.CartDiamonds).ThenInclude(cd => cd.Diamond)
                .Include(c => c.CartJewelries).ThenInclude(cj => cj.Jewelry)
                .FirstOrDefaultAsync(c => c.CartId == cartId);
        }

        public async Task<Cart> GetCartByUserId(string userId)
        {
            return await _context.Carts
                .Include(c => c.CartDiamonds)
                .Include(c => c.CartJewelries)
                .FirstOrDefaultAsync(c => c.UserId == userId);
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

        public async Task<Cart> GetCartJewelryByDetails(string userId, int? jewelryId, int? jewelrySizeId)
        {
            return await _context.Carts
                .Include(c => c.CartJewelries)
                .FirstOrDefaultAsync(c => c.UserId == userId &&
                                          jewelryId != null && c.CartJewelries.Any(cj => cj.JewelryId == jewelryId && cj.JewelrySizeId == jewelrySizeId));
        }

        public async Task<Cart> GetCartDiamondByDetails(string userId, int? diamondId)
        {
            return await _context.Carts
                .Include(c => c.CartDiamonds)
                .FirstOrDefaultAsync(c => c.UserId == userId &&
                                          diamondId != null && c.CartDiamonds.Any(cd => cd.DiamondId == diamondId));
        }

        public async Task<CartDiamond> GetCartDiamondById(int cartDiamondId)
        {
            return await _context.CartDiamonds.FindAsync(cartDiamondId);
        }

        public async Task<CartJewelry> GetCartJewelryById(int cartJewelryId)
        {
            return await _context.CartJewelries.FindAsync(cartJewelryId);
        }

        public async Task UpdateCartDiamond(CartDiamond cartDiamond)
        {
            _context.CartDiamonds.Update(cartDiamond);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCartJewelry(CartJewelry cartJewelry)
        {
            _context.CartJewelries.Update(cartJewelry);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCartDiamond(int cartDiamondId)
        {
            var cartDiamond = await _context.CartDiamonds.FindAsync(cartDiamondId);
            if (cartDiamond != null)
            {
                _context.CartDiamonds.Remove(cartDiamond);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteCartJewelry(int cartJewelryId)
        {
            var cartJewelry = await _context.CartJewelries.FindAsync(cartJewelryId);
            if (cartJewelry != null)
            {
                _context.CartJewelries.Remove(cartJewelry);
                await _context.SaveChangesAsync();
            }
        }
    }
}
