using System;
using System.Collections.Generic;

namespace MyLibrary.Models;

public partial class Prestamo
{
    public int Id { get; set; }

    public int? EstudianteId { get; set; }

    public int? EjemplarId { get; set; }

    public DateOnly? FechaPrestamo { get; set; }

    public DateOnly? FechaDevolucion { get; set; }

    public virtual Ejemplare? Ejemplar { get; set; }

    public virtual Estudiante? Estudiante { get; set; }
}
