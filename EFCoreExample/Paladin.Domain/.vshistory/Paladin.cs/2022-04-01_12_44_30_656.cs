namespace WDIPaladins.Domain
{
    public class Paladin
    {
        public long Id { get; set; }

        public Guid UniqueId { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public Monastery Monastery { get; set; }

        public List<PaladinItems> Items { get; set; }
        public List<PaladinSkills> Skills { get; set; }

        public Paladin()
        {
            Items = new List<PaladinItems>();
            Skills = new List<PaladinSkills>();
        }
    }
}