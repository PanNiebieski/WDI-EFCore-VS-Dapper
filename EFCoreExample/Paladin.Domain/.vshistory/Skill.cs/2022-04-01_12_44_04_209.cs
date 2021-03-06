using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WDIPaladins.Domain
{
    public class Skill
    {
        public long Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        [NotMapped]
        public bool ToInsert { get; set; }
    }


    public class PaladinSkills
    {
        public long Id { get; set; }

        public long PaladinId { get; set; }

        public long SkillId { get; set; }

        public Paladin Paladin { get; set; }

        public Skill Skill { get; set; }
    }

    public class PaladinItems
    {
        public long Id { get; set; }

        public long PaladinId { get; set; }

        public long ItemId { get; set; }
    }
}