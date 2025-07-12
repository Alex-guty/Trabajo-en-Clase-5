using System.Text.Json;
using MyLibrary.Interfaces;
using MyLibrary.Models;

namespace MyLibrary.Repositories;

public class JsonBookRepository : IBookRepository
{
    private readonly string _jsonFilePath;
    private List<Libro>? _books;

    public JsonBookRepository(IWebHostEnvironment environment)
    {
        _jsonFilePath = Path.Combine(environment.ContentRootPath, "Data", "books.json");
    }

    private async Task<List<Libro>> LoadBooksAsync()
    {
        if (_books == null)
        {
            if (File.Exists(_jsonFilePath))
            {
                var jsonContent = await File.ReadAllTextAsync(_jsonFilePath);
                _books = JsonSerializer.Deserialize<List<Libro>>(jsonContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<Libro>();
            }
            else
            {
                _books = new List<Libro>();
            }
        }
        return _books;
    }

    public async Task<LibroDto?> GetBookByIdAsync(int id)
    {
        var books = await LoadBooksAsync();
        var libro = books.FirstOrDefault(b => b.Id == id);
        return libro != null ? new LibroDto(libro) : null;
    }

    public async Task<IEnumerable<LibroDto>> SearchBooksByFullNameAsync(string fullName)
    {
        var books = await LoadBooksAsync();
        var filteredBooks = books.Where(b => (b.OriginalName != null && b.OriginalName.Contains(fullName, StringComparison.OrdinalIgnoreCase)) ||
                                             (b.SpanishName != null && b.SpanishName.Contains(fullName, StringComparison.OrdinalIgnoreCase)));
        
        return filteredBooks.Select(b => new LibroDto(b));
    }

    public async Task<IEnumerable<LibroDto>> SearchBooksByYearRangeAsync(int fromYear, int toYear)
    {
        var books = await LoadBooksAsync();
        var filteredBooks = books.Where(b => b.Year >= fromYear && b.Year <= toYear);
        
        return filteredBooks.Select(b => new LibroDto(b));
    }

    public async Task<IEnumerable<LibroDto>> SearchBooksByEditorAsync(string editor)
    {
        var books = await LoadBooksAsync();
        var filteredBooks = books.Where(b => b.Editor != null && b.Editor.Contains(editor, StringComparison.OrdinalIgnoreCase));
        
        return filteredBooks.Select(b => new LibroDto(b));
    }
}
