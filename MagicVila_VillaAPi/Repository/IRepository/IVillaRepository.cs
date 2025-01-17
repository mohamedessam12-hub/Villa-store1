using System.Linq.Expressions;
using MagicVila_VillaAPi.Model;

namespace MagicVila_VillaAPi.Repository.Repository
{
    public interface IVillaRepository : IRepository<Villa>
    {
        Task<Villa> UpdateAsync(Villa Entity);
    }
}
