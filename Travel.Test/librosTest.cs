using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Travel.Application.Infra_Contracts;
using Travel.Domain.Models;
using Travel.Infrastructure.Persistence;
using Travel.Infrastructure.Persistence.Repositories;

namespace Travel.Test
{
    [TestFixture]
    public class librosTest
    {
        private ILibroRepository _libroRepository;




        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddScoped<ILibroRepository, LibroRepository>();
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer("Server = CMARGOK\\SQLEXPRESS; Database=TravelBookBb; PersistSecurityInfo=True; Trusted_Connection=True; MultipleActiveResultSets=True; TrustServerCertificate=true;"));

            var serviceProvider = services.BuildServiceProvider();
            _libroRepository = serviceProvider.GetService<ILibroRepository>()!;
        }

        [Test]
        public async Task ExistsAsync_ReturnsTrue_WhenIsbnExists()
        {
            var isbn = "9780330255592";

            var result = await _libroRepository.ExistsAsync(isbn, new CancellationToken());

            Assert.IsTrue(result);
        }

        [Test]
        public async Task ExistsAsync_ReturnsFalse_WhenIsbnNotExists()
        {
            var isbn = "9780330255512";

            var result = await _libroRepository.ExistsAsync(isbn, new CancellationToken());

            Assert.IsFalse(result);
        }

        [Test]
        public async Task GetbyIdAsync_ReturnsCorrectObject()
        {
            var isbn = "9780330255592";
           
            var result = await _libroRepository.GetbyIdAsync(0, new CancellationToken(), isbn);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<Libro>());
            // Verifica que los valores de los objetos devueltos sean los esperados
            Assert.That(result.Isbn, Is.EqualTo("9780330255592"));
            Assert.That(result.Titulo, Is.EqualTo("Cien años de soledad"));
            Assert.That(result.Sinopsis, Is.EqualTo("Entre la boda de José Arcadio Buendía con Amelia Iguarán hasta la maldición de Aureliano Babilonia transcurre todo un siglo. Cien años de soledad para una estirpe única, fantástica, capaz de fundar una ciudad tan especial como Macondo y de engendrar niños con cola de cerdo. En medio, una larga docena de personajes dejarán su impronta a las generaciones venideras, que tendrán que lidiar con un mundo tan complejo como sencillo."));
            Assert.That(result.NPaginas, Is.EqualTo("496"));
            Assert.That(result.EditorialId, Is.EqualTo(2));
        }

        [Test]
        public async Task GetbyIdAsync_ReturnsNull()
        {
            var isbn = "9780330255500";

            var result = await _libroRepository.GetbyIdAsync(0, new CancellationToken(), isbn);

            Assert.IsNull(result);
        }


        [Test]
        public async Task GetAllByAutorId_ReturnsCorrectObject()
        {
            var autorId = 3;

            var result = await _libroRepository.GetAllByAutorId(autorId, new CancellationToken());

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<List<AutoresHasLibro>>());
            // Verifica que los valores de los objetos devueltos sean los esperados
            Assert.That(result[0].Libro.Isbn, Is.EqualTo("9780330255592"));
            Assert.That(result[0].Libro.Titulo, Is.EqualTo("Cien años de soledad"));
            Assert.That(result[0].Libro.Sinopsis, Is.EqualTo("Entre la boda de José Arcadio Buendía con Amelia Iguarán hasta la maldición de Aureliano Babilonia transcurre todo un siglo. Cien años de soledad para una estirpe única, fantástica, capaz de fundar una ciudad tan especial como Macondo y de engendrar niños con cola de cerdo. En medio, una larga docena de personajes dejarán su impronta a las generaciones venideras, que tendrán que lidiar con un mundo tan complejo como sencillo."));
            Assert.That(result[0].Libro.NPaginas, Is.EqualTo("496"));
            Assert.That(result[0].Libro.EditorialId, Is.EqualTo(2));
        }



        [Test]
        public async Task AddAsync_ReturnsTrue()
        {
            var autorId = 1;
            var libro  = new Libro
            {
                EditorialId = 2,
                Isbn = "5544887765412",
                NPaginas = "258",
                Sinopsis = "Es mi libro de prueba",
                Titulo = "La prueba es el exito"
            };

            var result = await _libroRepository.AddAsync(libro,autorId, new CancellationToken());

            Assert.That(result, Is.True);
            _libroRepository.DeleteForTesting(libro.Isbn);
           
        }

        [Test]
        public async Task AddAsync_ReturnsFalse()
        {
            var autorId = 0;
            var libro = new Libro
            {
                EditorialId = 2,
                Isbn = "5544887765412",
                NPaginas = "258",
                Sinopsis = "Es mi libro de prueba",
                Titulo = "La prueba es el exito"
            };

            var result = await _libroRepository.AddAsync(libro, autorId, new CancellationToken());

            Assert.That(result, Is.False);

        }
    }
}