using AutoMapper;
using MagicVila_VillaAPi.Model;
using MagicVila_VillaAPi.Model.VillaDTO;
using MagicVila_VillaAPi.Repository.Repository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

namespace MagicVila_VillaAPi.Service
{
    public class VillaService : IVillaService
    {
        private readonly IVillaRepository _repository;
        private readonly IMapper _mapper;

        public VillaService(IVillaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<APIResponse> GetAllVillas(int rate, string? search)
        {
            var response = new APIResponse();
            try
            {
                IEnumerable<Villa> villaList;
                if (rate > 0)
                {
                    villaList = await _repository.GetAllAsync(e => e.Rate == rate);
                }
                else
                {
                    villaList = await _repository.GetAllAsync();
                }

                if (!string.IsNullOrEmpty(search))
                {
                    villaList = villaList.Where(s => s.Name.ToLower().Contains(search));
                }

                response.Result = _mapper.Map<IEnumerable<VillaDTO>>(villaList);
                response.statusCode = HttpStatusCode.OK;
                response.isSuccess = true;
            }
            catch (Exception ex)
            {
                response.isSuccess = false;
                response.ErorMassege = new List<string>() { ex.ToString() };
            }
            return response;
        }

        public async Task<APIResponse> GetVilla(int id)
        {
            var response = new APIResponse();
            try
            {
                var villa = await _repository.GetAsync(v => v.Id == id);
                if (villa == null)
                {
                    response.statusCode = HttpStatusCode.NotFound;
                    return response;
                }
                response.Result = _mapper.Map<VillaDTO>(villa);
                response.statusCode = HttpStatusCode.OK;
                response.isSuccess = true;
            }
            catch (Exception ex)
            {
                response.isSuccess = false;
                response.ErorMassege = new List<string>() { ex.ToString() };
            }
            return response;
        }

        public async Task<APIResponse> AddVilla(VillaCreateDTO createDTO)
        {
            var response = new APIResponse();
            try
            {
                if (await _repository.GetAsync(v => v.Name.ToLower() == createDTO.Name.ToLower()) != null)
                {
                    response.statusCode = HttpStatusCode.BadRequest;
                    response.ErorMassege = new List<string> { "Villa already exists" };
                    return response;
                }

                var villa = _mapper.Map<Villa>(createDTO);
                await _repository.CreateAsync(villa);
                response.Result = _mapper.Map<VillaDTO>(villa);
                response.statusCode = HttpStatusCode.Created;
                response.isSuccess = true;
            }
            catch (Exception ex)
            {
                response.isSuccess = false;
                response.ErorMassege = new List<string>() { ex.ToString() };
            }
            return response;
        }

        public async Task<APIResponse> RemoveVilla(int id)
        {
            var response = new APIResponse();
            try
            {
                var villa = await _repository.GetAsync(v => v.Id == id);
                if (villa == null)
                {
                    response.statusCode = HttpStatusCode.NotFound;
                    return response;
                }

                await _repository.RemoveAsync(villa);
                response.statusCode = HttpStatusCode.OK;
                response.isSuccess = true;
            }
            catch (Exception ex)
            {
                response.isSuccess = false;
                response.ErorMassege = new List<string>() { ex.ToString() };
            }
            return response;
        }

        public async Task<APIResponse> UpdateVilla(int id, VillaUpdateDTO updateDTO)
        {
            var response = new APIResponse();
            try
            {
                var villa = _mapper.Map<Villa>(updateDTO);
                await _repository.UpdateAsync(villa);
                response.statusCode = HttpStatusCode.NoContent;
                response.isSuccess = true;
            }
            catch (Exception ex)
            {
                response.isSuccess = false;
                response.ErorMassege = new List<string>() { ex.ToString() };
            }
            return response;
        }

        public async Task<APIResponse> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDTO> patchDoc, ModelStateDictionary modelState)
        {
            var response = new APIResponse();
            try
            {
                var villa = await _repository.GetAsync(v => v.Id == id, false);
                if (villa == null)
                {
                    response.statusCode = HttpStatusCode.NotFound;
                    return response;
                }

                var villaDTO = _mapper.Map<VillaUpdateDTO>(villa);
                patchDoc.ApplyTo(villaDTO, modelState);

                var updatedModel = _mapper.Map<Villa>(villaDTO);
                await _repository.UpdateAsync(updatedModel);

                if (!modelState.IsValid)
                {
                    response.statusCode = HttpStatusCode.BadRequest;
                    response.ErorMassege = modelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    return response;
                }

                response.statusCode = HttpStatusCode.NoContent;
                response.isSuccess = true;
            }
            catch (Exception ex)
            {
                response.isSuccess = false;
                response.ErorMassege = new List<string>() { ex.ToString() };
            }
            return response;
        }
    }
}
