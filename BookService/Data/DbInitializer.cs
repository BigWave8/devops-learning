using BookService.Models;

namespace BookService.Data
{
    public static class DbInitializer
    {
        public static void Seed(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<BookDbContext>();

            context.Database.EnsureCreated();

            if (!context.Books.Any())
            {
                context.Books.AddRange(
                    new Book { Title = "Clean Code", Author = "1", Year = 2010, Price = 600 },
                    new Book { Title = "Domain-Driven Design", Author = "2", Year = 2020, Price = 750 },
                    new Book { Title = "The Pragmatic Programmer", Author = "3", Year = 2025, Price = 550 }
                );

                context.SaveChanges();
            }
        }
    }
}