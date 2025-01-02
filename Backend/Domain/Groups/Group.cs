namespace Domain.Groups
{
    public sealed class Group
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }

        private Group(string id, string name, string description, byte[] image)
        {
            Id = id;
            Name = name;
            Description = description;
            Image = image;
        }

        private Group()
        {
        }

        public static Group Create(string id, string name, string description, byte[] image)
        {
            var group = new Group();
            return group;
        }
    }
}
