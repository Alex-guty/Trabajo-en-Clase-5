using System;
using System.Collections.Generic;

namespace MyLibrary.Models;

public partial class Estudiante
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
}
