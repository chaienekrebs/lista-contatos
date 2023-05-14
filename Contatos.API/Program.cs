using Microsoft.AspNetCore;
using Contatos.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Contatos.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ContatosDbContext>();
                context.Database.Migrate();

                ContatosInitializer.Initialize(context);
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
