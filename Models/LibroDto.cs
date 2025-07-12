using System;

namespace MyLibrary.Models;

public class LibroDto
{
    public int Id { get; set; }
    public string OriginalName { get; set; }
    public string SpanishName { get; set; }
    public string Edition { get; set; }
    public int? Year { get; set; }
    public string Editor { get; set; }
    public string FullName { get; set; }
    public int Antiquity { get; set; }

    public LibroDto(Libro libro)
    {
        Id = libro.Id;
        OriginalName = libro.OriginalName ?? string.Empty;
        SpanishName = libro.SpanishName ?? string.Empty;
        Edition = libro.Edition ?? string.Empty;
        Year = libro.Year;
        Editor = libro.Editor ?? string.Empty;
        FullName = $"{libro.OriginalName} by {libro.Editor}";
        // Calculate antiquity based on publication year
        Antiquity = libro.Year.HasValue ? DateTime.Now.Year - libro.Year.Value : 0;
    }
}
