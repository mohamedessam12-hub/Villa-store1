using Microsoft.AspNetCore.Authentication.Cookies;
using Villa_mvc.Service;
using Villa_mvc.Service.IService;

namespace Villa_mvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddAutoMapper(typeof(MappingConfig));         
            builder.Services.AddScoped<IVillaServics, VillaService>();
            builder.Services.AddScoped<IVillaNumberServics, VillaNumberService>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddHttpClient("MagicVilla", client =>
            {
                client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ServiceUrls:VillaAPI"));
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential= true;
            });
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(optinos =>
            {
                optinos.Cookie.HttpOnly = true;
                optinos.ExpireTimeSpan= TimeSpan.FromMinutes(10);
                optinos.LoginPath = "/User/Login";
                optinos.AccessDeniedPath = "/User/AccessDenied";
                optinos.SlidingExpiration = true;
            });

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
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
