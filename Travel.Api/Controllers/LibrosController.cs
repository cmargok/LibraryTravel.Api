using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
