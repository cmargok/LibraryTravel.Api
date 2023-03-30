using Travel.Application.Dtos.Libros;

namespace Travel.Application.Dtos.Autores
{
    public class AutorConLibrosDto : AutorBasicDto
    {
        public List<LibroEditorial> Libros { get; set; }
    }
}
