public interface IConfigService
{
    public Task<List<ConfigDTO>> GetAll(bool includeInactive);
    public Task<List<ConfigDTO>> GetByOrg(string org);
    public Task<ConfigDTO?> GetById(Guid configId);
    public Task<bool> ActivateById(Guid configId);
    public Task<bool> DeactivateById(Guid configId);
}

public class ConfigService : IConfigService
{
    private readonly DataContext _context;

    public ConfigService(DataContext context)
    {
        _context = context;
    }

    public async Task<bool> ActivateById(Guid configId)
    {
        var config = await _context.Configs.FirstOrDefaultAsync(c =>
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
        var config = await _context.Configs.FirstOrDefaultAsync(c =>
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

    public async Task<List<ConfigDTO>> GetAll(bool includeInactive = false)
    {
        IQueryable<ConfigDTO> configs;

        if (includeInactive == false)
        {
            configs = _context
                .Configs.Where(c => c.IsActive)
                .Select(c => new ConfigDTO
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
            configs = _context.Configs.Select(c => new ConfigDTO
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

    public async Task<ConfigDTO?> GetById(Guid configId)
    {
        return await _context
            .Configs.Where(c => c.ConfigId == configId)
            .Select(c => new ConfigDTO
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

    public async Task<List<ConfigDTO>> GetByOrg(string org)
    {
        return await _context
            .Configs.Where(c => c.Org == org && c.IsActive)
            .Select(c => new ConfigDTO
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
