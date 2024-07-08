using DiamondBusinessObject.Models;
using DiamondStoreRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondStoreRepository.Repositories
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        private readonly DiamondStoreContext _context;

        public PaymentRepository(DiamondStoreContext context) : base(context)
        {
            _context = context;
        }
    }
}
