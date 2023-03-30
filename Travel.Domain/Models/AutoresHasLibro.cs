using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Travel.Domain.Models
{
    [Table("Autores_has_libros")]
    public class AutoresHasLibro
    {
        
        public int AutorId { get; set; }
        public string Isbn { get; set; }


        public Autor Autor { get; set; }
        public Libro Libro { get; set; }

    }

}
