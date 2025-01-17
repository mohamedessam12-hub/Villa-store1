using MagicVila_VillaAPi.Model;
using MagicVila_VillaAPi.Repository.Repository;

namespace MagicVila_VillaAPi.Repository.IRepository
{
    public interface IRepositoryVillanum : IRepository<VillaNumber>
    {
        Task<VillaNumber> UpdateAsync(VillaNumber Entity);
    }
}
