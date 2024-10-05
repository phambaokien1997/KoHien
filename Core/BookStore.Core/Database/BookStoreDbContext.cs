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
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<BookOrder> BookOrders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<AuthorGenre> AuthorGenres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .Property(b => b.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<OrderDetail>()
                .Property(od => od.TotalPrice)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<BookOrder>()
                .Property(bo => bo.Discount)
                .HasColumnType("decimal(18,2)");

            // Fluent API configurations can be done here
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BookAuthor>()
                .HasKey(ba => new { ba.BookId, ba.AuthorId });

            modelBuilder.Entity<Book>()
               .HasMany(b => b.BookAuthors)
               .WithOne(a => a.Book); // Tạo bảng liên kết BookAuthors

            modelBuilder.Entity<Author>()
               .HasMany(a => a.BookAuthors)
               .WithOne(ba => ba.Author);
            // Cấu hình cho AuthorGenre
            modelBuilder.Entity<AuthorGenre>()
                .ToTable("AuthorGenres")
                .HasKey(ag => new { ag.AuthorId, ag.GenreId });

            modelBuilder.Entity<Author>()
                .HasMany(a => a.AuthorGenres)
                .WithOne(ag => ag.Author);

            modelBuilder.Entity<Genre>()
                .HasMany(g => g.AuthorGenres)
                .WithOne(ag => ag.Genre);


            // Cấu hình mối quan hệ một-nhiều giữa Genre và Book
            modelBuilder.Entity<Genre>()
                .HasMany(g => g.Books)
                .WithOne(b => b.Genre)
                .HasForeignKey(b => b.GenreId);

            modelBuilder.Entity<Publisher>()
               .HasMany(p => p.Books)
               .WithOne(b => b.Publisher)
               .HasForeignKey(b => b.PublisherId);

            modelBuilder.Entity<BookOrder>()
            .HasOne(od => od.Book)
            .WithMany(b => b.BookOrders)
            .HasForeignKey(od => od.BookId);

            modelBuilder.Entity<BookOrder>()
            .HasOne(od => od.OrderDetail)
            .WithMany(bo => bo.BookOrders)
            .HasForeignKey(od => od.OrderDetailID);
        }
    }
}
