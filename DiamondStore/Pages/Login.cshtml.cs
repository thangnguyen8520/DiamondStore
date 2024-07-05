﻿using DiamondStoreService.Interfaces;
using DiamondStoreService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace DiamondStore.Pages
{
    public class LoginRegisterModel : PageModel
    {
        private readonly IAuthService _authService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginRegisterModel(IAuthService authService, IHttpContextAccessor httpContextAccessor)
        {
            _authService = authService;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public LoginRequest LoginInput { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostLoginAsync()
        {
            if (ModelState.IsValid)
            {
                bool result = await _authService.Login(LoginInput, _httpContextAccessor.HttpContext);

                if (result)
                {
                    // Đăng nhập thành công, điều hướng đến trang chính hoặc lấy thông tin user từ session
                    return RedirectToPage("/Index");
                }

                // Thêm lỗi vào ModelState
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return Page();
        }
    }

}
