namespace FamilyBudget.Infrastructure.Auth;

public class AuthOptions
{
    public string IssuerSigningKey { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int ExpiryInMinutes { get; set; }
}