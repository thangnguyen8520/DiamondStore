using DiamondBusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondStoreService.Interfaces
{
    public interface IPromotionService
    {
        Task<List<UserPromotion>> GetUserPromotions(string userId);
    }
}
