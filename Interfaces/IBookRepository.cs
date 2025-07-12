using MyLibrary.Models;

namespace MyLibrary.Interfaces;

public interface IBookRepository
{
    Task<LibroDto?> GetBookByIdAsync(int id);
    Task<IEnumerable<LibroDto>> SearchBooksByFullNameAsync(string fullName);
    Task<IEnumerable<LibroDto>> SearchBooksByYearRangeAsync(int fromYear, int toYear);
    Task<IEnumerable<LibroDto>> SearchBooksByEditorAsync(string editor);
}
