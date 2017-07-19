using MSTech.CoreSSO.BLL;
using MSTech.CoreSSO.Entities;
using System;
using System.Collections.Generic;

namespace Rio.SME.Web.DTO
{
    [Serializable]
    public class Usuario : SYS_Usuario
    {
        // UA ou entidades do usuário logado
        private IList<SYS_UsuarioGrupoUA> uasGrupo;

        public bool Autenticado { get; set; }
        public String MsgRetorno { get; set; }
        // Grupo logado
        public SYS_Grupo Grupo
        {
            get { return Grupo; }
            set { Grupo = value; uasGrupo = SYS_UsuarioBO.GetSelectByUsuarioGrupoUA(usu_id, Grupo.gru_id); }
        }
        public IList<SYS_UsuarioGrupoUA> GrupoUA { get { return uasGrupo; } }
        // Permissao do módulo do usuário
        private SYS_GrupoPermissao grupoPermissao;



        //public string Matricula { get; set; }
        //public String Nome { get; set; }
        //public String Senha { get; set; }
        //public string CodigoUnidadeGestora { get; set; }
        //public string NomeUnidadeGestora { get; set; }
        //public int CodigoPessoa { get; set; }

        //public int CodigoCargoPrincipal { get; set; }
        //public string NomeCargoPrincipal { get; set; }


        //public DateTime DtUltimoAcesso { get; set; }

        /// <summary>
        /// Checa se o usuário tem o cargo especificado como principal
        /// </summary>
        /// <param name="grupoCargo">Cargo a ser verificado</param>
        /// <returns>Retorna true se o usuário possui o cargo</returns>
        //public bool VerificarCargo(GrupoPermissaoEnum grupoCargo)
        //{
        //    return NomeCargoPrincipal == EnumHelper.GetDescriptionFromEnumValue(grupoCargo);
        //}
    }
}