using Microsoft.EntityFrameworkCore;

namespace BookStore.Core.Database
{
    public class BookStoreDbContext : DbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options)
        : base(options)
        {
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
		public DbSet<Genre> Genres { get; set; }
		public DbSet<Publisher> Publisher { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Fluent API configurations can be done here
            base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Book>()
			   .HasMany(b => b.Authors)
			   .WithMany(a => a.Books)
			   .UsingEntity(j => j.ToTable("BookAuthors")); // Tạo bảng liên kết BookAuthors
			modelBuilder.Entity<Genre>()
			   .HasMany(g => g.Authors)
			   .WithMany(a => a.Genres)
			   .UsingEntity(j => j.ToTable("GenreAuthors"));

			// Cấu hình mối quan hệ một-nhiều giữa Genre và Book
			modelBuilder.Entity<Genre>()
				.HasMany(g => g.Books)
				.WithOne(b => b.Genre)
				.HasForeignKey(b => b.GenreId);
			modelBuilder.Entity<Publisher>()
			   .HasMany(p => p.Books)
			   .WithOne(b => b.Publisher)
			   .HasForeignKey(b => b.PublisherId);
			modelBuilder.Entity<OrderDetail>()
			.HasOne(od => od.Book)
			.WithMany(b => b.OrderDetails)
			.HasForeignKey(od => od.BookId);
			modelBuilder.Entity<OrderDetail>()
			.HasOne(od => od.BookOrder)
			.WithMany(bo => bo.OrderDetails)
			.HasForeignKey(od => od.BookOrderId);
		}
    }
}
