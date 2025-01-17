using MagicVila_VillaAPi.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MagicVila_VillaAPi.Repository
{
    public class Repository<T> where T : class
    {
        private readonly ApplicationdbContext _dp;
        internal DbSet<T> dpSet;
        public Repository(ApplicationdbContext dp)
        {
            _dp = dp;
            this.dpSet = _dp.Set<T>();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> Fillter = null, bool Tracked = true, string? includeproperty = null)
        {
            IQueryable<T> Quary = dpSet;
            if (!Tracked)
            {
                Quary = Quary.AsNoTracking();
            }

            if (Fillter != null)
            {
                Quary = Quary.Where(Fillter);
            }
            if (includeproperty != null)
            {
                foreach(var includeprop in includeproperty.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    Quary = Quary.Include(includeprop);
                }
            }
            return await Quary.FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? Fillter = null, string? includeproperty = null)
        {
            IQueryable<T> Quary = dpSet;
            if (Fillter != null)
            {
                Quary = Quary.Where(Fillter);
            }
            if (includeproperty != null)
            {
                foreach (var prop in includeproperty.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    Quary = Quary.Include(prop);
                }
            }
            return await Quary.ToListAsync();
        }

        public async Task RemoveAsync(T Entity)
        {
            _dp.Remove(Entity);
            await saveAsync();
        }

        public async Task saveAsync()
        {
            await _dp.SaveChangesAsync();
        }
        public async Task CreateAsync(T Entity)
        {
            await dpSet.AddAsync(Entity);
            await saveAsync();
        }
    }
}
