using System.Linq;
using System.Linq.Expressions;
using MagicVila_VillaAPi.Model;
using MagicVila_VillaAPi.Repository.Repository;
using Microsoft.EntityFrameworkCore;
namespace MagicVila_VillaAPi.Repository
{
    public class VillaRepository : Repository<Villa> ,IVillaRepository
    {
        private readonly ApplicationdbContext _dp;

        public VillaRepository(ApplicationdbContext dp) : base(dp) 
        {
            _dp = dp;
        }

       
        public async Task<Villa> UpdateAsync(Villa Entity)
        {
            Entity.UpdateDate = DateTime.Now;
            _dp.Villas.Update(Entity);
            await _dp.SaveChangesAsync();
            return Entity;
        }
    }
}
