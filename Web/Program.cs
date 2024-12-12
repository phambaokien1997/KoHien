using BookStore.Core.Database;
using BookStore.Core.Repositories;
using BookStore.Core.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var migrationAssembly = typeof(BookStoreDbContext).Assembly.GetName().Name;
services.AddDbContext<BookStoreDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
		sqlOptions => sqlOptions.MigrationsAssembly(migrationAssembly)).LogTo(Console.WriteLine, LogLevel.Information) // Log SQL to console
           .EnableSensitiveDataLogging()) ;

// Add services to the container.
services.AddScoped<IBookService, BookService>();
services.AddScoped<IAuthorService, AuthorService>();
services.AddScoped<IGenreService, GenreService>();
services.AddScoped<IPublisherService, PublisherService>();
services.AddScoped<IAuthorRepository, AuthorRepository>();
services.AddScoped<IBookRepository, BookRepository>();
services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});
services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oidc";
})
.AddCookie("Cookies")
.AddOpenIdConnect("oidc", options =>
{
    options.Authority = "https://localhost:5001"; // IdentityServer URL
    options.ClientId = "mvc_client";
    //options.ClientSecret = "secret"; // Add your client secret if required
    options.ResponseType = "code"; // Use Authorization Code flow

    options.SaveTokens = true; // Save tokens for API calls
    options.Scope.Add("openid"); // Standard OpenID scopes
    options.Scope.Add("profile");
    options.Scope.Add("api1"); // Add API scopes as needed

    options.GetClaimsFromUserInfoEndpoint = true; // Fetch additional claims
});

builder.Services.AddRazorPages();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
	var dbContext = scope.ServiceProvider.GetRequiredService<BookStoreDbContext>();
	dbContext.Database.Migrate();
    // Khởi tạo DataSeeder và gọi phương thức SeedDataAsync
    var dataSeeder = new DataSeeder(dbContext);
    await dataSeeder.SeedDataAsync();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCors("AllowAllOrigins"); // Áp dụng chính sách CORS

app.UseAuthorization();

//app.MapRazorPages(); 
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
