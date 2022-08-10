using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webApi5;
using webApi5.Model;

namespace webApi5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class booksController : ControllerBase
    {
        private readonly BookDemoDbContext _context;

        public booksController(BookDemoDbContext context)
        {
            _context = context;
        }

        #region GET METHOD

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<books>>> GetBookDetails()
        {
            if (_context.BookDetails == null)
            {
                return NotFound();
            }
            return await _context.BookDetails.ToListAsync();
        }
        #endregion

        #region SEARCH METHOD

        [HttpGet("{searchString}")]
        public async Task<IActionResult> ShowOneBook(string searchString)
        {
            if (searchString == null)
            {
                return BadRequest("input can't be null");
            }
            if (_context.BookDetails == null)
            {
                return NotFound("Table doesn't exists");
            }
            var book = await _context.BookDetails.Where(e => e.BookName.Contains(searchString) || e.Zoner.Contains(searchString)).ToListAsync();
            if (book == null)
            {
                return NotFound("Record doesn't exists");
            }
            return Ok(book);
        }

        //// GET: api/Books
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Book>> GetBook(int id)
        //{
        //  if (_context.BookDetails == null)
        //  {
        //      return NotFound();
        //  }
        //    var book = await _context.BookDetails.FindAsync(id);

        //    if (book == null)
        //    {
        //        return NotFound();
        //    }

        //    return book;
        //}

        #endregion

        #region PUT METHOD

        // PUT: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, books book)
        {
            if (id != book.bookID)
            {
                return BadRequest();
            }

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        #endregion

        #region POST METHOD

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<books>> PostBook(books book)
        {
            if (_context.BookDetails == null)
            {
                return Problem("Entity set 'BookDbContext.BookDetails'  is null.");
            }
            _context.BookDetails.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBook", new { id = book.bookID }, book);
        }

        #endregion

        #region DELETE METHOD

        // DELETE: api/Books
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            if (_context.BookDetails == null)
            {
                return NotFound();
            }
            var book = await _context.BookDetails.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.BookDetails.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        #endregion
        private bool BookExists(int id)
        {
            return (_context.BookDetails?.Any(e => e.bookID == id)).GetValueOrDefault();
        }
    }
}