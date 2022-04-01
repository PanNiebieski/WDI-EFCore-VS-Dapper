namespace WDIPaladins.Application
{
    public interface IPaladinRepositoryMoreDapperFriendly
    {
        Task AddItemToPaladin(int itemId, int paladinId);

        Task DeleteItemToPaladin(int itemId, int paladinId);

        Task AddSkillToPaladin(int skillId, int paladinId);

        Task DeleteSkillToPaladin(int skillId, int paladinId);

        Task ChangeNameOfPaladin(int paladinId, string newName);

        Task ChangeTitleOfPaladin(int paladinId, string newTitle);
    }
}