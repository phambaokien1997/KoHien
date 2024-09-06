using BookStore.Core.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var migrationAssembly = typeof(BookStoreDbContext).Assembly.GetName().Name;
services.AddDbContext<BookStoreDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
		sqlOptions => sqlOptions.MigrationsAssembly(migrationAssembly)));

// Add services to the container.
builder.Services.AddRazorPages();
 // o day ne, cái ni add mấy hồi, demo để ông hiểu cái dependency injection thôi chớ
 // cũng đã xài đéo đâu
 // vcc chừ t ui tạo lại cái bookstore web làm y chang ri à ?
 // mẹ, cái vụ migration làm xong rồi, thì commit lên đã, ưng sửa chi tiếp thì sửa, code vẫn còn đó, chớ xóa rồi hồi than mất code ăn lồn à
 // vcc ông commit chưa hay để tui commit?// mới commit đó ko thấy hà ê mà tui hỏi ni cái ni để làm chi ?
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
	var dbContext = scope.ServiceProvider.GetRequiredService<BookStoreDbContext>();
	dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
// đĩ mẹ chỉ vì 1 cai lam ko dc mà đạp đổ 9 cái làm đc rồi. đéo có cái ngu mô bằng cái ngu ni, ngu vccc oong chua xoa cai web kia dm cai no la quan trong nhat a roi may cai dang ky repository hoi chieu dau ???
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
