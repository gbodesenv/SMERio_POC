using System;
using System.Data.Common;

namespace Rio.SME.Repositories.Context
{
    public interface IContextFactory : IDisposable
    {
        SMEContext GetDbContext();
        void SetInMemoryDbContext(DbConnection connection);
    }
}