﻿using WDIPaladins.Domain;


namespace WDIPaladins.Application
{
    public interface IPaladinsRepository
    {
        Task<Paladin> GetByIdAsync(long id);

        Task<Paladin> GetByUniqueIdAsync(Guid id);

        Task<IReadOnlyList<Paladin>> GetAllAsync();

        Task UpdateAsync(Paladin entity);

        Task<Paladin> AddAsync(Paladin entity);

        Task DeleteAsync(long  id);
    }
}