using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Database
{
    public class DataSeeder
    {
        private readonly BookStoreDbContext _context;
        public DataSeeder(BookStoreDbContext context)
        {
            _context = context;
        }
        public async Task SeedDataAsync()
        {
            try
            {
                await SeedBooksAsync();
                await SeedAuthorsAsync();
                await SeedGenresAsync();
                await SeedPublishersAsync();
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception or print it to the console
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        public async Task SeedBooksAsync()
        {
            if (await _context.Books.AnyAsync())
                return;
            var books = new List<Book>()
            {
                new Book()
                {
                    Title = "The Great Gatsby",
                Name = "The Great Gatsby",
                ShortDescription = "A novel set in the Roaring Twenties",
                Price = 10.99m,
                Quantity = 100,
                PublicationDate = new DateTime(1925, 4, 10),
                GenreId = 1,  // Giả định rằng bạn đã có Genre với Id = 1
                PublisherId = 1, // Giả định rằng bạn đã có Publisher với Id = 1
                AuthorId = 1
                },
                new Book()
                {
                    Title = "1984",
                Name = "1984",
                ShortDescription = "Dystopian novel by George Orwell",
                Price = 9.99m,
                Quantity = 150,
                PublicationDate = new DateTime(1949, 6, 8),
                GenreId = 2, // Giả định rằng bạn đã có Genre với Id = 2
                PublisherId = 2, // Giả định rằng bạn đã có Publisher với Id = 2
                AuthorId = 2
                },
                new Book()
                {
                    Title = "To Kill a Mockingbird",
                Name = "To Kill a Mockingbird",
                ShortDescription = "A novel by Harper Lee exploring racial injustice",
                Price = 8.99m,
                Quantity = 200,
                PublicationDate = new DateTime(1960, 7, 11),
                GenreId = 3, // Giả định rằng bạn đã có Genre với Id = 3
                PublisherId = 3, // Giả định rằng bạn đã có Publisher với Id = 3
                AuthorId = 3,
                },
                new Book()
                {
                    Title = "The Hobbit",
                Name = "The Hobbit",
                ShortDescription = "A fantasy novel by J.R.R. Tolkien",
                Price = 11.99m,
                Quantity = 250,
                PublicationDate = new DateTime(1937, 9, 21),
                GenreId = 5, // Giả định rằng bạn đã có Genre với Id = 5
                PublisherId = 5, // Giả định rằng bạn đã có Publisher với Id = 5
                AuthorId =5,
                },
                new Book()
                {
                    Title = "Harry Potter and the Sorcerer's Stone",
                Name = "Harry Potter and the Sorcerer's Stone",
                ShortDescription = "The first book in the Harry Potter series by J.K. Rowling",
                Price = 12.99m,
                Quantity = 300,
                PublicationDate = new DateTime(1997, 6, 26),
                GenreId = 4, // Giả định rằng bạn đã có Genre với Id = 4
                PublisherId = 4, // Giả định rằng bạn đã có Publisher với Id = 4
                AuthorId = 4,
                }
            };
            await _context.Books.AddRangeAsync(books);

        }
        public async Task SeedAuthorsAsync()
        {
            if (await _context.Authors.AnyAsync())
                return;
            var authors = new List<Author>() {
                new Author()
                {
                    Name = "F. Scott Fitzgerald",
                DateOfBirth = new DateTime(1896, 9, 24),
                Gender = true, // Male
                Country = "United States",
                PhoneNumber = "123-456-7890"
                },
                new Author()
                {
                    Name = "George Orwell",
                DateOfBirth = new DateTime(1903, 6, 25),
                Gender = true, // Male
                Country = "United Kingdom",
                PhoneNumber = "234-567-8901"
                },
                new Author()
                {
                     Name = "Harper Lee",
                DateOfBirth = new DateTime(1926, 4, 28),
                Gender = false, // Female
                Country = "United States",
                PhoneNumber = "345-678-9012"
                },
                new Author()
                {
                    Name = "J.K. Rowling",
                DateOfBirth = new DateTime(1965, 7, 31),
                Gender = false, // Female
                Country = "United Kingdom",
                PhoneNumber = "456-789-0123"
                },
                new Author()
                {
                    Name = "J.R.R. Tolkien",
                DateOfBirth = new DateTime(1892, 1, 3),
                Gender = true, // Male
                Country = "United Kingdom",
                PhoneNumber = "567-890-1234"
                },
            };
            await _context.AddRangeAsync(authors);
        }
        public async Task SeedGenresAsync()
        {
            if (await _context.Genres.AnyAsync())
                return;
            var genres = new List<Genre>()
            {
                new Genre()
                {
                    Name = "Classic",
                    Description = "Timeless literature that has transcended generations."

                },
                new Genre()
                {
                    Name = "Dystopian",
                    Description = "Fiction depicting a controlled, often oppressive society."
                },new Genre()
                {

                },new Genre()
                {
                    Name = "Southern Gothic",
                    Description = "A genre that focuses on the Southern United States with elements of horror and decay."
                },new Genre()
                {
                    Name = "Adventure",
                    Description = "Stories involving exciting journeys or escapades, often with physical danger."
                }
            };
            await _context.Genres.AddRangeAsync(genres);
        }
        public async Task SeedPublishersAsync()
        {
            if (await _context.Publishers.AnyAsync())
                return;
            var publishers = new List<Publisher>()
            {
                new Publisher()
                {
                    Name = "Scribner",
                    Description = "An American publisher known for publishing classics.",
                    Address = "123 Publisher St, New York, NY, 10001",
                    ContactEmail = "contact@scribner.com",
                    PhoneNumber = "123-456-7890"
                },
                new Publisher()
                {
                    Name = "Secker & Warburg",
                    Description = "A UK publisher known for a wide range of literature.",
                    Address = "456 Publisher Ave, London, UK, SW1A 1AA",
                    ContactEmail = "info@seckerwarburg.co.uk",
                    PhoneNumber = "234-567-8901"
                },
                new Publisher()
                {
                    Name = "J.B. Lippincott & Co.",
                    Description = "An American publishing house known for literary works.",
                    Address = "789 Publisher Blvd, Philadelphia, PA, 19103",
                    ContactEmail = "support@lippincott.com",
                    PhoneNumber = "345-678-9012"
                },
                new Publisher()
                {
                    Name = "Allen & Unwin",
                    Description = "An Australian publishing company.",
                    Address = "101 Publisher Rd, Sydney, NSW, 2000",
                    ContactEmail = "contact@allenandunwin.com",
                    PhoneNumber = "456-789-0123"
                },
                new Publisher()
                {
                    Name = "Bloomsbury",
                    Description = "A leading UK publisher, known for contemporary fiction.",
                    Address = "202 Publisher Ln, London, UK, EC1A 1BB",
                    ContactEmail = "info@bloomsbury.com",
                    PhoneNumber = "567-890-1234"
                }
            };
            await _context.Publishers.AddRangeAsync(publishers);
        }
    }
}
