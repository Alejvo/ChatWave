namespace Domain.Tokens;

public sealed class Token
{
    public string Id { get; set; }
    public string JwtToken { get; set; }
    public DateTime ExpiryTime { get; set; }
    public string UserId { get; set; }
}
