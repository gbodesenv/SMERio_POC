using System;


namespace Rio.SME.Web.DTO
{
    [Serializable]
    public class Usuario
    {
        public bool Autenticado { get; set; }
        public String MsgRetorno { get; set; }
        public string Matricula { get; set; }
        public String Nome { get; set; }
        public String Senha { get; set; }
        public string CodigoUnidadeGestora { get; set; }
        public string NomeUnidadeGestora { get; set; }
        public int CodigoPessoa { get; set; }

        public int CodigoCargoPrincipal { get; set; }
        public string NomeCargoPrincipal { get; set; }


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