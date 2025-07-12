using MyLibrary.Models;

namespace MyLibrary.Interfaces;

public interface ICombinedBookRepository
{
    Task<IEnumerable<LibroDto>> GetAllBooksFromSqlAsync();
    Task<IEnumerable<LibroDto>> GetAllBooksFromJsonAsync();
    Task<IEnumerable<LibroDto>> GetAllBooksFromBothSourcesAsync();
}
