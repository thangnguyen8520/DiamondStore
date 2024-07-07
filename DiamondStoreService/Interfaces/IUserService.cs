﻿using DiamondStoreService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondStoreService.Interfaces
{
    public interface IUserService
    {
        Task<UserProfileViewDTO> GetUserByIdAsync(string userId);
    }
}
