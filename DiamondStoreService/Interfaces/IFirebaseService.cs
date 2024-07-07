using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondStoreService.Interfaces
{
    public interface IFirebaseService
    {
        Task<string> UploadImageAsync(string fileName, Stream stream);
    }
}
