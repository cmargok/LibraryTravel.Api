using Travel.Application.Dtos.Autores;
using Travel.Application.Dtos.Editoriales;

namespace Travel.Application.Services.Contracts
{
    public interface IAutorManager
    {
        public Task<AutorDto> GetAuthorInfoById(int AutorId, CancellationToken cancellationToken);

        public Task<List<AutorDto>> GetAutores(CancellationToken cancellationToken);
        public Task<bool> AddAutor(AutorBasicDto autorDto, CancellationToken cancellationToken);
    }
}
