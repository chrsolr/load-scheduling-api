public static class JwtUtils
{
    public static JwtSecurityToken ConvertToSecurityToken(string? token)
    {
        return new JwtSecurityTokenHandler().ReadJwtToken(token);
    }

    public static DecodedJwtToken? DecodeToken(JwtSecurityToken token)
    {
        var decodedToken = JsonSerializer.Deserialize<DecodedJwtToken>(
            token.Payload.SerializeToJson()
        );

        return decodedToken ?? null;
    }
}
