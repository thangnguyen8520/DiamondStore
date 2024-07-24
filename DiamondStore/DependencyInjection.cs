using DiamondBusinessObject.Models;
using DiamondStoreRepository.Interfaces;
using DiamondStoreRepository.Repositories;
using DiamondStoreService.Interfaces;
using DiamondStoreService.Mappers;
using DiamondStoreService.Services;
using DiamondStoreService.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DiamondStore
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructuresService(this IServiceCollection services, IConfiguration configuration)
        {
            // Add UnitOfWork, Repositories and Services
            services.AddScoped<IUnitOfWork, UnitOfWork>();
          
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IGoogleService, GoogleService>();
            services.AddScoped<IFirebaseService, FirebaseService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IDiamondRepository, DiamondRepository>();
            services.AddScoped<IDiamondService, DiamondService>();
            services.AddScoped<IJewelryRepository, JewelryRepository>();
            services.AddScoped<IJewelryTypeRepository, JewelryTypeRepository>();
            services.AddScoped<IJewelryMaterialRepository, JewelryMaterialRepository>();
            services.AddScoped<IJewelrySizeRepository, JewelrySizeRepository>();
            services.AddScoped<IJewelryService, JewelryService>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IPromotionRepository, PromotionRepository>();
            services.AddScoped<IPromotionService, PromotionService>();
            services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
            services.AddScoped<IPaymentMethodService, PaymentMethodService>();
            services.AddHttpClient<IPaymentService, PaymentService>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IPaymentService, PaymentService>();



            // Add DbContext
            services.AddDbContext<DiamondStoreContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DatabaseConnection")));

            // Add Default Identity
            services.AddIdentity<User, IdentityRole>(options =>
                options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<DiamondStoreContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders()
                .AddRoles<IdentityRole>();

            services.Configure<DataProtectionTokenProviderOptions>(options =>
             {
                 options.TokenLifespan = TimeSpan.FromMinutes(10);
             });

            // Add AutoMapper
            services.AddAutoMapper(typeof(MapperConfigurationsProfile).Assembly);

            return services;
        }
    }
}
