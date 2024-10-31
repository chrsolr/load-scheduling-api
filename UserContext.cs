using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LoadSchedulingAPI;

public partial class UserContext : DbContext
{
    public UserContext()
    {
    }

    public UserContext(DbContextOptions<UserContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Config> Configs { get; set; }

    public virtual DbSet<Credential> Credentials { get; set; }

    public virtual DbSet<DefaultNop> DefaultNops { get; set; }

    public virtual DbSet<DefaultPriceInfo> DefaultPriceInfos { get; set; }

    public virtual DbSet<IbtContract> IbtContracts { get; set; }

    public virtual DbSet<LocationAttribute> LocationAttributes { get; set; }

    public virtual DbSet<MarketMetadatum> MarketMetadata { get; set; }

    public virtual DbSet<Migration> Migrations { get; set; }

    public virtual DbSet<PriceInfo> PriceInfos { get; set; }

    public virtual DbSet<RoundingCriterion> RoundingCriteria { get; set; }

    public virtual DbSet<Submission> Submissions { get; set; }

    public virtual DbSet<SubmissionStatusesArchived> SubmissionStatusesArchiveds { get; set; }

    public virtual DbSet<SubmissionsArchived> SubmissionsArchiveds { get; set; }

    public virtual DbSet<SubmissionsMetaArchived> SubmissionsMetaArchiveds { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=USER;Username=somepostgresuser;Password=somepostgrespassword;Port=5435");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");

        modelBuilder.Entity<Config>(entity =>
        {
            entity.HasKey(e => e.ConfigId).HasName("configs_pkey");

            entity.ToTable("configs");

            entity.HasIndex(e => new { e.Org, e.Brand, e.Market }, "configs_org_brand_market_key").IsUnique();

            entity.Property(e => e.ConfigId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("config_id");
            entity.Property(e => e.Brand).HasColumnName("brand");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasDefaultValueSql("'iw_db_default_user@innowatts.com'::text")
                .HasColumnName("created_by");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(false)
                .HasColumnName("is_active");
            entity.Property(e => e.Market).HasColumnName("market");
            entity.Property(e => e.Org).HasColumnName("org");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasDefaultValueSql("'iw_db_default_user@innowatts.com'::text")
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<Credential>(entity =>
        {
            entity.HasKey(e => e.CredentialId).HasName("credentials_pkey");

            entity.ToTable("credentials");

            entity.HasIndex(e => new { e.Org, e.Brand, e.Market, e.Zone, e.Location, e.UserSubAccount }, "credentials_org_brand_market_zone_location_user_sub_account_key").IsUnique();

            entity.Property(e => e.CredentialId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("credential_id");
            entity.Property(e => e.Brand).HasColumnName("brand");
            entity.Property(e => e.ConfigId).HasColumnName("config_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasDefaultValueSql("'iw_db_default_user@innowatts.com'::text")
                .HasColumnName("created_by");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(false)
                .HasColumnName("is_active");
            entity.Property(e => e.IsSuma)
                .HasDefaultValue(false)
                .HasColumnName("is_suma");
            entity.Property(e => e.Location).HasColumnName("location");
            entity.Property(e => e.LocationName).HasColumnName("location_name");
            entity.Property(e => e.Market).HasColumnName("market");
            entity.Property(e => e.Org).HasColumnName("org");
            entity.Property(e => e.Pfx).HasColumnName("pfx");
            entity.Property(e => e.PfxPassphrase).HasColumnName("pfx_passphrase");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasDefaultValueSql("'iw_db_default_user@innowatts.com'::text")
                .HasColumnName("updated_by");
            entity.Property(e => e.UseCertificate)
                .HasDefaultValue(true)
                .HasColumnName("use_certificate");
            entity.Property(e => e.UserAccount).HasColumnName("user_account");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.UserPassword).HasColumnName("user_password");
            entity.Property(e => e.UserSubAccount)
                .HasDefaultValueSql("'DEFAULT'::text")
                .HasColumnName("user_sub_account");
            entity.Property(e => e.Zone).HasColumnName("zone");

            entity.HasOne(d => d.Config).WithMany(p => p.Credentials)
                .HasForeignKey(d => d.ConfigId)
                .HasConstraintName("credentials_config_id_fkey");
        });

        modelBuilder.Entity<DefaultNop>(entity =>
        {
            entity.HasKey(e => e.NopId).HasName("default_nop_pkey");

            entity.ToTable("default_nop");

            entity.HasIndex(e => new { e.ConfigId, e.Org, e.Market, e.Brand, e.Location }, "default_nop_config_id_org_market_brand_location_key").IsUnique();

            entity.Property(e => e.NopId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("nop_id");
            entity.Property(e => e.Bid)
                .HasDefaultValue(0)
                .HasColumnName("bid");
            entity.Property(e => e.Brand)
                .HasMaxLength(255)
                .HasColumnName("brand");
            entity.Property(e => e.ConfigId).HasColumnName("config_id");
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .HasColumnName("location");
            entity.Property(e => e.Market)
                .HasMaxLength(255)
                .HasColumnName("market");
            entity.Property(e => e.Offer)
                .HasDefaultValue(0)
                .HasColumnName("offer");
            entity.Property(e => e.Org)
                .HasMaxLength(255)
                .HasColumnName("org");

            entity.HasOne(d => d.Config).WithMany(p => p.DefaultNops)
                .HasForeignKey(d => d.ConfigId)
                .HasConstraintName("default_nop_config_id_fkey");
        });

        modelBuilder.Entity<DefaultPriceInfo>(entity =>
        {
            entity.HasKey(e => e.PriceInfoId).HasName("default_price_info_pkey");

            entity.ToTable("default_price_info");

            entity.HasIndex(e => new { e.ConfigId, e.Org, e.Market }, "default_price_info_config_id_org_market_key").IsUnique();

            entity.Property(e => e.PriceInfoId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("price_info_id");
            entity.Property(e => e.BidPrice)
                .HasPrecision(10, 2)
                .HasColumnName("bid_price");
            entity.Property(e => e.ConfigId).HasColumnName("config_id");
            entity.Property(e => e.Market)
                .HasMaxLength(255)
                .HasColumnName("market");
            entity.Property(e => e.OfferPrice)
                .HasPrecision(10, 2)
                .HasColumnName("offer_price");
            entity.Property(e => e.Org)
                .HasMaxLength(255)
                .HasColumnName("org");

            entity.HasOne(d => d.Config).WithMany(p => p.DefaultPriceInfos)
                .HasForeignKey(d => d.ConfigId)
                .HasConstraintName("default_price_info_config_id_fkey");
        });

        modelBuilder.Entity<IbtContract>(entity =>
        {
            entity.HasKey(e => e.IbtContractId).HasName("ibt_contracts_pkey");

            entity.ToTable("ibt_contracts");

            entity.HasIndex(e => new { e.Org, e.Market, e.Brand, e.Zone, e.ContractName }, "ibt_contracts_org_market_brand_zone_contract_name_key").IsUnique();

            entity.Property(e => e.IbtContractId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("ibt_contract_id");
            entity.Property(e => e.Brand).HasColumnName("brand");
            entity.Property(e => e.BuySell).HasColumnName("buy_sell");
            entity.Property(e => e.BuyerName).HasColumnName("buyer_name");
            entity.Property(e => e.BuyerQseCode).HasColumnName("buyer_qse_code");
            entity.Property(e => e.ConfigId).HasColumnName("config_id");
            entity.Property(e => e.ContractId).HasColumnName("contract_id");
            entity.Property(e => e.ContractName).HasColumnName("contract_name");
            entity.Property(e => e.Counterparty).HasColumnName("counterparty");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasDefaultValueSql("'iw_db_default_user@innowatts.com'::text")
                .HasColumnName("created_by");
            entity.Property(e => e.CredentialId).HasColumnName("credential_id");
            entity.Property(e => e.EndDate)
                .HasDefaultValueSql("'2099-12-31'::text")
                .HasColumnName("end_date");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(false)
                .HasColumnName("is_active");
            entity.Property(e => e.Market).HasColumnName("market");
            entity.Property(e => e.Org).HasColumnName("org");
            entity.Property(e => e.SellerName).HasColumnName("seller_name");
            entity.Property(e => e.SellerQseCode).HasColumnName("seller_qse_code");
            entity.Property(e => e.StartDate)
                .HasDefaultValueSql("'1899-12-31'::text")
                .HasColumnName("start_date");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasDefaultValueSql("'iw_db_default_user@innowatts.com'::text")
                .HasColumnName("updated_by");
            entity.Property(e => e.Zone).HasColumnName("zone");

            entity.HasOne(d => d.Config).WithMany(p => p.IbtContracts)
                .HasForeignKey(d => d.ConfigId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ibt_contracts_config_id_fkey");

            entity.HasOne(d => d.Credential).WithMany(p => p.IbtContracts)
                .HasForeignKey(d => d.CredentialId)
                .HasConstraintName("ibt_contracts_credential_id_fkey");
        });

        modelBuilder.Entity<LocationAttribute>(entity =>
        {
            entity.HasKey(e => e.LocationAttributeId).HasName("location_attributes_pkey");

            entity.ToTable("location_attributes");

            entity.HasIndex(e => new { e.ConfigId, e.CredentialId, e.Market, e.Brand, e.Zone, e.Location, e.Utility }, "location_attributes_config_id_credential_id_market_brand_zo_key").IsUnique();

            entity.Property(e => e.LocationAttributeId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("location_attribute_id");
            entity.Property(e => e.Brand).HasColumnName("brand");
            entity.Property(e => e.ConfigId).HasColumnName("config_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasDefaultValueSql("'iw_db_default_user@innowatts.com'::text")
                .HasColumnName("created_by");
            entity.Property(e => e.CredentialId).HasColumnName("credential_id");
            entity.Property(e => e.Location).HasColumnName("location");
            entity.Property(e => e.LocationName).HasColumnName("location_name");
            entity.Property(e => e.Market).HasColumnName("market");
            entity.Property(e => e.Org).HasColumnName("org");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasDefaultValueSql("'iw_db_default_user@innowatts.com'::text")
                .HasColumnName("updated_by");
            entity.Property(e => e.Utility).HasColumnName("utility");
            entity.Property(e => e.Zone).HasColumnName("zone");

            entity.HasOne(d => d.Config).WithMany(p => p.LocationAttributes)
                .HasForeignKey(d => d.ConfigId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("location_attributes_config_id_fkey");

            entity.HasOne(d => d.Credential).WithMany(p => p.LocationAttributes)
                .HasForeignKey(d => d.CredentialId)
                .HasConstraintName("location_attributes_credential_id_fkey");
        });

        modelBuilder.Entity<MarketMetadatum>(entity =>
        {
            entity.HasKey(e => e.MarketMetadataId).HasName("market_metadata_pkey");

            entity.ToTable("market_metadata");

            entity.HasIndex(e => e.Market, "market_metadata_market_key").IsUnique();

            entity.Property(e => e.MarketMetadataId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("market_metadata_id");
            entity.Property(e => e.ClearingTime).HasColumnName("clearing_time");
            entity.Property(e => e.CloseTime).HasColumnName("close_time");
            entity.Property(e => e.Market).HasColumnName("market");
            entity.Property(e => e.MaxDate).HasColumnName("max_date");
            entity.Property(e => e.Region).HasColumnName("region");
            entity.Property(e => e.Timezone).HasColumnName("timezone");
        });

        modelBuilder.Entity<Migration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("migrations_pkey");

            entity.ToTable("migrations");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<PriceInfo>(entity =>
        {
            entity.HasKey(e => e.PriceInfoId).HasName("price_info_pkey");

            entity.ToTable("price_info");

            entity.HasIndex(e => new { e.ConfigId, e.Org, e.Market, e.Brand, e.Location, e.IntervalStart, e.IntervalEnd }, "price_info_config_id_org_market_brand_location_interval_sta_key").IsUnique();

            entity.Property(e => e.PriceInfoId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("price_info_id");
            entity.Property(e => e.BidPrice)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("10")
                .HasColumnName("bid_price");
            entity.Property(e => e.Brand)
                .HasMaxLength(255)
                .HasColumnName("brand");
            entity.Property(e => e.ConfigId).HasColumnName("config_id");
            entity.Property(e => e.IntervalEnd)
                .HasDefaultValue(24)
                .HasColumnName("interval_end");
            entity.Property(e => e.IntervalStart)
                .HasDefaultValue(1)
                .HasColumnName("interval_start");
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .HasColumnName("location");
            entity.Property(e => e.Market)
                .HasMaxLength(255)
                .HasColumnName("market");
            entity.Property(e => e.OfferPrice)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("10")
                .HasColumnName("offer_price");
            entity.Property(e => e.Org)
                .HasMaxLength(255)
                .HasColumnName("org");

            entity.HasOne(d => d.Config).WithMany(p => p.PriceInfos)
                .HasForeignKey(d => d.ConfigId)
                .HasConstraintName("price_info_config_id_fkey");
        });

        modelBuilder.Entity<RoundingCriterion>(entity =>
        {
            entity.HasKey(e => e.RoundingCriteriaId).HasName("rounding_criteria_pkey");

            entity.ToTable("rounding_criteria");

            entity.HasIndex(e => e.ConfigId, "rounding_criteria_config_id_key").IsUnique();

            entity.Property(e => e.RoundingCriteriaId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("rounding_criteria_id");
            entity.Property(e => e.Bid)
                .HasPrecision(10, 2)
                .HasColumnName("bid");
            entity.Property(e => e.ConfigId).HasColumnName("config_id");
            entity.Property(e => e.Offer)
                .HasPrecision(10, 2)
                .HasColumnName("offer");
            entity.Property(e => e.Org).HasColumnName("org");

            entity.HasOne(d => d.Config).WithOne(p => p.RoundingCriterion)
                .HasForeignKey<RoundingCriterion>(d => d.ConfigId)
                .HasConstraintName("rounding_criteria_config_id_fkey");
        });

        modelBuilder.Entity<Submission>(entity =>
        {
            entity.HasKey(e => e.SubmissionId).HasName("submissions_pkey1");

            entity.ToTable("submissions");

            entity.Property(e => e.SubmissionId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("submission_id");
            entity.Property(e => e.BidId).HasColumnName("bid_id");
            entity.Property(e => e.Brand).HasColumnName("brand");
            entity.Property(e => e.ContractName).HasColumnName("contract_name");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasDefaultValueSql("'iw_db_default_user@innowatts.com'::text")
                .HasColumnName("created_by");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Intervals)
                .HasColumnType("json")
                .HasColumnName("intervals");
            entity.Property(e => e.Location).HasColumnName("location");
            entity.Property(e => e.LocationName).HasColumnName("location_name");
            entity.Property(e => e.Market).HasColumnName("market");
            entity.Property(e => e.MarketRequest).HasColumnName("market_request");
            entity.Property(e => e.MarketResponse).HasColumnName("market_response");
            entity.Property(e => e.Message).HasColumnName("message");
            entity.Property(e => e.Mrid).HasColumnName("mrid");
            entity.Property(e => e.Org).HasColumnName("org");
            entity.Property(e => e.SchedulingType).HasColumnName("scheduling_type");
            entity.Property(e => e.SubmissionGroupId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("submission_group_id");
            entity.Property(e => e.SubmissionStatus).HasColumnName("submission_status");
            entity.Property(e => e.SubmitStatus).HasColumnName("submit_status");
            entity.Property(e => e.SubmitType).HasColumnName("submit_type");
            entity.Property(e => e.SubmittedBy).HasColumnName("submitted_by");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasDefaultValueSql("'iw_db_default_user@innowatts.com'::text")
                .HasColumnName("updated_by");
            entity.Property(e => e.WasSuccessful)
                .HasDefaultValue(false)
                .HasColumnName("was_successful");
            entity.Property(e => e.Zone).HasColumnName("zone");
        });

        modelBuilder.Entity<SubmissionStatusesArchived>(entity =>
        {
            entity.HasKey(e => e.SubmissionStatus).HasName("submission_statuses_pkey");

            entity.ToTable("submission_statuses_archived");

            entity.Property(e => e.SubmissionStatus).HasColumnName("submission_status");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<SubmissionsArchived>(entity =>
        {
            entity.HasKey(e => e.SubmissionId).HasName("submissions_pkey");

            entity.ToTable("submissions_archived");

            entity.Property(e => e.SubmissionId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("submission_id");
            entity.Property(e => e.BidId).HasColumnName("bid_id");
            entity.Property(e => e.Brand).HasColumnName("brand");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasDefaultValueSql("'iw_db_default_user@innowatts.com'::text")
                .HasColumnName("created_by");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Intervals)
                .HasColumnType("json")
                .HasColumnName("intervals");
            entity.Property(e => e.Location).HasColumnName("location");
            entity.Property(e => e.LocationName).HasColumnName("location_name");
            entity.Property(e => e.Market).HasColumnName("market");
            entity.Property(e => e.MarketRequest).HasColumnName("market_request");
            entity.Property(e => e.MarketResponse).HasColumnName("market_response");
            entity.Property(e => e.Message).HasColumnName("message");
            entity.Property(e => e.Mrid).HasColumnName("mrid");
            entity.Property(e => e.Org).HasColumnName("org");
            entity.Property(e => e.SubmissionMetaId).HasColumnName("submission_meta_id");
            entity.Property(e => e.SubmitStatus).HasColumnName("submit_status");
            entity.Property(e => e.SubmitType).HasColumnName("submit_type");
            entity.Property(e => e.SubmittedBy).HasColumnName("submitted_by");
            entity.Property(e => e.TransactionId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("transaction_id");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasDefaultValueSql("'iw_db_default_user@innowatts.com'::text")
                .HasColumnName("updated_by");
            entity.Property(e => e.WasSuccessful)
                .HasDefaultValue(false)
                .HasColumnName("was_successful");
            entity.Property(e => e.Zone).HasColumnName("zone");
        });

        modelBuilder.Entity<SubmissionsMetaArchived>(entity =>
        {
            entity.HasKey(e => e.SubmissionMetaId).HasName("submissions_meta_pkey");

            entity.ToTable("submissions_meta_archived");

            entity.HasIndex(e => new { e.SubmissionMetaId, e.SubmissionGroupId, e.AggregateKey }, "submissions_meta_submission_meta_id_submission_group_id_agg_key").IsUnique();

            entity.Property(e => e.SubmissionMetaId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("submission_meta_id");
            entity.Property(e => e.AggregateKey).HasColumnName("aggregate_key");
            entity.Property(e => e.Brand).HasColumnName("brand");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasDefaultValueSql("'iw_db_default_user@innowatts.com'::text")
                .HasColumnName("created_by");
            entity.Property(e => e.EndFlowDate).HasColumnName("end_flow_date");
            entity.Property(e => e.Location).HasColumnName("location");
            entity.Property(e => e.Market).HasColumnName("market");
            entity.Property(e => e.Org).HasColumnName("org");
            entity.Property(e => e.StartFlowDate).HasColumnName("start_flow_date");
            entity.Property(e => e.SubmissionGroupId).HasColumnName("submission_group_id");
            entity.Property(e => e.SubmissionLog).HasColumnName("submission_log");
            entity.Property(e => e.SubmissionStatus).HasColumnName("submission_status");
            entity.Property(e => e.Submissions)
                .HasColumnType("json")
                .HasColumnName("submissions");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
            entity.Property(e => e.Zone).HasColumnName("zone");

            entity.HasOne(d => d.SubmissionStatusNavigation).WithMany(p => p.SubmissionsMetaArchiveds)
                .HasForeignKey(d => d.SubmissionStatus)
                .HasConstraintName("submissions_meta_submission_status_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
