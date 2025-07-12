using Microsoft.AspNetCore.Mvc;
using MyLibrary.Interfaces;
using MyLibrary.Models;

namespace MyLibrary.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksApiController : ControllerBase
{
    private readonly IBookRepository _bookRepository;

    public BooksApiController(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LibroDto>> GetBook(int id)
    {
        var book = await _bookRepository.GetBookByIdAsync(id);
        if (book == null)
        {
            return NotFound();
        }
        return Ok(book);
    }

    [HttpGet("search/fullname")]
    public async Task<ActionResult<IEnumerable<LibroDto>>> SearchByFullName([FromQuery] string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
        {
            return BadRequest("FullName parameter is required");
        }

        var books = await _bookRepository.SearchBooksByFullNameAsync(fullName);
        return Ok(books);
    }

    [HttpGet("search/editor")]
    public async Task<ActionResult<IEnumerable<LibroDto>>> SearchByEditor([FromQuery] string editor)
    {
        if (string.IsNullOrWhiteSpace(editor))
        {
            return BadRequest("Editor parameter is required");
        }

        var books = await _bookRepository.SearchBooksByEditorAsync(editor);
        return Ok(books);
    }

    [HttpGet("search/yearrange")]
    public async Task<ActionResult<IEnumerable<LibroDto>>> SearchByYearRange([FromQuery] int fromYear, [FromQuery] int toYear)
    {
        if (fromYear <= 0 || toYear <= 0 || fromYear > toYear)
        {
            return BadRequest("Valid fromYear and toYear parameters are required");
        }

        var books = await _bookRepository.SearchBooksByYearRangeAsync(fromYear, toYear);
        return Ok(books);
    }
}
