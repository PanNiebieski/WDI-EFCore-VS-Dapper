using WDIPaladins.Domain;


namespace WDIPaladins.Application
{
    public interface IPaladinsRepository
    {
        Task<Paladin> GetByIdAsync(int id);

        Task<Paladin> GetByIdAsync(int id);

        Task<IReadOnlyList<Paladin>> GetAllAsync();

        Task UpdateAsync(Paladin entity);

        Task DeleteAsync(Paladin entity);
    }
}