namespace WDIPaladins.Domain
{
    public class Item
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Speed { get; set; }

        public int Mana { get; set; }
    }
}