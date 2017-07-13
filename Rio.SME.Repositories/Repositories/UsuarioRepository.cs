namespace Rio.SME.Repositories.Repositories
{
    using Domain.Filters;
    using Domain.Entities;
    using Domain.Contracts.Data.Repositories;
    using Context;
    using Global;
    using Domain.Util.ExtensionMethods;
    using System.Linq.Expressions;
    using System;

    public class UsuarioRepository : Repository<Usuario, UsuarioFilter>, IUsuarioRepository
    {
        public UsuarioRepository(IContextFactory dbContextFactory)
            : base(dbContextFactory)
        {
        }

        public bool ValidarUsuario(string email, string senha)
        {
            Expression<Func<Usuario, bool>> testeEmail = ps => ps.Email.Equals(email);
            Expression<Func<Usuario, bool>> testeSenha = ps => ps.Senha.Equals(senha);
           
            return WhereRaw(testeEmail.And(testeSenha)).Count() > 0;
        }
    }
}