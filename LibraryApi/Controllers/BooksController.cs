using AutoMapper;
using LibraryApi.Domain;
using LibraryApi.Models.Books;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;

namespace LibraryApi.Controllers
{
    public class BooksController : ControllerBase
    {
        private readonly LibraryDataContext _context;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _config;

        public BooksController(LibraryDataContext context, IMapper mapper, MapperConfiguration config)
        {
            _context = context;
            _mapper = mapper;
            _config = config;
        }

        [HttpGet("books/{id:int}")]
        public async Task<ActionResult> GetBookById(int id)
        {
            var response = await _context.GetBooksInInvetory()
                .ProjectTo<GetBookDetailsResponse>(_config)
                .Where(b => b.Id == id)
                .SingleOrDefaultAsync();

            if(response == null)
            {
                return NotFound();
            } else
            {
                return Ok(response);
            }
        }

        [HttpGet("books")]
        public async Task<ActionResult> GetAllBooks([FromQuery] string genre = "All")
        {
            var response = new GetBooksResponse();
            var booksQuery = _context.GetBooksInInvetory()
                .ProjectTo<GetBooksResponseItem>(_config);

            if (genre != "All")
            {
                booksQuery = booksQuery.Where(b => b.Genre == genre);
            }
            response.Data = await booksQuery.ToListAsync();
            response.NumberOfBooks = response.Data.Count;
            response.Genre = genre;
            return Ok(response);
        }
    }
}
