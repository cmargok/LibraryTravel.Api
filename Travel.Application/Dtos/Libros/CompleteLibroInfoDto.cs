using Travel.Application.Dtos.Autores;
using Travel.Application.Dtos.Editoriales;

namespace Travel.Application.Dtos.Libros
{
    public class CompleteLibroInfoDto: LibroDto
    {
        public EditorialBasicDto Editorial { get; set; }
        public AutorBasicDto Autor { get; set; }
    }
}
