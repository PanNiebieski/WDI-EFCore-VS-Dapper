using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WDIPaladins.Domain;
using Microsoft.Extensions.Logging;

namespace WDIPaladins.Infrastructure.EFCore
{
    public class PaladinsContext : DbContext
    {
        public PaladinsContext(DbContextOptions<PaladinsContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();

        }

        public DbSet<Paladin> Paladins { get; set; }

        public DbSet<Monastery> Monasteries { get; set; }

        public DbSet<Skill> Skills { get; set; }

        public DbSet<Item> Items { get; set; }



        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        //entry.Entity.CreatedAt = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        //entry.Entity.LastModifiedDate = DateTime.Now;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
         

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLoggerFactory(ConsoleLoggerFactory);
            }

            base.OnConfiguring(optionsBuilder);
        }

        public static readonly ILoggerFactory ConsoleLoggerFactory = LoggerFactory.Create(builder =>

        {
            builder.AddFilter((category, level) =>
            category == DbLoggerCategory.Database.Command.Name
            && level == LogLevel.Information)
            .AddConsole();

        });


    }
}