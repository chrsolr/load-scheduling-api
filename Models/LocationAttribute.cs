[Table("credentials")]
[Index(
    nameof(ConfigId),
    nameof(CredentialId),
    nameof(Market),
    nameof(Brand),
    nameof(Zone),
    IsUnique = true,
    Name = "location_attributes_config_id_credential_id_market_brand_zo_key"
)]
public partial class LocationAttribute
{
    [Key]
    [Column("location_attribute_id")]
    public Guid LocationAttributeId { get; set; }

    [ForeignKey("config_id")]
    public Guid ConfigId { get; set; }

    [ForeignKey("credential_id")]
    public Guid CredentialId { get; set; }

    [Column("org")]
    public string Org { get; set; } = null!;

    [Column("market")]
    public string Market { get; set; } = null!;

    [Column("brand")]
    public string Brand { get; set; } = null!;

    [Column("zone")]
    public string Zone { get; set; } = null!;

    [Column("utility")]
    public string Utility { get; set; } = null!;

    [Column("location")]
    public string Location { get; set; } = null!;

    [Column("location_name")]
    public string LocationName { get; set; } = null!;

    [Column("created_by")]
    public string CreatedBy { get; set; } = "iw_db_default_user@innowatts.com";

    [Column("updated_by")]
    public string UpdatedBy { get; set; } = "iw_db_default_user@innowatts.com";

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public virtual Config Config { get; set; } = null!;

    public virtual Credential Credential { get; set; } = null!;
}
