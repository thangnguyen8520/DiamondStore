using System.Threading.Tasks;
using DiamondBusinessObject.Models;
using DiamondStoreRepository.Interfaces;
using DiamondStoreRepository.Repositories;
using Microsoft.EntityFrameworkCore;

public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
{
    private readonly DiamondStoreContext _context;

    public PaymentRepository(DiamondStoreContext context) : base(context)
    {
        _context = context;
    }

    public async Task<User> GetUserWithCarts(string userId)
    {
        return await _context.Users
            .Include(u => u.Carts)
                .ThenInclude(c => c.CartDiamonds)
                .ThenInclude(cd => cd.Diamond)
            .Include(u => u.Carts)
                .ThenInclude(c => c.CartJewelries)
                .ThenInclude(cj => cj.Jewelry)
                .ThenInclude(cj => cj.Diamond)
            .Include(u => u.Carts)
                .ThenInclude(c => c.CartJewelries)
                .ThenInclude(cj => cj.Jewelry)
                .ThenInclude(cj => cj.SecondaryDiamonds)
            .FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task UpdatePayment(Payment payment)
    {
        _context.Payments.Update(payment);
        await _context.SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
