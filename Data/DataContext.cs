public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options) { }

    public DbSet<Config> Configs => Set<Config>();
    public DbSet<Credential> Credentials => Set<Credential>();
    public DbSet<LocationAttribute> LocationAttributes =>
        Set<LocationAttribute>();

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
                .Property(e => e.ConfigId)
                .HasColumnName("config_id")
                .IsRequired();

            entity
                .Property(e => e.CredentialId)
                .HasDefaultValueSql("gen_random_uuid()")
                .IsRequired();

            entity
                .Property(e => e.UserSubAccount)
                .HasDefaultValueSql("'DEFAULT'::text");

            entity
                .Property(e => e.IsActive)
                .HasDefaultValue(false)
                .HasColumnName("is_active");

            entity
                .Property(e => e.IsSuma)
                .HasDefaultValue(false)
                .HasColumnName("is_suma");

            entity
                .Property(e => e.UseCertificate)
                .HasDefaultValue(true)
                .HasColumnName("use_certificate");

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

        modelBuilder.Entity<LocationAttribute>(entity =>
        {
            entity.ToTable("location_attributes");

            entity
                .HasKey(e => e.LocationAttributeId)
                .HasName("location_attribute_id");

            entity
                .Property(e => e.ConfigId)
                .HasColumnName("config_id")
                .IsRequired();

            entity
                .Property(e => e.CredentialId)
                .HasColumnName("credential_id")
                .IsRequired();

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
                .WithMany(p => p.LocationAttributes)
                .HasForeignKey(d => d.ConfigId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("location_attributes_config_id_fkey");

            entity
                .HasOne(d => d.Credential)
                .WithMany(p => p.LocationAttributes)
                .HasForeignKey(d => d.CredentialId)
                .HasConstraintName("location_attributes_credential_id_fkey");
        });
    }
}
