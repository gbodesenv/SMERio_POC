using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rio.SME.Repositories.Mappings
{
    using Domain.Entities;

    public class AgrupadorMap : EntityTypeConfiguration<Agrupador>, IMapping
    {
        public AgrupadorMap()
        {
            //primary key
            HasKey(u => u.Id);
            //auto incremento 
            Property(u => u.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //configuração das propriedades
            Property(u => u.Nome).IsRequired().HasMaxLength(500);
            Property(u => u.IndicadorAtivo).IsRequired();            
        }
    }
}
