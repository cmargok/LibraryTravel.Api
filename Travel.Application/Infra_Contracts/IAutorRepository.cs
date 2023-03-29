﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Models;

namespace Travel.Application.Infra_Contracts
{
    public interface IAutorRepository : IGeneralRepository<Autore>
    {
        public Task<List<Autore>> GetAll(CancellationToken cancellationToken);
    }

}
