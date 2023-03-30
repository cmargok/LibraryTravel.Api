using System.ComponentModel.DataAnnotations;

namespace Travel.Application.Dtos.Libros
{
    public class AddLibroDto : LibroDto
    {
        [Required]
        public int AutorId { get; set; }

        [Required]
        public int EditorialId { get; set; }    
    }



}
