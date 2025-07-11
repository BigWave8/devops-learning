using BookService.Data;
using BookService.Grpc;
using BookService.Models;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BookDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure()
    )
);

builder.Services.AddGrpc();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(80, listen =>
    {
        listen.Protocols = HttpProtocols.Http1AndHttp2;
    });

    //options.ListenAnyIP(5000, listen => listen.Protocols = HttpProtocols.Http1); // Swagger + Controllers
    //options.ListenAnyIP(5001, listen => listen.Protocols = HttpProtocols.Http2); // gRPC
});


var app = builder.Build();

//if (app.Environment.IsProduction())
{
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<BookDbContext>();
        try
        {
            Console.WriteLine("Applying migrations...");
            context.Database.Migrate();
            Console.WriteLine("Migrations applied successfully!");

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
        catch (Exception ex)
        {
            Console.WriteLine($"Error applying migrations: {ex.Message}");
            throw;
        }
    }
}

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGrpcService<BookGrpcService>();
app.MapGet("/", () => "This service uses gRPC. Use a gRPC client to communicate.");

//DbInitializer.Seed(app);

app.Run();
