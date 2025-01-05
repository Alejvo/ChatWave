namespace Domain.Users;

public sealed record LoginRequest(
    string Email,
    string Password
    );