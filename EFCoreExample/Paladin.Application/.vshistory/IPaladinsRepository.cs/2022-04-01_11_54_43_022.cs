using WDIPaladins.Domain;


namespace WDIPaladins.Application
{
    public interface IPaladinsRepository
    {
        Task<Paladin> GetByIdAsync(int id);

        Task<Paladin> GetByUniqueIdAsync(Guid id);

        Task<IReadOnlyList<Paladin>> GetAllAsync();

        Task UpdateAsync(Paladin entity);

        Task<Paladin> AddAsync(Paladin entity);

        Task DeleteAsync(int id);
    }


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