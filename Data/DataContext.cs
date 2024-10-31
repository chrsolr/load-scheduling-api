using Microsoft.EntityFrameworkCore;

public class Test
{
    public int Id { get; set; }
    public string Name { get; set; } = "Test";
}

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options) { }

    public DbSet<Test> Tests => Set<Test>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Test>()
            .HasData(
                new Test { Id = 1, Name = "Test 1" },
                new Test { Id = 2, Name = "Test 2" },
                new Test { Id = 3, Name = "Test 3" }
            );
    }
}
