namespace Paladin.Domain
{
    public class Paladin
    {
        public int Id { get; set; }

        public Guid UniqueId { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public List<Item> Items { get; set; }

    }

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