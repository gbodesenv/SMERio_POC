
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Rio.SME.Repositories.Global
{    
    
    using Context;
    using Domain.Filters;
    using Domain.Contracts.Entities;
    using Domain.Contracts.Data.Global;    

    public class Repository<T, F> : IRepository<T, F>
        where T : class, IEntity
        where F : Filter, new()
    {
        internal SMEContext _db;
        internal DbSet<T> _dbSet;

        public Repository(IContextFactory context)
        {
            _db = context.GetDbContext();
            _dbSet = _db.Set<T>();
        }
               
        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            var old = _dbSet.Where(x => x.Id == entity.Id).SingleOrDefault();
            _db.Entry(old).CurrentValues.SetValues(entity);
            _db.ChangeObjectState(old, EntityState.Modified);
        }

        public virtual void Delete(int id)
        {
            // Exclusão lógica
            var entity = _dbSet.Find(id);
            entity.Excluido = true;
            _db.Entry(entity).CurrentValues.SetValues(entity);
            _db.ChangeObjectState(entity, EntityState.Modified);
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<T> All(int skip = 0, int take = 25)
        {
            return _dbSet.Where(x => !x.Excluido).OrderBy(x => x.Id).Skip(skip).Take(take).AsNoTracking();
        }

        public IEnumerable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(x => !x.Excluido).Where(predicate).AsNoTracking();
        }
        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(x => !x.Excluido).AsNoTracking().FirstOrDefault(predicate);
        }

        /// <summary>
        /// Disponibiliza acesso direto ao Where do DbSet, sem checar se a entidade está excluida 
        /// ou qualquer outra condição
        /// </summary>
        /// <param name="predicate">Expressão para o Linq</param>
        /// <returns></returns>
        public IEnumerable<T> WhereRaw(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).AsNoTracking();
        }

        public int WhereCount(F filter)
        {
            return this.Where(filter).Count();
        }

        public virtual IEnumerable<T> Where(F filter)
        {
            throw new NotImplementedException("MensagensErro.ErroRepositorioBaseNaoImplementado");
        }

        /// <summary>
        /// Filtro básico que incluir ordem, numero de registros e situação diferente de Excluido para retorno e posição
        /// </summary>
        /// <param name = "query" > Query de consulta</param>
        /// <param name = "filter" > Filtro de consulta</param>
        protected virtual void ApplyBasicFilter(ref IQueryable<T> query, ref F filter)
        {
            filter = filter ?? new F();
            query = query.Where(x => !x.Excluido);

            filter.TotalRecords = query.Count();
            if (filter.EnablePaging)
                query = query.Skip(filter.Skip).Take(filter.Take);
        }

        private bool disposed = false;

        protected void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
