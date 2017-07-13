using System;
using System.Linq;
using System.Data.Common;
using System.Data.Entity;
using System.Reflection;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Rio.SME.Repositories.Context
{
    using Domain.Entities;
    using Mappings;

    public class SMEContext : DbContext
    {
        public SMEContext()
       : base("name=SMEConnection")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public SMEContext(DbConnection connection)
        : base(connection, true)
        {
            Database.Initialize(false);
        }
        
        public DbSet<Usuario> Usuario { get; set; }
   
            
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Conventions
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //Fazendo o mapeamento com o banco de dados
            //Pega todas as classes que estão implementando a interface IMapping
            //Assim o Entity Framework é capaz de carregar os mapeamentos
            var typesToMapping = (from x in Assembly.GetExecutingAssembly().GetTypes()
                                  where x.IsClass && typeof(IMapping).IsAssignableFrom(x)
                                  select x).ToList();

            // Varrendo todos os tipos que são mapeamento 
            // Com ajuda do Reflection criamos as instancias 
            // e adicionamos no Entity Framework
            foreach (var mapping in typesToMapping)
            {
                dynamic mappingClass = Activator.CreateInstance(mapping);
                modelBuilder.Configurations.Add(mappingClass);
            }
        }

        public virtual void ChangeObjectState(object model, EntityState state)
        {
            //Aqui trocamos o estado do objeto, 
            //facilita quando temos alterações e exclusões
            ((IObjectContextAdapter)this)
                          .ObjectContext
                          .ObjectStateManager
                          .ChangeObjectState(model, state);
        }
    }
}
