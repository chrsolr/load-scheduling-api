public record DecodedJwtToken
{
    [JsonPropertyName("exp")]
    public int Exp { get; set; }

    [JsonPropertyName("ver")]
    public int Version { get; set; }

    [JsonPropertyName("sub")]
    public string? Email { get; set; }

    [JsonPropertyName("uid")]
    public string? UserId { get; set; }

    [JsonPropertyName("orgs")]
    public string[] Organizations { get; set; } = Array.Empty<string>();

    [JsonPropertyName("jti")]
    public string? Jti { get; set; }

    [JsonPropertyName("iss")]
    public string? Issuer { get; set; }

    [JsonPropertyName("aud")]
    public string? Audience { get; set; }

    [JsonPropertyName("iat")]
    public int IssuedAt { get; set; }

    [JsonPropertyName("cid")]
    public string? ClientId { get; set; }

    [JsonPropertyName("scp")]
    public string[] Scopes { get; set; } = Array.Empty<string>();

    [JsonPropertyName("auth_time")]
    public int AuthTime { get; set; }

    [JsonPropertyName("toolPolicies")]
    public string[] ToolPolicies { get; set; } = Array.Empty<string>();

    [JsonPropertyName("podTypeSubscriptions")]
    public string[] PodTypeSubscriptions { get; set; } = Array.Empty<string>();

    [JsonPropertyName("dashboardType")]
    public string[] DashboardType { get; set; } = Array.Empty<string>();
}
