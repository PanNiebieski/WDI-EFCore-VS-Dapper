namespace WDIPaladins.Domain
{
    public class PaladinSkills
    {
        public long Id { get; set; }

        public long PaladinId { get; set; }

        public long SkillId { get; set; }

        public Paladin Paladin { get; set; }

        public Skill Skill { get; set; }
    }
}