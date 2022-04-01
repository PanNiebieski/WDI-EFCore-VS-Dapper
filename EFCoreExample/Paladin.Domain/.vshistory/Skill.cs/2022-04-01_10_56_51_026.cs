using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WDIPaladins.Domain
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        [NotMapped]
        public bool ToInsert { get; set; }
    }
}