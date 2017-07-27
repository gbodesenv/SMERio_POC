using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Rio.SME.Domain.Contracts.Data.Global;
using Rio.SME.Domain.Contracts.Data.Repositories;
using Rio.SME.Domain.Entities;
using Rio.SME.Domain.Filters;
using Rio.SME.Domain.Contracts.Services;

namespace Rio.SME.Service.Services
{
    public class AgrupadorService : Service, IAgrupadorService
    {
        private readonly IAgrupadorRepository _agrupadorRepository;
        public AgrupadorService(
            IUnitOfWork uow,
            IAgrupadorRepository agrupadorRepository)
            : base(uow)
        {
            _agrupadorRepository = agrupadorRepository;
        }

        public Agrupador Buscar(int id)
        {
            return TryCatch(() =>
            {
                return _agrupadorRepository.GetById(id);
            });
        }

        public void Excluir(int id)
        {
            TryCatch(() =>
           {
               throw new NotImplementedException();
           });
        }

        public IEnumerable<Agrupador> Listar(AgrupadorFilter filtro)
        {
            return TryCatch(() =>
            {
                return _agrupadorRepository.Where(filtro);
            });
        }

        public IEnumerable<Agrupador> ListarGrid(AgrupadorFilter filtro)
        {
            return TryCatch(() =>
            {
                return _agrupadorRepository.Where(filtro);
            });
        }

        public void Salvar(Agrupador entidade)
        {
            TryCatch(() =>
           {
               entidade.Validate();

               if (entidade.Id == 0)
                   _agrupadorRepository.Add(entidade);
               else
                   _agrupadorRepository.Update(entidade);
           });
        }

    }
}
