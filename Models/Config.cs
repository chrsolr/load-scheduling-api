[Table("configs")]
[Index(
    nameof(Org),
    nameof(Brand),
    nameof(Market),
    IsUnique = true,
    Name = "configs_org_brand_market_key"
)]
public record Config
{
    [Key]
    [Column("config_id")]
    public Guid ConfigId { get; set; }

    [Required]
    [Column("org")]
    public string Org { get; set; } = null!;

    [Required]
    [Column("market")]
    public string Market { get; set; } = null!;

    [Required]
    [Column("brand")]
    public string Brand { get; set; } = null!;

    [Column("created_by")]
    public string CreatedBy { get; set; } = "iw_db_default_user@innowatts.com";

    [Column("updated_by")]
    public string UpdatedBy { get; set; } = "iw_db_default_user@innowatts.com";

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    [Column("is_active")]
    public bool IsActive { get; set; } = false;

    public virtual ICollection<Credential> Credentials { get; set; } =
        new List<Credential>();

    public virtual ICollection<LocationAttribute> LocationAttributes { get; set; } =
        new List<LocationAttribute>();
}
