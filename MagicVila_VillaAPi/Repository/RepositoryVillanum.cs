using MagicVila_VillaAPi.Model;
using MagicVila_VillaAPi.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace MagicVila_VillaAPi.Repository
{
    public class RepositoryVillanum : Repository<VillaNumber>, IRepositoryVillanum
    {
        private readonly ApplicationdbContext _dp;
        public RepositoryVillanum(ApplicationdbContext dp) : base(dp)
        {
            _dp = dp;
          
        }
        public async Task<VillaNumber> UpdateAsync(VillaNumber Entity)
        {
            Entity.UpdatedDate = DateTime.Now;
            _dp.villaNumbers.Update(Entity);
            await _dp.SaveChangesAsync();
            return Entity;
        }

       
    }


}
