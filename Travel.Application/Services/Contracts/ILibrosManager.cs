using Travel.Application.Dtos.Autores;
using Travel.Application.Dtos.Libros;

namespace Travel.Application.Services.Contracts
{
    public interface ILibrosManager
    {
        public Task<List<LibroDto>> GetAllBooks(CancellationToken cancellationToken);
        public Task<AutorConLibrosDto> GetAllBooksByAuthorId(int AutorId, CancellationToken cancellationToken);
        public Task<bool> AddLibro(AddLibroDto autorDto, CancellationToken cancellationToken);
        public Task<LibroDto> GetLibroById(string LibroId, CancellationToken cancellationToken);
    }
}
