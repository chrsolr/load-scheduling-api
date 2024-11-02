public class ConfigDTO
{
    public Guid ConfigId { get; set; }

    public string Org { get; set; } = null!;
    public string Market { get; set; } = null!;
    public string Brand { get; set; } = null!;

    public string CreatedBy { get; set; } = null!;
    public string UpdatedBy { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public bool IsActive { get; set; } = false;
}
