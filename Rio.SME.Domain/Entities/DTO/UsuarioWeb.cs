using MSTech.CoreSSO.Entities;
using System;
using System.Collections.Generic;

namespace Rio.SME.Domain.Entities.DTO
{
    [Serializable]
    public class UsuarioWeb
    {
        public bool Autenticado { get; set; }
        public String MsgRetorno { get; set; }
        public string Matricula { get; set; }
        public String Nome { get; set; }
        public String Senha { get; set; }
        public string CodigoUnidadeGestora { get; set; }
        public string NomeUnidadeGestora { get; set; }
        public int CodigoPessoa { get; set; }
        public string NomeCargoPrincipal { get; set; }
        public SistemaWebCoreSSO SistemaWebCoreSSO { get; set; }

        public SYS_Usuario Usuario { get; set; }

        /// <summary>
        /// Retorna o grupo na qual o usuário pertense
        /// naquele sistema.
        /// </summary>
        public SYS_Grupo Grupo { get; set; }

        /// <summary>
        /// Recebe a permissão do módulo do usuário logado.
        /// </summary>
        public SYS_GrupoPermissao grupoPermissao;

        /// <summary>
        /// Retorna as unidades administrativa e entidades de um grupo do usuário.
        /// </summary>
        public IList<SYS_UsuarioGrupoUA> GrupoUA { get; set; }
    }
}