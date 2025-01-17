using Villa_mvc.Model.VillaDTO;

namespace Villa_mvc.Service.IService
{
    public interface IVillaServics
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(VillaCreateDTO obj, string token);
        Task<T> UpdateAsync<T>(VillaUpdateDTO obj, string token);
        Task<T> DeleteAsync<T>(int id, string token);
    }
}
