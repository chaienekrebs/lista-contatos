using Contatos.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Contatos.Persistence
{
    public class ContatosDbContext : DbContext
    {
        public ContatosDbContext(DbContextOptions<ContatosDbContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //#if !DEBUG
        //            optionsBuilder.UseNpgsql(@"User ID=svc_pg_portalservidor;Password=bzigrrqOIU8upw2fgrB4;Host=192.168.152.25;Port=5432;Database=portalservidor_db;");
        //#else

        //            optionsBuilder.UseNpgsql(@"User ID=postgres;Password=tDdBw5pM45AnCKte;Host=192.168.152.25;Port=5432;Database=demoportalservidor_db;");
        //#endif

        //        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContatosDbContext).Assembly);
        }
        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<TipoContato> TipoContato { get; set; }
        public DbSet<Contato> Contato { get; set; }
    }
}