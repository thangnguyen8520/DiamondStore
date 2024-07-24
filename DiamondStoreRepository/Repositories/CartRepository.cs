using DiamondBusinessObject.Models;
using DiamondStoreRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiamondStoreRepository.Repositories
{
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        private readonly DiamondStoreContext _context;

        public CartRepository(DiamondStoreContext context) : base(context)
        {
            _context = context;
        }

        public void Add(Cart cart)
        {
            _context.Carts.Add(cart);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        private float CalculateTotalPrice(Jewelry jewelry)
        {
            float mainDiamondPrice = jewelry.Diamond?.DiamondPrice ?? 0;
            float secondaryDiamondPrice = jewelry.SecondaryDiamonds.Sum(sd => sd.Diamond?.DiamondPrice ?? 0);
            return 1.3f * (mainDiamondPrice + secondaryDiamondPrice + jewelry.JewelryPrice + jewelry.LaborCost);
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
            var carts = await _context.Carts
                .Include(c => c.CartDiamonds)
                    .ThenInclude(cd => cd.Diamond)
                .Include(c => c.CartJewelries)
                    .ThenInclude(cj => cj.Jewelry)
                    .ThenInclude(j => j.Diamond)
                .Include(c => c.CartJewelries)
                    .ThenInclude(cj => cj.Jewelry)
                    .ThenInclude(j => j.SecondaryDiamonds)
                    .ThenInclude(sd => sd.Diamond)
                .Include(c => c.CartPromotions)
                    .ThenInclude(cp => cp.UserPromotion)
                    .ThenInclude(up => up.Promotion)
                .Where(c => c.UserId == userId)
                .ToListAsync();

            foreach (var cart in carts)
            {
                foreach (var cartJewelry in cart.CartJewelries)
                {
                    cartJewelry.Jewelry.TotalPrice = CalculateTotalPrice(cartJewelry.Jewelry);
                }
            }

            return carts;
        }

        public async Task<Cart> GetCartItem(int cartId)
        {
            var cart = await _context.Carts
                .Include(c => c.CartDiamonds).ThenInclude(cd => cd.Diamond)
                .Include(c => c.CartJewelries).ThenInclude(cj => cj.Jewelry)
                .ThenInclude(j => j.Diamond)
                .Include(c => c.CartJewelries).ThenInclude(cj => cj.Jewelry)
                .ThenInclude(j => j.SecondaryDiamonds).ThenInclude(sd => sd.Diamond)
                .Include(c => c.CartPromotions).ThenInclude(cp => cp.UserPromotion)
                .ThenInclude(up => up.Promotion)
                .FirstOrDefaultAsync(c => c.CartId == cartId);

            if (cart != null)
            {
                foreach (var cartJewelry in cart.CartJewelries)
                {
                    cartJewelry.Jewelry.TotalPrice = CalculateTotalPrice(cartJewelry.Jewelry);
                }
            }

            return cart;
        }

        public async Task<Cart> GetCartByUserId(string userId)
        {
            return await _context.Carts
                .Include(c => c.CartDiamonds)
                .Include(c => c.CartJewelries)
                .Include(c => c.CartPromotions).ThenInclude(cp => cp.UserPromotion).ThenInclude(up => up.Promotion)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task<List<Cart>> GetCartsByUserIdAsync(string userId)
        {
            return await _context.Set<Cart>()
                .Where(c => c.UserId == userId && c.Status)
                .ToListAsync();
        }

        public async Task<Cart> GetActiveCartByUserIdAsync(string userId)
        {
            return await _context.Carts
                .Include(c => c.CartDiamonds)
                    .ThenInclude(cd => cd.Diamond)
                .Include(c => c.CartJewelries)
                    .ThenInclude(cj => cj.Jewelry)
                    .ThenInclude(j => j.Diamond)
                .Include(c => c.CartJewelries)
                    .ThenInclude(cj => cj.Jewelry)
                    .ThenInclude(j => j.SecondaryDiamonds)
                    .ThenInclude(sd => sd.Diamond)
                .Include(c => c.CartPromotions)
                    .ThenInclude(cp => cp.UserPromotion)
                    .ThenInclude(up => up.Promotion)
                .Where(c => c.UserId == userId && c.Status)
                .FirstOrDefaultAsync();
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

        public async Task AddCartPromotion(CartPromotion cartPromotion)
        {
            await _context.CartPromotions.AddAsync(cartPromotion);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveCartPromotion(int cartPromotionId)
        {
            var cartPromotion = await _context.CartPromotions.FindAsync(cartPromotionId);
            if (cartPromotion != null)
            {
                _context.CartPromotions.Remove(cartPromotion);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<CartPromotion>> GetCartPromotions(string userId)
        {
            return await _context.CartPromotions
                .Include(cp => cp.UserPromotion)
                .ThenInclude(up => up.Promotion)
                .Where(cp => cp.Cart.UserId == userId)
                .ToListAsync();
        }

        public async Task<CartDiamond> GetCartDiamondById(int cartDiamondId)
        {
            return await _context.CartDiamonds
                .Include(cd => cd.Diamond)
                .FirstOrDefaultAsync(cd => cd.CartDiamondId == cartDiamondId);
        }

        public async Task<CartJewelry> GetCartJewelryById(int cartJewelryId)
        {
            return await _context.CartJewelries
                .Include(cj => cj.Jewelry)
                .FirstOrDefaultAsync(cj => cj.CartJewelryId == cartJewelryId);
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
