namespace WDIPaladins.Domain
{
    public class PaladinsSkills
    {
        public long Id { get; set; }

        public long PaladinId { get; set; }

        public long SkillId { get; set; }


        public Skill Skill { get; set; }
    }
}