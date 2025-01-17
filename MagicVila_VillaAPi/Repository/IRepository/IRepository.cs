using MagicVila_VillaAPi.Model;
using System.Linq.Expressions;

namespace MagicVila_VillaAPi.Repository.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? Fillter = null, string? includeproperty = null);
        Task<T> GetAsync(Expression<Func<T, bool>> Fillter = null, bool Tracked = true, string? includeproperty = null);
        Task saveAsync();
        Task RemoveAsync(T Entity);
        Task CreateAsync(T Entity);
    }
    
}
