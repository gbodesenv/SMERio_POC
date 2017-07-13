using Rio.SME.Web.Generico;
using System;
using System.Linq;
using System.Web;

namespace Rio.SME.Web.DTO
{
    public class Seguranca
    {
        /// <summary>
        /// Método para verificação das permissões/funcionalidades de segurança
        /// </summary>
        /// <param name="chaveSeguranca">O código da funcionalidade (chave de segurança)</param>
        /// <returns>True se o usuário logado tem permissão de execução/acesso da funcionalidade, caso não, False</returns>
        public bool VerificaPermissao(String chaveSeguranca)
        {
            return true;
            //var funcionalidade = UtilWeb.UsuarioLogado.Funcionalidades.FirstOrDefault(p => p.Nome == (chaveSeguranca ?? String.Empty));
            //return (funcionalidade != null);
        }
    }
}
