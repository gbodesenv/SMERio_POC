using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rio.SME.Repositories.Mappings
{
    using Domain.Entities;

    public class UsuarioMap : EntityTypeConfiguration<Usuario>, IMapping
    {
        public UsuarioMap()
        {
            //primary key
            HasKey(u => u.Id);
            //auto incremento 
            Property(u => u.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //configuração das propriedades
            Property(u => u.Nome).IsRequired().HasMaxLength(100);
            Property(u => u.Senha).IsRequired().HasMaxLength(60);
            Property(u => u.Telefone).IsRequired().HasMaxLength(11);
            Property(u => u.Email).IsRequired().HasMaxLength(100);
        }
    }
}
