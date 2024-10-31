public interface IOrgConfigService
{
    public Task<List<OrgConfigDTO>> GetAll(bool includeInactive);
}

public class OrgConfigService : IOrgConfigService
{
    private readonly DataContext _context;

    public OrgConfigService(DataContext context)
    {
        _context = context;
    }

    public async Task<List<OrgConfigDTO>> GetAll(bool includeInactive = false)
    {
        IQueryable<OrgConfigDTO> configs;

        if (includeInactive == false)
        {
            configs = _context
                .OrgConfigs.Where(c => c.IsActive)
                .Select(c => new OrgConfigDTO
                {
                    ConfigId = c.ConfigId,
                    Org = c.Org,
                    Market = c.Market,
                    Brand = c.Brand,
                    CreatedBy = c.CreatedBy,
                    UpdatedBy = c.UpdatedBy,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt,
                    IsActive = c.IsActive,
                });
        }
        else
        {
            configs = _context.OrgConfigs.Select(c => new OrgConfigDTO
            {
                ConfigId = c.ConfigId,
                Org = c.Org,
                Market = c.Market,
                Brand = c.Brand,
                CreatedBy = c.CreatedBy,
                UpdatedBy = c.UpdatedBy,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt,
                IsActive = c.IsActive,
            });
        }

        return await configs.ToListAsync();
    }
}
