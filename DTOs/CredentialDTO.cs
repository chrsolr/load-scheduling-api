public record CredentialDTO
{
    public Guid CredentialId { get; set; }
    public Guid ConfigId { get; set; }

    public string Org { get; set; } = null!;
    public string Market { get; set; } = null!;
    public string Brand { get; set; } = null!;

    public string Zone { get; set; } = null!;
    public string Location { get; set; } = null!;
    public string LocationName { get; set; } = null!;

    public string? UserId { get; set; }
    public string? UserAccount { get; set; }
    public string? UserPassword { get; set; }
    public string UserSubAccount { get; set; } = null!;

    public string? Pfx { get; set; }
    public string? PfxPassphrase { get; set; }

    public bool IsActive { get; set; } = false;
    public bool IsSuma { get; set; }
    public bool UseCertificate { get; set; }

    public string CreatedBy { get; set; } = null!;
    public string UpdatedBy { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public virtual Config Config { get; set; } = null!;
}
