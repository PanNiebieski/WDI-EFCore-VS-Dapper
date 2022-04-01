using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WDIPaladins.Domain
{
    public class Item
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public long Attack { get; set; }
        public long Defense { get; set; }
        public long Speed { get; set; }

        public long Mana { get; set; }

        [JsonIgnore]
        [NotMapped]
        public bool ToInsert { get; set; }
    }
}