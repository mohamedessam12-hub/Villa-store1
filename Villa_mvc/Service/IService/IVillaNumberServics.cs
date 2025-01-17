using Villa_mvc.Model.VillaDTO;

namespace Villa_mvc.Service.IService
{
    public interface IVillaNumberServics
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(VillaNumberCreateDTO obj, string token);
        Task<T> UpdateAsync<T>(VillaNumberUpdateDTO obj, string token);
        Task<T> DeleteAsync<T>(int id, string token);
    }
}
