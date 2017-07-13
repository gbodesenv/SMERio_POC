using System.Collections.Generic;
namespace Rio.SME.Domain.Contracts.Services
{    
    using Filters;
    using Rio.SME.Domain.Entities;

    public interface IUsuarioService
    {        
        Usuario Buscar(int id);
        IEnumerable<Usuario> Listar(UsuarioFilter filtro);
        IEnumerable<Usuario> ListarGrid(UsuarioFilter filtro);
        bool ValidarUsuario(string email, string senha);

        void Salvar(Usuario entidade);
        void Excluir(int id);
    }
}
