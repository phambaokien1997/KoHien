using BookStore.IdentityServer.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Reflection;
using System;
using System.Security.Cryptography.X509Certificates;

namespace BookStore.IdentityServer
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }
        public IConfiguration _configuration { get; }
        public Startup(IWebHostEnvironment environment, IConfiguration configuration)

        {
            _configuration = configuration;
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

            // Configure Identity
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                // Chính sách mật khẩu
                options.Password.RequireDigit = false;            // Yêu cầu số
                options.Password.RequiredLength = 3;            // Độ dài tối thiểu
                options.Password.RequireNonAlphanumeric = false; // Yêu cầu ký tự đặc biệt
                options.Password.RequireUppercase = false;       // Yêu cầu chữ hoa
                options.Password.RequireLowercase = false;       // Yêu cầu chữ thường
                options.ClaimsIdentity.UserIdClaimType = "sub";
            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            // Configure IdentityServer without AddAspNetIdentity
            string assemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var certificate = new X509Certificate2($"{assemblyLocation}/Certificate/localhost.pfx", "_helloWorld162");

            // Cấu hình IdentityServer để sử dụng chứng chỉ tự ký
            services.AddIdentityServer()
                .AddSigningCredential(certificate)
                .AddInMemoryClients(Config.Clients) // Configure clients
                .AddInMemoryApiScopes(Config.ApiScopes) // Configure API scopes
                .AddInMemoryIdentityResources(Config.IdentityResources) // Configure identity resources
                .AddProfileService<CustomProfileService>(); // Link IdentityServer to ASP.NET Identity

            services.AddAuthentication("Cookies")
                    .AddCookie("Cookies", options =>
                    {
                        options.LoginPath = "/Account/Login"; // Redirect to login page
                    });

            services.AddAuthorization();

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dbcontext = scope.ServiceProvider.GetService<AppDbContext>();
                dbcontext.Database.Migrate();
            }
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();
             app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
