﻿namespace WDIPaladins.Domain
{
    public class PaladinsItems
    {
        public long Id { get; set; }

        public long PaladinId { get; set; }

        public long ItemId { get; set; }

        public Item Skill { get; set; }
    }
}