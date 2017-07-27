namespace Rio.SME.Repositories.Repositories
{
    using Domain.Filters;
    using Domain.Entities;
    using Domain.Contracts.Data.Repositories;
    using Context;
    using Global;
    using Domain.Util.ExtensionMethods;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Dynamic;

    public class AgrupadorRepository : Repository<Agrupador, AgrupadorFilter>, IAgrupadorRepository
    {
        public AgrupadorRepository(IContextFactory dbContextFactory)
            : base(dbContextFactory)
        {

        }
        
        public override IEnumerable<Agrupador> Where(AgrupadorFilter filter)
        {
            var query = from agrupador in _db.Agrupador
                        select agrupador;

            this.AplicarFiltro(ref query, filter);

            return query.ToList();
        }

        private void AplicarFiltro(ref IQueryable<Agrupador> query, AgrupadorFilter filter)
        {
            // Ordenação
            string order = string.Format("{0} {1}", filter.CampoOrdenacao.ToString(), filter.Order.ToString());
            query = query.OrderBy(order);

            if (filter.Id > 0)
                query = query.Where(x => filter.Id == x.Id);

            if (!filter.Nome.IsNullOrEmpty())
                query = query.Where(x => filter.Nome == x.Nome);

            // Filtro
            base.ApplyBasicFilter(ref query, ref filter);
        }

    }
}