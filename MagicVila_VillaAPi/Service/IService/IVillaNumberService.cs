using MagicVila_VillaAPi.Model;
using MagicVila_VillaAPi.Model.VillaDTO;

namespace MagicVila_VillaAPi.Service.IService
{
    public interface IVillaNumberService
    {
        Task<APIResponse> GetAllVillaNumbersAsync();
        Task<APIResponse> GetVillaNumberAsync(int id);
        Task<APIResponse> CreateVillaNumberAsync(VillaNumberCreateDTO createDTO);
        Task<APIResponse> UpdateVillaNumberAsync(int id, VillaNumberUpdateDTO updateDTO);
        Task<APIResponse> DeleteVillaNumberAsync(int id);
    }

}
