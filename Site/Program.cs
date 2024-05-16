using BusinessAccessLayer.Abstraction;
using BusinessAccessLayer.Implementation;
using BusinessAccessLayer.Implemention;
using DataAccessLayer.DbServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ModelAccessLayer.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(option => { option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); });
builder.Services.AddIdentity<ApplicationUser , IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
builder.Services.AddScoped<IAccountServices,AccountServices>();
builder.Services.AddScoped<IAdminServices, AdminServices>();
builder.Services.AddScoped<IProductServices, ProductServices>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IOrderServices, OrderServices>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=LoginAdmin}/{id?}");

app.Run();
