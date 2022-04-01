namespace WDIPaladins.Infrastructure.Dapper
{
    public static class DeleteFlags
    {
        private static List<DeleteSkill> _deleteSkill = new List<DeleteSkill>();

        private static List<DeleteItem> _deleteitem = new List<DeleteItem>();

        public static void AddDeleteSkillFlag(long paladinId, long skillId)
        {
            _deleteSkill.Add(new(paladinId, skillId));
        }


        public static void AddDeleteItemFlag(long paladinId, long skillId)
        {
            _deleteitem.Add(new(paladinId, skillId));
        }

        public static List<DeleteSkill> GetDeletedSkills()
        {
            return _deleteSkill;
        }

        public static List<DeleteItem> GetDeletedItems()
        {
            return _deleteitem;
        }

        public static void ClearSkill(long paldinId)
        {
            _deleteSkill = _deleteSkill.Where
                (k => k.paladinId != paldinId).ToList();
        }

        public static void Clearitem(long paldinId)
        {
            _deleteitem = _deleteitem.Where
                (k => k.paladinId != paldinId).ToList();
        }
    }
}
