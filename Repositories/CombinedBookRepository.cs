using Microsoft.EntityFrameworkCore;
using MyLibrary.Interfaces;
using MyLibrary.Models;
using System.Text.Json;

namespace MyLibrary.Repositories;

public class CombinedBookRepository : ICombinedBookRepository
{
    private readonly AdventureWorksContext _context;
    private readonly string _jsonFilePath;
    
    public CombinedBookRepository(AdventureWorksContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _jsonFilePath = Path.Combine(environment.ContentRootPath, "Data", "books.json");
    }

    public async Task<IEnumerable<LibroDto>> GetAllBooksFromSqlAsync()
    {
        var libros = await _context.Libros.ToListAsync();
        return libros.Select(l => new LibroDto(l));
    }

    public async Task<IEnumerable<LibroDto>> GetAllBooksFromJsonAsync()
    {
        if (File.Exists(_jsonFilePath))
        {
            var jsonContent = await File.ReadAllTextAsync(_jsonFilePath);
            var books = JsonSerializer.Deserialize<List<Libro>>(jsonContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<Libro>();
            
            return books.Select(b => new LibroDto(b));
        }
        
        return new List<LibroDto>();
    }

    public async Task<IEnumerable<LibroDto>> GetAllBooksFromBothSourcesAsync()
    {
        var sqlBooks = await _context.Libros.ToListAsync();
        var jsonBooks = new List<Libro>();
        
        if (File.Exists(_jsonFilePath))
        {
            var jsonContent = await File.ReadAllTextAsync(_jsonFilePath);
            jsonBooks = JsonSerializer.Deserialize<List<Libro>>(jsonContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<Libro>();
        }
        
        // Combine both sources, avoiding duplicates based on ID
        var allBooks = new List<Libro>();
        
        // Add SQL books first
        allBooks.AddRange(sqlBooks);
        
        // Add JSON books that don't exist in SQL (different IDs)
        foreach (var jsonBook in jsonBooks)
        {
            if (!allBooks.Any(b => b.Id == jsonBook.Id))
            {
                allBooks.Add(jsonBook);
            }
        }
        
        return allBooks.OrderBy(b => b.Id).Select(l => new LibroDto(l));
    }
}
