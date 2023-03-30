using Travel.Application.Dtos.Autores;
using Travel.Application.Dtos.Editoriales;
using Travel.Application.Infra_Contracts;
using Travel.Application.Services.Contracts;
using Travel.Domain.Models;

namespace Travel.Application.Services
{
    public class AutorManager : IAutorManager
    {
        private readonly IAutorRepository _autorRepository;
        public AutorManager(IAutorRepository autorRepository)
        {
            _autorRepository = autorRepository;
        }

        public async Task<bool> AddAutor(AutorBasicDto autorDto, CancellationToken cancellationToken)
        {
            if (autorDto == null) throw new ArgumentNullException();

            var autorEntity = new Autor()
            {
               Apellido = autorDto.Apellido,
               Nombre = autorDto.Nombre,
            };

            return await _autorRepository.AddAsync(autorEntity, cancellationToken);
        }

        public async Task<AutorDto> GetAuthorInfoById(int AutorId, CancellationToken cancellationToken)
        {
            var autor = await _autorRepository.GetbyIdAsync(AutorId,cancellationToken);

            if(autor != null){
                return new AutorDto
                {
                    Apellido = autor.Apellido,
                    AutorId = AutorId,
                    Nombre = autor.Nombre,
                };
            }

            return null!;
        }

        public async Task<List<AutorDto>> GetAutores(CancellationToken cancellationToken)
        {
            var autores = await _autorRepository.GetAllAsync(cancellationToken);

            if (autores == null || autores.Count == 0)
            {
                return new List<AutorDto>();
            }

            return autores.Select(e => new AutorDto
            {
              Nombre=e.Nombre,
              Apellido=e.Apellido,
              AutorId = e.AutorId
            }).ToList();

        }
    }
}
  