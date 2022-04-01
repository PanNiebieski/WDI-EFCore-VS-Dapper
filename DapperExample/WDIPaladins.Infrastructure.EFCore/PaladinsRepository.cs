using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WDIPaladins.Application;
using WDIPaladins.Domain;

namespace WDIPaladins.Infrastructure.EFCore
{
    public class PaladinsRepository : IPaladinsRepository
    {
        protected readonly PaladinsContext _dbContext;

        public PaladinsRepository(PaladinsContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<Paladin> AddAsync(Paladin entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteAsync(long id)
        {
            _dbContext.Set<Paladin>().
                Remove(new Paladin() { Id = id});

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<Paladin>> GetAllAsync()
        {
            //var oneWay = await _dbContext.Paladins.ToListAsync();

            var better = await _dbContext.Paladins
                .Include(p => p.Items)
                .Include(p => p.Monastery)
                .ToListAsync();

            return better;
        }

        public async Task<Paladin> GetByIdAsync(long id)
        {
            return await _dbContext.Paladins.FindAsync(id);
        }

        public async Task<Paladin> GetByUniqueIdAsync(Guid id)
        {
            return await _dbContext.Paladins.
                FirstOrDefaultAsync(p => p.UniqueId == id);
        }

        public async Task UpdateAsync(Paladin entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
