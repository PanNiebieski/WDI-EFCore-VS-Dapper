namespace WDIPaladins.Api
{
    public class PaladynDto
    {
        public string Name { get; set; }

        public string Title { get; set; }

        public int MonasteryId { get; set; }

        public List<long> ItemIds { get; set; }

        public List<long> SkillIds { get; set; }
    }
}
