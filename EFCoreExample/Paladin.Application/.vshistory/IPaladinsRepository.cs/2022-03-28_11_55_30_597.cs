namespace Paladin.Application
{
    public interface IPaladinsRepository
    {
        Task<Paladin> GetByIdAsync(int id);
    }
}