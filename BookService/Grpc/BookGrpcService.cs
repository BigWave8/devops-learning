using BookService.Data;
using Contracts.Protos;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace BookService.Grpc
{
    public class BookGrpcService : BookProtoService.BookProtoServiceBase
    {
        private readonly BookDbContext _context;

        public BookGrpcService(BookDbContext context)
        {
            _context = context;
        }

        public override async Task<BookResponse> GetBookById(BookRequest request, ServerCallContext context)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == request.Id) 
                ?? throw new RpcException(new Status(StatusCode.NotFound, "Book not found"));

            return new BookResponse
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Year = book.Year,
                Price = book.Price
            };
        }

        public override async Task<BooksResponse> GetAllBooks(Empty request, ServerCallContext context)
        {
            var books = await _context.Books.ToListAsync();
            var response = new BooksResponse();
            response.Books.AddRange(books.Select(b => new BookResponse
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                Year = b.Year,
                Price = b.Price
            }));
            return response;
        }
    }
}
