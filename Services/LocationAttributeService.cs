public interface ILocationAttributeService
{
    public Task<List<LocationAttributeDTO>> GetByOrg(string org);
    public Task<List<LocationAttributeDTO>> GetByConfigId(Guid configId);
    public Task<LocationAttributeDTO?> GetById(Guid locationAttributeId);
}

public class LocationAttributeService : ILocationAttributeService
{
    private readonly DataContext _context;

    public LocationAttributeService(DataContext context)
    {
        _context = context;
    }

    public async Task<List<LocationAttributeDTO>> GetByOrg(string org)
    {
        return await _context
            .LocationAttributes.Where(la => la.Org == org)
            .Select(la => new LocationAttributeDTO
            {
                LocationAttributeId = la.LocationAttributeId,
                ConfigId = la.ConfigId,
                CredentialId = la.CredentialId,

                Org = la.Org,
                Market = la.Market,
                Brand = la.Brand,
                Zone = la.Zone,
                Utility = la.Utility,
                Location = la.Location,
                LocationName = la.LocationName,

                CreatedBy = la.CreatedBy,
                UpdatedBy = la.UpdatedBy,
                CreatedAt = la.CreatedAt,
                UpdatedAt = la.UpdatedAt,
            })
            .ToListAsync();
    }

    public async Task<List<LocationAttributeDTO>> GetByConfigId(Guid configId)
    {
        return await _context
            .LocationAttributes.Where(la => la.ConfigId == configId)
            .Select(la => new LocationAttributeDTO
            {
                LocationAttributeId = la.LocationAttributeId,
                ConfigId = la.ConfigId,
                CredentialId = la.CredentialId,

                Org = la.Org,
                Market = la.Market,
                Brand = la.Brand,
                Zone = la.Zone,
                Utility = la.Utility,
                Location = la.Location,
                LocationName = la.LocationName,

                CreatedBy = la.CreatedBy,
                UpdatedBy = la.UpdatedBy,
                CreatedAt = la.CreatedAt,
                UpdatedAt = la.UpdatedAt,
            })
            .ToListAsync();
    }

    public async Task<LocationAttributeDTO?> GetById(Guid locationAttributeId)
    {
        return await _context
            .LocationAttributes.Where(la =>
                la.LocationAttributeId == locationAttributeId
            )
            .Select(la => new LocationAttributeDTO
            {
                LocationAttributeId = la.LocationAttributeId,
                ConfigId = la.ConfigId,
                CredentialId = la.CredentialId,

                Org = la.Org,
                Market = la.Market,
                Brand = la.Brand,
                Zone = la.Zone,
                Utility = la.Utility,
                Location = la.Location,
                LocationName = la.LocationName,

                CreatedBy = la.CreatedBy,
                UpdatedBy = la.UpdatedBy,
                CreatedAt = la.CreatedAt,
                UpdatedAt = la.UpdatedAt,
            })
            .FirstOrDefaultAsync();
    }
}
