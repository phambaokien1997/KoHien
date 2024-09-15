using BookStore.Core.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BookStore.Web
{
    public class BookStoreDbContextFactory : IDesignTimeDbContextFactory<BookStoreDbContext>
{
    public BookStoreDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<BookStoreDbContext>();
        optionsBuilder.UseSqlServer("Server=DESKTOP-4QUI8GB;Database=BookStoreDb;Integrated Security=True;TrustServerCertificate=True");

        return new BookStoreDbContext(optionsBuilder.Options);
    }
}

}
