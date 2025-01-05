namespace Domain.Groups;

public sealed class GroupRequest
{
    public string Id {  get; private set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public byte[] Image { get; set; }

    private GroupRequest(string id, string name, string description, byte[] image)
    {
        Id = id;
        Name = name;
        Description = description;
        Image = image;
    }

    public static GroupRequest Create(string name, string description, byte[] image)
    {
        var id = Guid.NewGuid().ToString();
        var group = new GroupRequest(id, name, description, image);
        return group;
    }
}