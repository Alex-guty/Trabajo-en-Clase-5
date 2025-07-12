using Microsoft.EntityFrameworkCore;
using MyLibrary.Models;

namespace MyLibrary.Extensions;

public static class DatabaseSeeding
{
    public static async Task SeedDatabaseAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AdventureWorksContext>();
        
        // Ensure database is created
        await context.Database.EnsureCreatedAsync();
        
        // Check if we already have books
        if (await context.Libros.AnyAsync())
        {
            return; // Database already seeded
        }
        
        // Add 10 books with correct English/Spanish mapping
        var books = new[]
        {
            new Libro { Id = 1, OriginalName = "Don Quixote", SpanishName = "El Quijote", Edition = "1st Edition", Year = 1605, Editor = "Miguel de Cervantes" },
            new Libro { Id = 2, OriginalName = "One Hundred Years of Solitude", SpanishName = "Cien años de soledad", Edition = "1st Edition", Year = 1967, Editor = "Gabriel García Márquez" },
            new Libro { Id = 3, OriginalName = "The House of the Spirits", SpanishName = "La casa de los espíritus", Edition = "1st Edition", Year = 1982, Editor = "Isabel Allende" },
            new Libro { Id = 4, OriginalName = "Hopscotch", SpanishName = "Rayuela", Edition = "1st Edition", Year = 1963, Editor = "Julio Cortázar" },
            new Libro { Id = 5, OriginalName = "Fictions", SpanishName = "Ficciones", Edition = "1st Edition", Year = 1944, Editor = "Jorge Luis Borges" },
            new Libro { Id = 6, OriginalName = "Pedro Páramo", SpanishName = "Pedro Páramo", Edition = "1st Edition", Year = 1955, Editor = "Juan Rulfo" },
            new Libro { Id = 7, OriginalName = "The Time of the Hero", SpanishName = "La ciudad y los perros", Edition = "1st Edition", Year = 1963, Editor = "Mario Vargas Llosa" },
            new Libro { Id = 8, OriginalName = "The Labyrinth of Solitude", SpanishName = "El laberinto de la soledad", Edition = "1st Edition", Year = 1950, Editor = "Octavio Paz" },
            new Libro { Id = 9, OriginalName = "On Heroes and Tombs", SpanishName = "Sobre héroes y tumbas", Edition = "1st Edition", Year = 1961, Editor = "Ernesto Sabato" },
            new Libro { Id = 10, OriginalName = "The Tunnel", SpanishName = "El túnel", Edition = "1st Edition", Year = 1948, Editor = "Ernesto Sabato" }
        };
        
        await context.Libros.AddRangeAsync(books);
        await context.SaveChangesAsync();
        
        Console.WriteLine($"Database seeded with {books.Length} books.");
    }
}
