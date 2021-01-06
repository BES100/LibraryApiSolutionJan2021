using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Domain
{
    public class LibraryDataContext : DbContext
    {
        public LibraryDataContext(DbContextOptions options): base(options)
        {

        }
        public DbSet<Book> Books { get; set; }


        public IQueryable<Book> GetBooksInInvetory()
        {
            return Books.Where(b => b.IsInInventory);
        }
    }
}
