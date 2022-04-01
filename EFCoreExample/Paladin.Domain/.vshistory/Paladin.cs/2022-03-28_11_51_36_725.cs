namespace Paladin.Domain
{
    public class Paladin
    {
        public int Id { get; set; }

        public Guid UniqueId { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public List<Item> Items { get; set; }
        public List<Skill> Skills { get; set; }
    }
}