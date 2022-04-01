namespace WDIPaladins.Domain
{
    public class Paladin
    {
        public int Id { get; set; }

        public Guid UniqueId { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public Monastery Monastery { get; set; }

        public List<Item> Items { get; set; }
        public List<Skill> Skills { get; set; }

        public Paladin()
        {
            Items = new List<Item>();
            Skills = new List<Skill>();
        }
    }
}