using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Models;

namespace Travel.Application.Infra_Contracts
{
    public interface IAutorRepository : IGeneralRepository<Autor>
    {
        public Task<bool> AddAsync(Autor entity, CancellationToken cancellationToken);
        //por hacer
        /*
        
        -implementar el middleware de erroes
        -implementar el log
        -implementar documentacion en los metodos y servicios principales
        -implementar JWT por medio de la api de seguridad, osea implementar el endpoint par agenerar el token y conectarme a la api de seguridad...


        -implementar los unit test
         */
    }

}
