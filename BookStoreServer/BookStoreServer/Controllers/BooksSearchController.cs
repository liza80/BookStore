using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using WebApplication15.Models;

namespace WebApplication15.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize] // Allow access to registered users (User and Admin)
    public class BooksSearchController : ControllerBase
    {
        private readonly BooksStoreContext _context;

        public BooksSearchController(BooksStoreContext context)
        {
            _context = context;
        }

        // GET: api/books/search
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Book>>> SearchBooks(
            [FromQuery] string queryParameters)
        {
            var booksQuery = _context.Books.AsQueryable();

            if (!string.IsNullOrEmpty(queryParameters))
            {
                booksQuery = booksQuery.Where(b =>
                    b.Title.Contains(queryParameters) ||
                    b.Author.Contains(queryParameters) ||
                    b.Genre.Contains(queryParameters));
            }

            //if (!string.IsNullOrEmpty(queryParameters.Genre))
            //{
            //    booksQuery = booksQuery.Where(b => b.Genre.Equals(queryParameters.Genre, StringComparison.OrdinalIgnoreCase));
            //}

            //if (queryParameters.MinPrice.HasValue)
            //{
            //    booksQuery = booksQuery.Where(b => b.Price >= queryParameters.MinPrice.Value);
            //}

            //if (queryParameters.MaxPrice.HasValue)
            //{
            //    booksQuery = booksQuery.Where(b => b.Price <= queryParameters.MaxPrice.Value);
            //}

            //if (queryParameters.PublishedAfter.HasValue)
            //{
            //    booksQuery = booksQuery.Where(b => b.PublishedDate >= queryParameters.PublishedAfter.Value);
            //}

            //if (queryParameters.PublishedBefore.HasValue)
            //{
            //    booksQuery = booksQuery.Where(b => b.PublishedDate <= queryParameters.PublishedBefore.Value);
            //}

            var books = await booksQuery.ToListAsync();
            return Ok(books);
        }

        // Other CRUD methods here
    }
    public class BookQueryParameters
    {
        public string SearchTerm { get; set; }
        public string Genre { get; set; }
        public Double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
        public DateTime? PublishedAfter { get; set; }
        public DateTime? PublishedBefore { get; set; }
    }
}
