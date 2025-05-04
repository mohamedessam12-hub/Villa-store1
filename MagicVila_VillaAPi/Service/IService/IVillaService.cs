using MagicVila_VillaAPi.Model;
using MagicVila_VillaAPi.Model.VillaDTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MagicVila_VillaAPi.Service
{
    public interface IVillaService
    {
        Task<APIResponse> GetAllVillas(int rate, string? search);
        Task<APIResponse> GetVilla(int id);
        Task<APIResponse> AddVilla(VillaCreateDTO createDTO);
        Task<APIResponse> RemoveVilla(int id);
        Task<APIResponse> UpdateVilla(int id, VillaUpdateDTO updateDTO);
        Task<APIResponse> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDTO> patchDoc, ModelStateDictionary modelState);
    }
}
