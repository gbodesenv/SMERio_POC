using System.Collections.Generic;
namespace Rio.SME.Domain.Contracts.Services
{
    using Filters;
    using Rio.SME.Domain.Entities;

    public interface IAgrupadorService
    {
        Agrupador Buscar(int id);
        IEnumerable<Agrupador> Listar(AgrupadorFilter filtro);
        IEnumerable<Agrupador> ListarGrid(AgrupadorFilter filtro);
        
        void Salvar(Agrupador entidade);
        void Excluir(int id);
    }
}