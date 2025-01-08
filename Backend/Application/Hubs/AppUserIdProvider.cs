using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Application.Hubs;

public class AppUserIdProvider : IUserIdProvider
{
    public string GetUserId(HubConnectionContext connection)
    {

        var userId = connection.User?.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
        return userId;
    }
}
