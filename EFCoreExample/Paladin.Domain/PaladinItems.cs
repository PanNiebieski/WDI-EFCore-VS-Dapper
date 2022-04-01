namespace WDIPaladins.Domain
{
    public class PaladinsItems
    {
        public long Id { get; set; }

        public long PaladinId { get; set; }

        public long ItemId { get; set; }

        public Item Item { get; set; }
    }
}