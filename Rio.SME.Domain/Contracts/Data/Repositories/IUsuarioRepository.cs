namespace Rio.SME.Domain.Contracts.Data.Repositories
{
    using Filters;
    using Global;
    using Rio.SME.Domain.Entities;

    public interface IUsuarioRepository : IRepository<Usuario, UsuarioFilter>
    {
        bool ValidarUsuario(string email, string senha);
    }
}
