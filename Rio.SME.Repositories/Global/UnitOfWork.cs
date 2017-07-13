using System;
using System.Data;
using System.Data.Entity;

namespace Rio.SME.Repositories.Global
{
    using Domain.Contracts.Data.Global;
    using Context;

    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private DbContextTransaction _transaction;
        private static SMEContext _db;

        public UnitOfWork(IContextFactory dbContextFactory)
        {
            _db = dbContextFactory.GetDbContext();
        }

        public DbContextTransaction BeginTransaction()
        {
            //Entity não suporta transactions paralelas
            if (_transaction != null || _db.Database.CurrentTransaction != null)
            {
#if AmbienteNT
                throw new NotSupportedException("Duas transactions foram abertas no Entity Framework ao mesmo tempo, cheque o uso do argumento [CommitedTransaction] ou se algo está abrindo uma transaction no contexto atual (Request atual) sem usar o UnitOfWork. Isso deve ser corrigido. Dica: Se um metodo da controller chama outro metodo de alguma controller ou da mesma, e os dois estão marcados com o atributo, haverá duas tentivas de abrir uma transaction.");
#else
                return null;
#endif
            }
            _transaction = _db.Database.BeginTransaction(IsolationLevel.ReadCommitted);
            return _transaction;
        }


        public void RollbackTransaction()
        {
            if (_transaction != null)
                _transaction.Rollback();
        }

        public void CommitTransaction()
        {
            if (_transaction != null)
                _transaction.Commit();
        }

        public void Commit()
        {
            _db.SaveChanges();
        }

        public DbContextTransaction GetTransactionAtiva()
        {
            return _transaction;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    // _transaction = null;
                    //_db.Dispose();
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
