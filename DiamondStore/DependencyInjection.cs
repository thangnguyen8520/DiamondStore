using DiamondBusinessObject.Models;
using DiamondDAO;
using DiamondStoreRepository.Interfaces;
using DiamondStoreRepository.Repositories;
using DiamondStoreService.Interfaces;
using DiamondStoreService.Mappers;
using DiamondStoreService.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DiamondStore
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructuresService(this IServiceCollection services, IConfiguration configuration)
        {
            // Add UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ProductDAO>();


            // Add DbContext
            services.AddDbContext<DiamondStoreContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DatabaseConnection")));

            // Add Default Identity
            services.AddIdentity<User, IdentityRole>(options =>
                options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<DiamondStoreContext>()
                .AddSignInManager()
                .AddRoles<IdentityRole>();

            // Add AutoMapper
            services.AddAutoMapper(typeof(MapperConfigurationsProfile).Assembly);

            return services;
        }
    }
}
