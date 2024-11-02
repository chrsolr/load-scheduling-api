public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options) { }

    public DbSet<Config> Configs => Set<Config>();
    public DbSet<Credential> Credentials => Set<Credential>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");

        modelBuilder.Entity<Config>(entity =>
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
                .HasDefaultValueSql("gen_random_uuid()");

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

        modelBuilder.Entity<Credential>(entity =>
        {
            entity.ToTable("credentials");

            entity.HasKey(e => e.CredentialId).HasName("credentials_pkey");

            entity
                .Property(e => e.CredentialId)
                .HasDefaultValueSql("gen_random_uuid()");

            entity
                .Property(e => e.UserSubAccount)
                .HasDefaultValueSql("'DEFAULT'::text");

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

            entity
                .HasOne(d => d.Config)
                .WithMany(p => p.Credentials)
                .HasForeignKey(d => d.ConfigId)
                .HasConstraintName("credentials_config_id_fkey");
        });
    }
}
