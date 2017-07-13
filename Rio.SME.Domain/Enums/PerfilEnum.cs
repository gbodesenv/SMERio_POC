using System.ComponentModel;

namespace Rio.SME.Domain.Enums
{
    public enum PerfilEnum
    {
        [Description("Administrador")]
        Administrador = 1,
        [Description("Diretor")]
        Diretor = 2,
        [Description("Professor de Creche")]
        ProfessorCreche = 3,
        [Description("Professor de Pré-Escola")]
        ProfessorPreEscola = 4,
        [Description("Técnico da Gerência de Educação Infantíl")]
        TecnicoGFI = 5,
    }
}
