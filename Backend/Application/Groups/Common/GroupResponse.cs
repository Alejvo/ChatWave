namespace Application.Groups.Common;

public sealed record GroupResponse(
         string Id,
         string Name,
         string Description,
         string Image
    );