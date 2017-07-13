namespace Rio.SME.Domain.Contracts.Data.Global
{
    using System.Data.Entity;

    public interface IUnitOfWork
    {
        DbContextTransaction BeginTransaction();
        void RollbackTransaction();
        void CommitTransaction();
        void Commit();
        DbContextTransaction GetTransactionAtiva();
    }
}
