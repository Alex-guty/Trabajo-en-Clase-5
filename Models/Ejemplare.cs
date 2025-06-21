using System;
using System.Collections.Generic;

namespace MyLibrary.Models;

public partial class Ejemplare
{
    public int Id { get; set; }

    public int? LibroId { get; set; }

    public string? Estado { get; set; }

    public virtual Libro? Libro { get; set; }

    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
}
