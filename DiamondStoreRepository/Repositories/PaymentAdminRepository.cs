using DiamondBusinessObject.Models;
using DiamondStoreRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondStoreRepository.Repositories
{
    public class PaymentAdminRepository : GenericRepository<Payment>, IPaymentAdminRepository
    {
        private readonly DiamondStoreContext _context;

        public PaymentAdminRepository(DiamondStoreContext context) : base(context)
        {
        }
    }
}
