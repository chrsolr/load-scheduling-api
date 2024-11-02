public interface IOrgConfigService
{
    public Task<List<OrgConfigDTO>> GetAll(bool includeInactive);
    public Task<List<OrgConfigDTO>> GetByOrg(string org);
    public Task<OrgConfigDTO?> GetById(Guid configId);
    public Task<bool> ActivateById(Guid configId);
    public Task<bool> DeactivateById(Guid configId);
}

public class OrgConfigService : IOrgConfigService
{
    private readonly DataContext _context;

    public OrgConfigService(DataContext context)
    {
        _context = context;
    }

    public async Task<bool> ActivateById(Guid configId)
    {
        var config = await _context.OrgConfigs.FirstOrDefaultAsync(c =>
            c.ConfigId == configId
        );

        if (config is null)
        {
            return false;
        }

        config.IsActive = true;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeactivateById(Guid configId)
    {
        var config = await _context.OrgConfigs.FirstOrDefaultAsync(c =>
            c.ConfigId == configId
        );

        if (config is null)
        {
            return false;
        }

        config.IsActive = false;

        await _context.SaveChangesAsync();

        return true;
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

    public async Task<OrgConfigDTO?> GetById(Guid configId)
    {
        return await _context
            .OrgConfigs.Where(c => c.ConfigId == configId)
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
            })
            .FirstOrDefaultAsync();
    }

    public async Task<List<OrgConfigDTO>> GetByOrg(string org)
    {
        return await _context
            .OrgConfigs.Where(c => c.Org == org && c.IsActive)
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
            })
            .ToListAsync();
    }
}
