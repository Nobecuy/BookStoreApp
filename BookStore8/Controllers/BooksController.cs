using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStore8.Data;
using AutoMapper;
using BookStore8.Model.Book;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace BookStore8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BooksController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper mapper;

        public BooksController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper; 
        } 

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookReadOnlyDto>>> GetBooks()
        {
            var booksDtos = await _context.Books
                .Include(q => q.Author)
                .ProjectTo<BookReadOnlyDto>(mapper.ConfigurationProvider)
                .ToListAsync();
            return Ok(booksDtos);
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDetailDto>> GetBook(int id)
        {
            var book = await _context.Books
                .Include(q => q.Author)
                .ProjectTo<BookDetailDto>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(q => q.Id == id);

            if (book == null)
            {
                return NotFound("Book Not Fuound");
            }

            return book;
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutBook(int id, BookUpdateDto bookDto)
        {
            if (id != bookDto.Id)
            {
                return BadRequest("Id tidak sesuai");
            } 

            var dataLama = await _context.Books.FindAsync(id);

            if (dataLama == null)
            {
                return NotFound("Book Not Found");
            }

            //cek apakah title kembar
            if(bookDto.Title != dataLama.Title)
            {
                bool apakahBukuAda = await _context.Books.AnyAsync(x => x.Title == bookDto.Title);

                if (apakahBukuAda)
                    return BadRequest("Title telah digunakan");
            }

            //cek apakah isbn kembar
            if(bookDto.Isbn != dataLama.Isbn)
            {
                var isExist = await _context.Books.AnyAsync(x => x.Isbn == bookDto.Isbn);

                if (isExist)
                    return BadRequest("ISBN telah digunakan");
            }

            mapper.Map(bookDto, dataLama);
            _context.Entry(dataLama).State = EntityState.Modified;

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

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<Book>> PostBook(BookCreateDto bookDto)
        {
            
            //cek apakah title kembar
            var isExist = await _context.Books.AnyAsync(x => x.Title == bookDto.Title);

            if (isExist)
                return BadRequest("Title telah digunakan");

            //cek apakah isbn kembar
            isExist = await _context.Books.AnyAsync(x => x.Isbn == bookDto.Isbn);

            if (isExist)
                return BadRequest("ISBN telah digunakan");

            var book = mapper.Map<Book>(bookDto);
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBook", new { id = book.Id }, book);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound("Buku Tidak Di temukan");
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
