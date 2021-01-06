using LibraryApi.Domain;
using LibraryApi.Models.Books;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Controllers
{
    public class BooksController : ControllerBase
    {
        private readonly LibraryDataContext _context;

        public BooksController(LibraryDataContext context)
        {
            _context = context;
        }

        [HttpGet("books")]
        public async Task<ActionResult> GetAllBooks()
        {
            var response = new GetBooksResponse();
            var books = await _context.Books
                .Where(b => b.IsInInventory)
                .Select(b => new GetBooksResponseItem
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    Genre = b.Genre
                })
                .ToListAsync();

            response.Data = books;
            response.NumberOfBooks = books.Count;
            return Ok(response);
        }
    }
}
