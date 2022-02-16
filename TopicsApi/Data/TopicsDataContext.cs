
namespace TopicsApi.Data;

public class TopicsDataContext : DbContext
{
    public TopicsDataContext(DbContextOptions<TopicsDataContext> options): base(options)
    {

    }
    public DbSet<Topic>? Topics { get; set; }

    public DbSet<Resource>? Resources { get; set; } // you only have to add things here if you need to access them directly from the context.


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Resource>()
            .Property(x => x.Description)
            .HasMaxLength(300);
    }
}
