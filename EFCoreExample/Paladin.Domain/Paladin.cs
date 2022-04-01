namespace WDIPaladins.Domain
{
    public class Paladin
    {
        public long Id { get; set; }

        public Guid UniqueId { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public Monastery Monastery { get; set; }

        public List<PaladinsItems> Items { get; set; }
        public List<PaladinsSkills> Skills { get; set; }

        public Paladin()
        {
            Items = new List<PaladinsItems>();
            Skills = new List<PaladinsSkills>();
        }
    }
}