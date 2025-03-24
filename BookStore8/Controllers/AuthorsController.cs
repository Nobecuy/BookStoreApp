 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStore8.Data;
using BookStore8.Model.Author;
using AutoMapper;
using BookStore8.Static;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace BookStore8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AuthorsController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper mapper;
        private readonly ILogger<AuthorsController> logger;

        public AuthorsController(BookStoreDbContext context, IMapper mapper, ILogger<AuthorsController> logger)
        {
            _context = context;
            this.mapper = mapper;
            this.logger = logger;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorReadOnlyDto>>> GetAuthors()
        {
            logger.LogInformation($"Request to {nameof(GetAuthor)}");
            try
            {
                var authors = await _context.Authors.ToListAsync();
                var authorsDtos = mapper.Map<IEnumerable<AuthorReadOnlyDto>>(authors);
                return Ok(authorsDtos);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error perfoming GET in {nameof(GetAuthor)}");
                return StatusCode(500,Messages.Error500Messages);
            }
          
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorReadOnlyDto>> GetAuthor(int id)
        {
            try
            {
                var author = await _context.Authors.FindAsync(id);

                if (author == null)
                {
                    logger.LogWarning($"{nameof(Author)} Record not found in {nameof(GetAuthor)} -ID: {id}");
                    return NotFound();
                }

 
                var authorDto = mapper.Map<AuthorReadOnlyDto>(author);
                return Ok(authorDto);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error perfoming GET in {nameof(GetAuthor)}");
                return StatusCode(500, Messages.Error500Messages);
            }
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Administator")]
        public async Task<IActionResult> PutAuthor(int id, AuthorUpdateDto authorDto)
        {
            if (id != authorDto.Id)
            {
                logger.LogError( $"Updaate ID invalid in {nameof(PutAuthor)} -ID: {id}");
                return BadRequest();
            }

            var author = await _context.Authors.FindAsync(id);

            if (author == null)
            {
                logger.LogError($"{nameof(Author)} Record not dound in {nameof(PutAuthor)} -ID: {id}");
                return NotFound();
            }


             mapper.Map(authorDto ,author);
            _context.Entry(author).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await AuthorExists(id))
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

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Administator")]
        public async Task<ActionResult<AuthorCreateDto>> PostAuthor(AuthorCreateDto authorDto)
        {
            try
            {
                var author = mapper.Map<Author>(authorDto);
                await _context.Authors.AddAsync(author);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author); 
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error perfoming POST in {nameof(PostAuthor)}", authorDto);
                return StatusCode(500, Messages.Error500Messages);
            }
           
        }

        // DELETE: api/Authors/5n
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administator")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            try
            {
                var author = await _context.Authors.FindAsync(id);
                if (author == null)
                {
                    logger.LogWarning($"{nameof(Author)}Record not found in {nameof(DeleteAuthor)} -ID: {id}");
                    return NotFound();
                }

                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error perfoming DELETE in {nameof(DeleteAuthor)}");
                return StatusCode(500, Messages.Error500Messages);
            }
          
        }

        private async Task<bool> AuthorExists(int id)
        {
            return await _context.Authors.AnyAsync(e => e.Id == id);
        }
    }
}
