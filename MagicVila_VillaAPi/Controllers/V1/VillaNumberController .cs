using System.Net;
using AutoMapper;
using MagicVila_VillaAPi.Model;
using MagicVila_VillaAPi.Model.VillaDTO;
using MagicVila_VillaAPi.Repository;
using MagicVila_VillaAPi.Repository.IRepository;
using MagicVila_VillaAPi.Repository.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MagicVila_VillaAPi.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[Controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class VillaNumberController : ControllerBase
    {
        protected APIResponse _response;
        public readonly IRepositoryVillanum _repository;
        public readonly IMapper _mapper;
        public readonly IVillaRepository _villaRepository;
        public VillaNumberController(IRepositoryVillanum repository, IMapper mapper, IVillaRepository IvillaRepository)
        {
            _response = new APIResponse();
            _repository = repository;
            _mapper = mapper;
            _villaRepository = IvillaRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetAllVillaNumbers()
        {
            try
            {
                IEnumerable<VillaNumber> villaNumberList = await _repository.GetAllAsync(includeproperty: "Villa");
                _response.Result = _mapper.Map<IEnumerable<VillaNumberDTO>>(villaNumberList);
                _response.statusCode = HttpStatusCode.OK;
                _response.isSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.ErorMassege = new List<string> { ex.ToString() };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }


        [HttpGet("{id:int}", Name = "GetVillaNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetVillaNumber(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest("Invalid ID.");
                }

                var villaNumber = await _repository.GetAsync(v => v.VillaNo == id);
                if (villaNumber == null)
                {
                    return NotFound();
                }

                _response.Result = _mapper.Map<VillaNumberDTO>(villaNumber);
                _response.isSuccess = true;
                _response.statusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.ErorMassege = new List<string> { ex.ToString() };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> AddVillaNumber([FromBody] VillaNumberCreateDTO createDTO)
        {
            try
            {
                if (createDTO == null)
                {
                    return BadRequest(new APIResponse
                    {
                        isSuccess = false,
                        ErorMassege = new List<string> { "Input data is null or invalid." }
                    });
                }

                // تحقق من وجود VillaNo
                if (await _repository.GetAsync(v => v.VillaNo == createDTO.VillaNo) != null)
                {
                    return BadRequest(new APIResponse
                    {
                        isSuccess = false,
                        ErorMassege = new List<string> { "Villa Number already exists." }
                    });
                }

                if (await _villaRepository.GetAsync(v => v.Id == createDTO.VillaId) == null)
                {
                    return BadRequest(new APIResponse
                    {
                        isSuccess = false,
                        ErorMassege = new List<string> { "Villa ID does not exist." }
                    });
                }

                VillaNumber villaNumber = _mapper.Map<VillaNumber>(createDTO);
                villaNumber.CreatedUpdate = DateTime.UtcNow;
                villaNumber.UpdatedDate = DateTime.UtcNow;

                await _repository.CreateAsync(villaNumber);

                return CreatedAtRoute("GetVillaNumber", new { id = villaNumber.VillaNo }, new APIResponse
                {
                    isSuccess = true,
                    Result = _mapper.Map<VillaNumberDTO>(villaNumber)
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new APIResponse
                {
                    isSuccess = false,
                    ErorMassege = new List<string> { ex.Message }
                });
            }
        }



        [HttpDelete("{id:int}", Name = "RemoveVillaNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> RemoveVillaNumber(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest("Invalid ID.");
                }

                var villaNumber = await _repository.GetAsync(v => v.VillaNo == id);
                if (villaNumber == null)
                {
                    return NotFound();
                }

                await _repository.RemoveAsync(villaNumber);

                _response.statusCode = HttpStatusCode.OK;
                _response.isSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.ErorMassege = new List<string> { ex.ToString() };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpPut("{id:int}", Name = "UpdateVillaNumber")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> UpdateVillaNumber(int id, [FromBody] VillaNumberUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.VillaNo)
                {
                    return BadRequest("Invalid input data.");
                }

                var villaNumber = await _repository.GetAsync(v => v.VillaNo == id, false);
                if (villaNumber == null)
                {
                    return NotFound();
                }

                VillaNumber model = _mapper.Map<VillaNumber>(updateDTO);
                model.UpdatedDate = DateTime.UtcNow;

                await _repository.UpdateAsync(model);

                _response.statusCode = HttpStatusCode.NoContent;
                _response.isSuccess = true;
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.ErorMassege = new List<string> { ex.ToString() };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }
    }
}
