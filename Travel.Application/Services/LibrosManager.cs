using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Application.Dtos.Autores;
using Travel.Application.Dtos.Editoriales;
using Travel.Application.Dtos.Libros;
using Travel.Application.Infra_Contracts;
using Travel.Application.Services.Contracts;
using Travel.Domain.Models;

namespace Travel.Application.Services
{
    public class LibrosManager : ILibrosManager
    {
        private readonly ILibroRepository _libroRepository;
        private readonly IAutorManager _autorManager;

        public LibrosManager(ILibroRepository libroRepository, IAutorManager autorManager)
        {
            _libroRepository = libroRepository;
            _autorManager = autorManager;
        }

        public async Task<bool> AddLibro(AddLibroDto libroDto, CancellationToken cancellationToken)
        {
            if (libroDto == null) throw new ArgumentNullException();

            if(! await addingValidations(libroDto,cancellationToken)) { return false; }


            Libro libro = new Libro()
            {
                EditorialId = libroDto.EditorialId,
                Isbn = libroDto.Isbn,
                NPaginas = libroDto.NPaginas,
                Sinopsis = libroDto.Sinopsis,
                Titulo = libroDto.Titulo,
            };

             return await _libroRepository.AddAsync(libro, libroDto.AutorId, cancellationToken);

            
        }

        private async Task<bool> addingValidations(AddLibroDto libroDto, CancellationToken cancellationToken)
        {

            if(await _libroRepository.ExistsAsync(libroDto.Isbn, cancellationToken)) return false;

            if(libroDto.AutorId <= 0) return false;

            if (libroDto.EditorialId <= 0) return false;

            if(String.IsNullOrEmpty(libroDto.Titulo)) return false;

            if(String.IsNullOrEmpty(libroDto.NPaginas)) return false;

            return true;
        }

        public async Task<List<LibroDto>> GetAllBooks(CancellationToken cancellationToken)
        {
            var books = await _libroRepository.GetAllAsync(cancellationToken);

            if (books != null)
            {
                var booksOut = books.Select(b => new LibroDto
                {
                    Isbn = b.Isbn,
                    NPaginas = b.NPaginas,
                    Sinopsis = b.Sinopsis,
                    Titulo = b.Titulo,
                }).ToList();

                return booksOut;
            }

            return new List<LibroDto>();
        }

        public async Task<AutorConLibrosDto> GetAllBooksByAuthorId(int AutorId, CancellationToken cancellationToken)
        {
            var author = await _autorManager.GetAuthorInfoById(AutorId, cancellationToken);

            if(author is null) return null!;
           

            var books = await _libroRepository.GetAllByAutorId(AutorId, cancellationToken);

            if (books != null)
            {
                var libros = books.Select(b=> new LibroEditorial
                    {
                      Editorial = new EditorialBasicDto
                      {
                          Sede = b.Libro.Editoriales.Sede,
                          Nombre = b.Libro.Editoriales.Nombre
                      },
                    NPaginas = b.Libro.NPaginas,
                    Sinopsis = b.Libro.Sinopsis,
                    Titulo = b.Libro.Titulo,
                    Isbn = b.Libro.Isbn                   

                }).ToList();

                var booksOut = new AutorConLibrosDto
                {
                    Libros = libros,
                    Apellido = author.Apellido,
                    Nombre = author.Nombre,
                }; 

                return booksOut;
            }

             return null!;
        }
        public async Task<LibroDto> GetLibroById(string LibroId, CancellationToken cancellationToken)
        {

            var libro = await _libroRepository.GetbyIdAsync(0, cancellationToken, LibroId);

            if (libro != null)
            {
                return new LibroDto
                {
                   Isbn = libro.Isbn,
                   NPaginas = libro.NPaginas,
                   Sinopsis= libro.Sinopsis,
                   Titulo = libro.Titulo
                };
            }

            return null!;
        }
    }
}