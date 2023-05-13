using Contatos.Application.Services;
using Contatos.Domain.Interfaces.Application;
using Microsoft.Extensions.DependencyInjection;

namespace Contatos.Application
{
    public class DependencyInjectionConfig
    {
        public static void Inject(IServiceCollection services)
        {
            services.AddTransient<IPessoaService, PessoaService>();
            services.AddTransient<ITipoContatoService, TipoContatoService>();
        }
    }
}
