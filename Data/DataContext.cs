public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options) { }

    public DbSet<OrgConfig> OrgConfigs => Set<OrgConfig>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");

        modelBuilder.Entity<OrgConfig>(entity =>
        {
            entity.ToTable("configs");

            entity.HasKey(e => e.ConfigId).HasName("configs_pkey");

            entity
                .HasIndex(
                    e => new
                    {
                        e.Org,
                        e.Brand,
                        e.Market,
                    },
                    "configs_org_brand_market_key"
                )
                .IsUnique();

            entity
                .Property(e => e.ConfigId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("config_id");

            entity.Property(e => e.Org).HasColumnName("org");
            entity.Property(e => e.Market).HasColumnName("market");
            entity.Property(e => e.Brand).HasColumnName("brand");

            entity
                .Property(e => e.IsActive)
                .HasDefaultValue(false)
                .HasColumnName("is_active");

            entity
                .Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");

            entity
                .Property(e => e.CreatedBy)
                .HasDefaultValueSql("'iw_db_default_user@innowatts.com'::text")
                .HasColumnName("created_by");

            entity
                .Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");

            entity
                .Property(e => e.UpdatedBy)
                .HasDefaultValueSql("'iw_db_default_user@innowatts.com'::text")
                .HasColumnName("updated_by");
        });
    }
}
