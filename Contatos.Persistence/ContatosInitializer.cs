namespace Contatos.Persistence
{
    public class ContatosInitializer
    {
        public static void Initialize(ContatosDbContext context)
        {
            var initializer = new ContatosInitializer();
            initializer.SeedEverything(context);
        }

        public void SeedEverything(ContatosDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
