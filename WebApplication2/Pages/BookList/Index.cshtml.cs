using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Model;

namespace WebApplication2.Pages.BookList
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Book> Books
        { get; set; }


        public async Task OnGet()
        {
            Books = await _db.Book.ToListAsync();
        }


        public async Task<IActionResult> OnPostDelete(int id) //I actionResult işlem sonrası aynı sayfaya yönlendiriyor. o yuzden ekledik
        {
            var book = await _db.Book.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }
            _db.Book.Remove(book);
            await _db.SaveChangesAsync();

            return RedirectToPage("Index");

        }
    }
}