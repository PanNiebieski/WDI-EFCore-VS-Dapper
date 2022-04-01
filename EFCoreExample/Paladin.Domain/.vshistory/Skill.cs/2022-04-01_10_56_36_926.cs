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