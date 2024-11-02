[Table("credentials")]
[Index(
    nameof(Org),
    nameof(Brand),
    nameof(Market),
    nameof(Zone),
    nameof(Location),
    nameof(UserSubAccount),
    IsUnique = true,
    Name = "credentials_org_brand_market_zone_location_user_sub_account_key"
)]
public record Credential
{
    [Key]
    [Column("credential_id")]
    public Guid CredentialId { get; set; }

    [ForeignKey("ConfigId")]
    public Guid ConfigId { get; set; }

    [Column("org")]
    public string Org { get; set; } = null!;

    [Column("market")]
    public string Market { get; set; } = null!;

    [Column("brand")]
    public string Brand { get; set; } = null!;

    [Column("zone")]
    public string Zone { get; set; } = null!;

    [Column("location")]
    public string Location { get; set; } = null!;

    [Column("location_name")]
    public string LocationName { get; set; } = null!;

    [Column("user_id")]
    public string? UserId { get; set; }

    [Column("user_account")]
    public string? UserAccount { get; set; }

    [Column("user_password")]
    public string? UserPassword { get; set; }

    [Column("user_sub_account")]
    public string UserSubAccount { get; set; } = null!;

    [Column("pfx")]
    public string? Pfx { get; set; }

    [Column("pfx_passphrase")]
    public string? PfxPassphrase { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; }

    [Column("is_suma")]
    public bool IsSuma { get; set; }

    [Column("use_certificate")]
    public bool UseCertificate { get; set; }

    [Column("created_by")]
    public string CreatedBy { get; set; } = "iw_db_default_user@innowatts.com";

    [Column("updated_by")]
    public string UpdatedBy { get; set; } = "iw_db_default_user@innowatts.com";

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("created_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public virtual Config Config { get; set; } = null!;
}
