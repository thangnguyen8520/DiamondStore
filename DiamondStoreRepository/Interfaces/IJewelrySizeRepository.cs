using DiamondBusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondStoreRepository.Interfaces
{
    public interface IJewelrySizeRepository : IGenericRepository<JewelrySize>
    {
        Task<IList<JewelrySize>> GetAllJewelrySizes();

    }
}
