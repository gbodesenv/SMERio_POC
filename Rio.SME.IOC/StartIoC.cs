using SimpleInjector;
using SimpleInjector.Extensions.LifetimeScoping;
using SimpleInjector.Integration.Web;

namespace Rio.SME.Infra.IOC
{
    using Domain.Contracts.Data.Repositories;
    using Domain.Contracts.Data.Global;
    using Domain.Contracts.Services;
    using Repositories.Context;
    using Repositories.Global;
    using Repositories.Repositories;
    using Rio.SME.Service.Services;

    public class StartIOC
    {
        public Container Container { get; private set; }

        public T GetInstance<T>()
        {
            return (T)this.Container.GetInstance(typeof(T));
        }

        public StartIOC(bool unitTest = false)
        {
            Container = new Container();

            if (!unitTest)
                Container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            else
                Container.Options.DefaultScopedLifestyle = new LifetimeScopeLifestyle(true);

            //cerne da aplicação
            Container.Register<IUnitOfWork, UnitOfWork>(Lifestyle.Scoped);
            Container.Register<IContextFactory, ContextFactory>(Lifestyle.Scoped);

            // Repositories
            Container.Register<IUsuarioRepository, UsuarioRepository>(Lifestyle.Scoped);




            // Services
            Container.Register<IUsuarioService, UsuarioService>(Lifestyle.Transient);



            
            Container.Verify();
        }
    }
}
