
namespace Silo.API.Presistance.Contexts.General;

public class GeneralDbContext(DbContextOptions<GeneralDbContext> options)
    : IdentityDbContext<User, Role, int>(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(GeneralDbContext).Assembly);

        base.OnModelCreating(builder);
    }
}