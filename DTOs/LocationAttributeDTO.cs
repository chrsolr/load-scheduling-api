public record LocationAttributeDTO
{
    public Guid LocationAttributeId { get; set; }
    public Guid ConfigId { get; set; }
    public Guid CredentialId { get; set; }

    public string Org { get; set; } = null!;
    public string Market { get; set; } = null!;
    public string Brand { get; set; } = null!;
    public string Zone { get; set; } = null!;
    public string Utility { get; set; } = null!;
    public string Location { get; set; } = null!;
    public string LocationName { get; set; } = null!;

    public string CreatedBy { get; set; } = null!;
    public string UpdatedBy { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
