using Microsoft.AspNetCore.Mvc;
using MyLibrary.Interfaces;
using MyLibrary.Models;

namespace MyLibrary.Controllers;

public class BooksController : Controller
{
    private readonly IBookRepository _bookRepository;
    private readonly ICombinedBookRepository _combinedBookRepository;

    public BooksController(IBookRepository bookRepository, ICombinedBookRepository combinedBookRepository)
    {
        _bookRepository = bookRepository;
        _combinedBookRepository = combinedBookRepository;
    }

    public async Task<IActionResult> Index(int? id = null, string searchFullName = "", string searchEditor = "", int? fromYear = null, int? toYear = null)
    {
        IEnumerable<LibroDto> books;

        if (id.HasValue)
        {
            var book = await _bookRepository.GetBookByIdAsync(id.Value);
            books = book != null ? new List<LibroDto> { book } : new List<LibroDto>();
            ViewBag.SearchId = id.Value;
        }
        else if (!string.IsNullOrWhiteSpace(searchFullName))
        {
            books = await _bookRepository.SearchBooksByFullNameAsync(searchFullName);
            ViewBag.SearchFullName = searchFullName;
        }
        else if (!string.IsNullOrWhiteSpace(searchEditor))
        {
            books = await _bookRepository.SearchBooksByEditorAsync(searchEditor);
            ViewBag.SearchEditor = searchEditor;
        }
        else if (fromYear.HasValue && toYear.HasValue)
        {
            books = await _bookRepository.SearchBooksByYearRangeAsync(fromYear.Value, toYear.Value);
            ViewBag.FromYear = fromYear;
            ViewBag.ToYear = toYear;
        }
        else
        {
            books = new List<LibroDto>();
        }

        return View(books);
    }

    public async Task<IActionResult> Details(int id)
    {
        var book = await _bookRepository.GetBookByIdAsync(id);
        if (book == null)
        {
            return NotFound();
        }
        return View(book);
    }

    public async Task<IActionResult> AllSources()
    {
        var allBooks = await _combinedBookRepository.GetAllBooksFromBothSourcesAsync();
        ViewBag.ShowingAllSources = true;
        ViewBag.BookCount = allBooks.Count();
        return View("Index", allBooks);
    }

    public async Task<IActionResult> SqlOnly()
    {
        var sqlBooks = await _combinedBookRepository.GetAllBooksFromSqlAsync();
        ViewBag.ShowingSqlOnly = true;
        ViewBag.BookCount = sqlBooks.Count();
        return View("Index", sqlBooks);
    }

    public async Task<IActionResult> JsonOnly()
    {
        var jsonBooks = await _combinedBookRepository.GetAllBooksFromJsonAsync();
        ViewBag.ShowingJsonOnly = true;
        ViewBag.BookCount = jsonBooks.Count();
        return View("Index", jsonBooks);
    }
}
