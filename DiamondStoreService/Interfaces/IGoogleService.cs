using DiamondStoreService.Models;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondStoreService.Interfaces
{
    public interface IGoogleService
    {
        Task<GoogleCallbackResult> HandleGoogleCallbackAsync();
    }
}
