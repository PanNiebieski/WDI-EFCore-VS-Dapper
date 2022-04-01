using 


namespace WDIPaladins.Application
{
    public interface IPaladinsRepository
    {
        Task<Paladin> GetByIdAsync(int id);
    }
}