using AutoMapper;
using MagicVila_VillaAPi.Model;
using MagicVila_VillaAPi.Model.VillaDTO;
using MagicVila_VillaAPi.Repository.IRepository;
using MagicVila_VillaAPi.Repository.Repository;
using MagicVila_VillaAPi.Service.IService;
using System.Net;

namespace MagicVila_VillaAPi.Service
{
    public class VillaNumberService : IVillaNumberService
    {
        private readonly IRepositoryVillanum _repository;
        private readonly IMapper _mapper;
        private readonly IVillaRepository _villaRepository;
        private readonly APIResponse _response;

        public VillaNumberService(IRepositoryVillanum repository, IMapper mapper, IVillaRepository villaRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _villaRepository = villaRepository;
            _response = new APIResponse();
        }

        public async Task<APIResponse> GetAllVillaNumbersAsync()
        {
            try
            {
                var villaNumbers = await _repository.GetAllAsync(includeproperty: "Villa");
                _response.Result = _mapper.Map<IEnumerable<VillaNumberDTO>>(villaNumbers);
                _response.statusCode = HttpStatusCode.OK;
                _response.isSuccess = true;
            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.ErorMassege = new List<string> { ex.Message };
            }

            return _response;
        }

        public async Task<APIResponse> GetVillaNumberAsync(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.isSuccess = false;
                    _response.ErorMassege = new List<string> { "Invalid ID." };
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return _response;
                }

                var villaNumber = await _repository.GetAsync(v => v.VillaNo == id);
                if (villaNumber == null)
                {
                    _response.isSuccess = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return _response;
                }

                _response.Result = _mapper.Map<VillaNumberDTO>(villaNumber);
                _response.statusCode = HttpStatusCode.OK;
                _response.isSuccess = true;
            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.ErorMassege = new List<string> { ex.Message };
            }

            return _response;
        }

        public async Task<APIResponse> CreateVillaNumberAsync(VillaNumberCreateDTO createDTO)
        {
            try
            {
                if (await _repository.GetAsync(v => v.VillaNo == createDTO.VillaNo) != null)
                {
                    _response.isSuccess = false;
                    _response.ErorMassege = new List<string> { "Villa Number already exists." };
                    return _response;
                }

                if (await _villaRepository.GetAsync(v => v.Id == createDTO.VillaId) == null)
                {
                    _response.isSuccess = false;
                    _response.ErorMassege = new List<string> { "Villa ID does not exist." };
                    return _response;
                }

                var villaNumber = _mapper.Map<VillaNumber>(createDTO);
                villaNumber.CreatedUpdate = DateTime.UtcNow;
                villaNumber.UpdatedDate = DateTime.UtcNow;

                await _repository.CreateAsync(villaNumber);

                _response.Result = _mapper.Map<VillaNumberDTO>(villaNumber);
                _response.statusCode = HttpStatusCode.Created;
                _response.isSuccess = true;
            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.ErorMassege = new List<string> { ex.Message };
            }

            return _response;
        }

        public async Task<APIResponse> UpdateVillaNumberAsync(int id, VillaNumberUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.VillaNo)
                {
                    _response.isSuccess = false;
                    _response.ErorMassege = new List<string> { "Invalid input data." };
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return _response;
                }

                var villaNumber = await _repository.GetAsync(v => v.VillaNo == id, false);
                if (villaNumber == null)
                {
                    _response.isSuccess = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return _response;
                }

                var model = _mapper.Map<VillaNumber>(updateDTO);
                model.UpdatedDate = DateTime.UtcNow;

                await _repository.UpdateAsync(model);

                _response.statusCode = HttpStatusCode.NoContent;
                _response.isSuccess = true;
            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.ErorMassege = new List<string> { ex.Message };
            }

            return _response;
        }

        public async Task<APIResponse> DeleteVillaNumberAsync(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.isSuccess = false;
                    _response.ErorMassege = new List<string> { "Invalid ID." };
                    return _response;
                }

                var villaNumber = await _repository.GetAsync(v => v.VillaNo == id);
                if (villaNumber == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.isSuccess = false;
                    return _response;
                }

                await _repository.RemoveAsync(villaNumber);

                _response.statusCode = HttpStatusCode.OK;
                _response.isSuccess = true;
            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.ErorMassege = new List<string> { ex.Message };
            }

            return _response;
        }
    }

}
