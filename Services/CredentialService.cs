public interface ICredentialService
{
    public Task<List<CredentialDTO>> GetByOrg(string org);
}

public class CredentialService : ICredentialService
{
    private readonly DataContext _context;

    public CredentialService(DataContext context)
    {
        _context = context;
    }

    public async Task<List<CredentialDTO>> GetByOrg(string org)
    {
        return await _context
            .Credentials.Where(c => c.Org == org && c.IsActive)
            .Select(c => new CredentialDTO
            {
                CredentialId = c.CredentialId,
                ConfigId = c.ConfigId,

                Org = c.Org,
                Market = c.Market,
                Brand = c.Brand,

                Zone = c.Zone,
                Location = c.Location,
                LocationName = c.LocationName,

                UserId = c.UserId,
                UserAccount = c.UserAccount,
                UserPassword = c.UserPassword,
                UserSubAccount = c.UserSubAccount,

                Pfx = c.Pfx,
                PfxPassphrase = c.PfxPassphrase,

                IsActive = c.IsActive,
                IsSuma = c.IsSuma,
                UseCertificate = c.UseCertificate,

                CreatedBy = c.CreatedBy,
                UpdatedBy = c.UpdatedBy,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt,
            })
            .ToListAsync();
    }
}
