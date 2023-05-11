using Contatos.Domain.Interfaces.Repositories;
using Contatos.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Contatos.Persistence
{
    public class DependencyInjectionConfig
    {
        public static void Inject(IServiceCollection services)
        {
            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
        }
    }
}
