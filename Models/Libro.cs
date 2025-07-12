using System;
using System.Collections.Generic;

namespace MyLibrary.Models;

public partial class Libro
{
    public int Id { get; set; }

    public string? OriginalName { get; set; }

    public string? SpanishName { get; set; }

    public string? Edition { get; set; }

    public int? Year { get; set; }

    public string? Editor { get; set; }

    public virtual ICollection<Ejemplare> Ejemplares { get; set; } = new List<Ejemplare>();
}
