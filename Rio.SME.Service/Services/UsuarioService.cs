namespace Rio.SME.Service.Services
{
    using Rio.SME.Domain.Contracts.Services;
    using Rio.SME.Domain.Contracts.Data.Global;
    using Rio.SME.Domain.Contracts.Data.Repositories;
    using Rio.SME.Domain.Entities;
    using Rio.SME.Domain.Filters;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class UsuarioService : Service, IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepositorio;

        public UsuarioService(IUnitOfWork uow,
            IUsuarioRepository usuarioRepository)
            : base(uow)
        {
            _usuarioRepositorio = usuarioRepository;
        }

        public Usuario Buscar(int id)
        {
            return TryCatch(() =>
            {
                return _usuarioRepositorio.GetById(id);
            });
        }

        public void Excluir(int id)
        {
            TryCatch(() =>
           {
               _usuarioRepositorio.Delete(id);
           });
        }

        public Usuario GetUsuarioFromEmail(string email)
        {
            return TryCatch(() =>
            {
                return _usuarioRepositorio.WhereRaw((u) => u.Email.Contains(email)).FirstOrDefault();
            });
        }

        public IEnumerable<Usuario> Listar(UsuarioFilter filtro)
        {
            return TryCatch(() =>
            {
                return _usuarioRepositorio.Where(filtro);
            });
        }

        public IEnumerable<Usuario> ListarGrid(UsuarioFilter filtro)
        {
            return TryCatch(() =>
            {
                return _usuarioRepositorio.All();
            });
        }

        public void Salvar(Usuario entidade)
        {
            TryCatch(() =>
           {
               _usuarioRepositorio.Add(entidade);
           });
        }

        public bool ValidarUsuario(string email, string senha)
        {
            return TryCatch(() =>
            {
                return _usuarioRepositorio.ValidarUsuario(email, senha);
            });
        }
    }
}
