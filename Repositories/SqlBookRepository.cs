using Microsoft.EntityFrameworkCore;
using MyLibrary.Interfaces;
using MyLibrary.Models;

namespace MyLibrary.Repositories;

public class SqlBookRepository : IBookRepository
{
    private readonly AdventureWorksContext _context;

    public SqlBookRepository(AdventureWorksContext context)
    {
        _context = context;
    }

    public async Task<LibroDto?> GetBookByIdAsync(int id)
    {
        var libro = await _context.Libros.FindAsync(id);
        return libro != null ? new LibroDto(libro) : null;
    }

    public async Task<IEnumerable<LibroDto>> SearchBooksByFullNameAsync(string fullName)
    {
        var libros = await _context.Libros
            .Where(l => (l.OriginalName != null && l.OriginalName.Contains(fullName)) ||
                       (l.SpanishName != null && l.SpanishName.Contains(fullName)))
            .ToListAsync();
        
        return libros.Select(l => new LibroDto(l));
    }

    public async Task<IEnumerable<LibroDto>> SearchBooksByYearRangeAsync(int fromYear, int toYear)
    {
        var libros = await _context.Libros
            .Where(l => l.Year >= fromYear && l.Year <= toYear)
            .ToListAsync();
        
        return libros.Select(l => new LibroDto(l));
    }

    public async Task<IEnumerable<LibroDto>> SearchBooksByEditorAsync(string editor)
    {
        var libros = await _context.Libros
            .Where(l => l.Editor != null && l.Editor.Contains(editor))
            .ToListAsync();
        
        return libros.Select(l => new LibroDto(l));
    }
}
