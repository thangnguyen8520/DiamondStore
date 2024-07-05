using DiamondBusinessObject.Models;
using DiamondStore;
using DiamondStoreRepository.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<DiamondStoreContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection")));

builder.Services.AddInfrastructuresService(builder.Configuration);

//builder.Services.AddDefaultIdentity<User>(options =>
//        options.SignIn.RequireConfirmedAccount = false)
//        .AddEntityFrameworkStores<DiamondStoreContext>();

builder.Services.AddTransient<SeedData>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSession();
builder.Services.AddMemoryCache();
builder.Services.AddRazorPages();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var seedData = services.GetRequiredService<SeedData>();
    await seedData.Initialize(services, services.GetRequiredService<RoleManager<IdentityRole>>(), services.GetRequiredService<UserManager<User>>());
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
