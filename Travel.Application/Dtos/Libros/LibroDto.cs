using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Models;

namespace Travel.Application.Dtos.Libros
{
    public class LibroDto
    {
        [StringLength(13)]
        [Required]
        public string Isbn { get; set; }
    
        [StringLength(45)]
        [Required]
        public string Titulo { get; set; } = String.Empty;
 
        public string Sinopsis { get; set; } = String.Empty;

        [StringLength(45)]
        [Required]
        public string NPaginas { get; set; } = String.Empty;

    }



}
