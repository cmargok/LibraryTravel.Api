using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Travel.Application.Dtos.Autores;
using Travel.Application.Dtos.Editoriales;
using Travel.Application.Dtos.Libros;
using Travel.Application.Services;
using Travel.Application.Services.Contracts;
using Travel.Domain.Enums;
using Travel.Domain.Tools;

namespace Travel.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly ILibrosManager _librosManager;
        public LibrosController(ILibrosManager librosManager)
        {
            _librosManager = librosManager;
        }


        /// <summary>
        /// Metodo para obtener todos los libros en la libreria
        /// </summary>
        /// <returns>Lista con los libros</returns>
        [ProducesResponseType(typeof(ApiResponse<List<LibroDto>>),200)]
        [ProducesResponseType(typeof(ApiResponse<List<LibroDto>>), 404)]
        [ProducesResponseType(typeof(Problem),500)]
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var result = await _librosManager.GetAllBooks(cancellationToken);

            if(result.Count > 0)
            {
                return Ok(Tools.CreateResponse(result, Result.Success, result.Count));
            }

            return NotFound(Tools.CreateResponse(result, Result.NotFound, result.Count));

        }


        /// <summary>
        /// Metodo para obtener un libro en especifico por medio dele ISBN
        /// </summary>
        /// /// <param name="isbn"></param>
        /// <returns>Lista con los libros</returns>
        [ProducesResponseType(typeof(ApiResponse<LibroDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse<LibroDto>), 404)]
        [ProducesResponseType(typeof(Problem), 500)]
        [Authorize]
        [HttpGet("ISBN")]
        public async Task<IActionResult> GetById(string isbn,CancellationToken cancellationToken)
        {
            var result = await _librosManager.GetLibroById(isbn, cancellationToken);

            if (result != null)
            {
                return Ok(Tools.CreateResponse(result, Result.Success, 1));
            }

            return NotFound(Tools.CreateResponse(result, Result.NotFound, 0));

        }

        /// <summary>
        /// Metodo para obtener todos los libros que tiene un autor en especifico por medio del AutorId
        /// </summary>
        /// /// <param name="AutorId"></param>
        /// <returns>Lista con los libros</returns>
        [ProducesResponseType(typeof(ApiResponse<AutorConLibrosDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse<AutorConLibrosDto>), 404)]
        [ProducesResponseType(typeof(Problem), 500)]
      //  [Authorize]
        [HttpGet("/api/v1/Autor/{AutorId}/libros")]
        public async Task<IActionResult> Get(int AutorId, CancellationToken cancellationToken)
        {
            var result = await _librosManager.GetAllBooksByAuthorId(AutorId, cancellationToken);

            if (result != null)
            {
                return Ok(Tools.CreateResponse(result, Result.Success, 1));
            }

            return NotFound(Tools.CreateResponse(result, Result.NotFound, 0));

        }


        /// <summary>
        /// Metodo para añadir un libro a la libreria
        /// </summary>
        /// <param name="addLibro"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ApiResponse<bool>), 200)]
        [ProducesResponseType(typeof(ApiResponse<bool>), 404)]
        [ProducesResponseType(typeof(Problem), 500)]
        [Authorize]
        [HttpPost("Add")]
        public async Task<IActionResult> Add(AddLibroDto addLibro, CancellationToken cancellationToken)
        {
            var result = await _librosManager.AddLibro(addLibro, cancellationToken);

            if (result)
            {
                return Ok(Tools.CreateResponse(result, Result.Success, 1));
            }

            return NotFound(Tools.CreateResponse(result, Result.NotFound, 0));

        }
    }
}
