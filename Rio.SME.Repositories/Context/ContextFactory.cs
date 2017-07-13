namespace Rio.SME.Repositories.Context
{
    using System;
    using System.Data.Common;
    
    public sealed class ContextFactory : IContextFactory
    {
        private SMEContext _context;

        public ContextFactory()
        {
            _context = new SMEContext();
        }

        public SMEContext GetDbContext()
        {
            return _context;
        }

        public void SetInMemoryDbContext(DbConnection connection)
        {
            _context = new SMEContext(connection);
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
                GC.SuppressFinalize(this);
            }
        }
    }
}
