using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShopingStore.Data;
using ShopingStore.Models;
using ShopingStore.Repository;
using ShopingStore.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddCookiePolicy(options =>
{

});

builder.Services.AddDbContext<MyDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("myConn"), builder => builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null));
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<MyDBContext>().AddDefaultTokenProviders();

builder.Services.AddScoped(typeof(IMainService<Product>), typeof(ProductService));
builder.Services.AddScoped(typeof(IMainService<Category>), typeof(CategoryService));
builder.Services.AddScoped(typeof(IFilterService<Product>),typeof(FilterProduct));

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
// builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//                 .AddCookie();


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
app.UseSession();
app.UseCookiePolicy();

app.UseAuthorization();

// app.UseEndpoints(endpoint =>
// {
//     endpoint.MapAreaControllerRoute(
//         name: "Admin",
//         areaName: "Admin",
//         pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
// });


app.MapAreaControllerRoute(
         name: "Admin",
        areaName: "Admin",
        pattern: "{Admin}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
